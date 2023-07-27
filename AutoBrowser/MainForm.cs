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
        readonly string TxtFileFilter = "텍스트파일(*.txt)|*.txt";
        readonly string JsonFileFilter = "Json파일(*.json)|*.json";
        public MainForm()
        {
            var cefSettings = new CefSettings
            {
                Locale = "ko-kr",
                LogSeverity = LogSeverity.Disable,
            };
            Cef.Initialize(cefSettings);

            InitializeComponent();
            this.Icon = Properties.Resources.lamyLogo;
            this.chromiumWebBrowser.Load("http://lamysolution.com/");
            //if (string.IsNullOrWhiteSpace(Properties.Settings.Default.LastAddress)) { }
            //else { this.chromiumWebBrowser.Load(Properties.Settings.Default.LastAddress); }
            this.chromiumWebBrowser.LoadHandler = new LoadHandler(this);
            this.chromiumWebBrowser.LifeSpanHandler = new LifeSpanHandler();

            bindings = new BindingList<WorkEvent>(WorkManager.WorkEvents);
            this.dataGridViewEvents.DataSource = bindings;
            this.dataGridViewEvents.Columns[0].ReadOnly = true;
            //InputForm.Show(this);
        }

        private async void ToolStripMenuItemWorkDo_Click(object sender, EventArgs e)
        {
            this.menuStrip1.Enabled = false;
            int? idx = null;
            try
            {
                var sb = new StringBuilder();
                foreach (WorkEvent item in WorkManager.WorkEvents)
                {
                    idx = WorkManager.WorkEvents.IndexOf(item);

                    await Task.Factory.StartNew(() =>
                    {
                        while (this.chromiumWebBrowser.IsLoading) Thread.Sleep(100);
                    });

                    LoadUrlAsyncResponse response = null;
                    switch (item.EventType)
                    {
                        case BrowserEvent.Click:
                            await this.chromiumWebBrowser.SendMouseClickEventAsync(item.Path);
                            break;
                        case BrowserEvent.Input:
                            await this.chromiumWebBrowser.SendKeyEventAsync(item.Path, item.Value);
                            break;
                        case BrowserEvent.Wait:
                            await Task.Factory.StartNew(() =>
                            {
                                int sleep = Convert.ToInt32(item.Value) * 1000;
                                Thread.Sleep(sleep);
                            });
                            break;
                        case BrowserEvent.Load:
                            response = await this.chromiumWebBrowser.LoadUrlAsync(item.Path);
                            do { Thread.Sleep(100); }
                            while (!response.Success);
                            break;
                        case BrowserEvent.Get:
                            List<string> split = item.Path.Split("\n");
                            foreach (var address in split)
                            {
                                if (string.IsNullOrWhiteSpace(address)) continue;
                                response = await this.chromiumWebBrowser.LoadUrlAsync(address.Trim());
                                while (chromiumWebBrowser.IsLoading)
                                {
                                    Thread.Sleep(10);
                                }
                                string html = await this.chromiumWebBrowser.GetSourceAsync();
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
                    var result = MessageBox.Show("수집한 내용을 저장시겠습니까?", "AutoBrowser",
                        MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        var save = new SaveFileDialog();
                        var now = DateTime.UtcNow.ToString("yyyyMMddHHmmss").ToString();
                        save.FileName = $"save.txt";
                        save.Filter = TxtFileFilter;
                        DialogResult dialogResult = save.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            System.IO.File.WriteAllText(save.FileName, scrapTexts);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{idx + 1} 번째 잘못된 동작을 설정입니다.\n{exception.Message}");
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

        public class LifeSpanHandler : ILifeSpanHandler
        {
            public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
            {
                return true;
            }

            public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser) { }

            public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser) { }

            public bool OnBeforePopup(IWebBrowser chromiumWebBrowser,
                                      IBrowser browser,
                                      IFrame frame,
                                      string targetUrl,
                                      string targetFrameName,
                                      WindowOpenDisposition targetDisposition,
                                      bool userGesture,
                                      IPopupFeatures popupFeatures,
                                      IWindowInfo windowInfo,
                                      IBrowserSettings browserSettings,
                                      ref bool noJavascriptAccess,
                                      out IWebBrowser newBrowser)
            {
                newBrowser = null;
                if (popupFeatures.IsPopup == false)
                {
                    chromiumWebBrowser.Load(targetUrl);
                }
                return true;
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
            saveFileDialog.FileName = "work.json";
            saveFileDialog.Filter = JsonFileFilter;
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
            openFileDialog.FileName = "work.json";
            openFileDialog.Filter = JsonFileFilter;
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

        private void dataGridViewEvents_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataReload();
        }
    }
}
