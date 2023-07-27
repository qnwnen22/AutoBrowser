using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class MainForm : Form
    {
        BindingList<WorkEvent> bindings;

        public MainForm()
        {
            var cefSettings = new CefSettings
            {
                Locale = "ko-kr",
                LogSeverity = LogSeverity.Disable,
            };
            Cef.Initialize(cefSettings);

            InitializeComponent();
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.LastAddress))
            {
                this.chromiumWebBrowser.Load("https://www.google.com");
            }
            else
            {
                this.chromiumWebBrowser.Load(Properties.Settings.Default.LastAddress);
            }
            this.chromiumWebBrowser.LoadHandler = new LoadHandler(this);
            bindings = new BindingList<WorkEvent>(WorkManager.WorkEvents);
            this.dataGridViewEvents.DataSource = bindings;
            this.dataGridViewEvents.Columns[0].ReadOnly = true;
            //InputForm.Show(this);
        }

        private async void ToolStripMenuItemWorkDo_Click(object sender, EventArgs e)
        {
            this.menuStrip1.Enabled = false;
            WorkEvent current = null;
            try
            {
                var sb = new StringBuilder();
                foreach (WorkEvent item in WorkManager.WorkEvents)
                {
                    current = item;
                    switch (item.EventType)
                    {
                        case BrowserEvent.Click:
                            await this.chromiumWebBrowser.SendMouseClickEventAsync(item.Path);
                            break;
                        case BrowserEvent.Input:
                            await this.chromiumWebBrowser.SendKeyEventAsync(item.Path, item.Value.ToString());
                            break;
                        case BrowserEvent.Wait:
                            int sleep = Convert.ToInt32(item.Value) * 1000;
                            Thread.Sleep(sleep);
                            break;
                        case BrowserEvent.Load:
                            LoadUrlAsyncResponse response = await this.chromiumWebBrowser.LoadUrlAsync(item.Path.ToString());
                            break;
                        case BrowserEvent.Get:
                            List<string> split = item.Path.Split("\n");
                            foreach (var address in split)
                            {
                                LoadUrlAsyncResponse response2 = await this.chromiumWebBrowser.LoadUrlAsync(address.Trim());
                                string html = await this.chromiumWebBrowser.GetMainFrame().GetSourceAsync();
                                string text = html.SelectNode(item.Value, HtmlType.InnerText);
                                sb.AppendLine(text);
                            }
                            break;
                        default:
                            break;
                    }
                }

                var scrapTexts = sb.ToString();
                if (!string.IsNullOrWhiteSpace(scrapTexts))
                {
                    var result = MessageBox.Show("스크래핑한 텍스트를 클립보드에 저장시겠습니까?", "수집", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        Clipboard.SetText(scrapTexts);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"잘못된 동작을 설정입니다.\n{current?.ToString()}");
            }
            finally
            {
                this.menuStrip1.Invoke(new Action(() =>
                {
                    this.menuStrip1.Enabled = true;
                }));
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.chromiumWebBrowser.Back();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            this.chromiumWebBrowser.Forward();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            this.chromiumWebBrowser.Reload();
        }

        private void ToolStripMenuItemWorkAdd_Click(object sender, EventArgs e)
        {
            InputForm.Show(this);
        }

        public void DataReload(List<WorkEvent> workEvents = null)
        {
            if (workEvents != null)
            {
                WorkManager.WorkEvents = workEvents;
                bindings = new BindingList<WorkEvent>(WorkManager.WorkEvents);
                this.dataGridViewEvents.DataSource = bindings;
            }
            else
            {
                bindings.ResetBindings();
            }


        }
        public class LoadHandler : ILoadHandler
        {
            MainForm mainForm;
            public LoadEndEvent LoadEndEvent { get; set; }

            public LoadHandler(MainForm form)
            {
                this.mainForm = form;
            }

            public void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
            {
                mainForm.textBoxUrl.Invoke(new Action(() =>
                {
                    mainForm.textBoxUrl.Text = chromiumWebBrowser.Address;
                    Properties.Settings.Default.LastAddress = chromiumWebBrowser.Address;
                    Properties.Settings.Default.Save();
                }));
            }

            public void OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs)
            {
            }

            public void OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs)
            {
            }

            public void OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs loadingStateChangedArgs)
            {
            }
        }


        private void textBoxUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string address = null;
                try
                {
                    Uri uri = new Uri(this.textBoxUrl.Text);
                    address = this.textBoxUrl.Text;
                }
                catch
                {
                    address = $"https://www.google.com/search?q={this.textBoxUrl.Text}";
                }
                this.chromiumWebBrowser.Load(address);
            }
        }

        private void ToolStripMenuItemWorkSave_Click(object sender, EventArgs e)
        {
            if (WorkManager.WorkEvents.Count <= 0)
            {
                MessageBox.Show("저장할 동작이 없습니다");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "|*.json";
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var saveDir = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.SavedDirectory = saveDir;
                Properties.Settings.Default.Save();
                string json = WorkManager.WorkEvents.ToJson();
                System.IO.File.WriteAllText(saveFileDialog.FileName, json);
                MessageBox.Show("동작을 저장하였습니다");
            }
        }

        private void ToolStripMenuItemWorkLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.json";
            openFileDialog.InitialDirectory = Properties.Settings.Default.SavedDirectory;
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var json = System.IO.File.ReadAllText(openFileDialog.FileName);
                try
                {
                    var workEvents = json.ToClass<List<WorkEvent>>();
                    DataReload(workEvents);
                }
                catch
                {
                    MessageBox.Show("잘못된 형식의 파일입니다.");
                }
            }

        }
    }
}
