using AutoBrowser.Handler;
using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            
            this.Text += $" {version}";
            this.Icon = Properties.Resources.lamyLogo;
            
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.LastAddress))
                BrowserLoad("http://lamysolution.com/");
            else
                BrowserLoad(Properties.Settings.Default.LastAddress);
            
            this.chromiumWebBrowser.LoadHandler = new MainLoadHandler(this);
            this.chromiumWebBrowser.LifeSpanHandler = new LifeSpanHandler();
            bindings = new BindingList<WorkEvent>(WorkManager.WorkEvents);
            this.dataGridViewEvents.DataSource = bindings;
            this.dataGridViewEvents.Columns[0].ReadOnly = true;
        }

        private async void ToolStripMenuItemWorkDo_Click(object sender, EventArgs e)
        {
            this.MenuStrip1.Enabled = false;
            int? idx = null;
            try
            {
                var sb = new StringBuilder();
                foreach (WorkEvent item in WorkManager.WorkEvents)
                {
                    idx = WorkManager.WorkEvents.IndexOf(item);
                    await this.chromiumWebBrowser.WaitLoadingAsync();

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
                            int sleep = Convert.ToInt32(item.Value) * 1000;
                            await WaitAsync(sleep);
                            break;

                        case BrowserEvent.Load:
                            await this.chromiumWebBrowser.LoadUrlAsync(item.Path);
                            break;

                        case BrowserEvent.Get:

                            List<string> split = item.Path.Split("\n");
                            foreach (var address in split)
                            {
                                if (string.IsNullOrWhiteSpace(address)) continue;
                                response = await this.chromiumWebBrowser.LoadUrlAsync(address.Trim());

                                string html = await this.chromiumWebBrowser.GetSourceAsync();
                                List<string> values = item.Value.Split("\n");
                                if (values.Count <= 0)
                                {
                                    string text = html.SelectNode(item.Value, HtmlType.InnerText);
                                    sb.AppendLine(text);
                                }
                                else
                                {
                                    for (int i = 0; i < values.Count; i++)
                                    {
                                        string value = values[i];
                                        if (string.IsNullOrWhiteSpace(value)) continue;
                                        if (i != 0)
                                        {
                                            sb.Append("\t");
                                        }
                                        string text = html.SelectNode(value, HtmlType.InnerText);
                                        sb.Append(text);
                                    }
                                    sb.Append("\n");
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                var scrapTexts = sb.ToString();
                if (!string.IsNullOrWhiteSpace(scrapTexts))
                {
                    var result = MessageBox.Show("수집한 내용을 저장시겠습니까?", "AutoBrowser", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        SaveFile(scrapTexts, TxtFileFilter);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{idx + 1}번째는 잘못된 동작 설정입니다.\n{exception.Message}");
            }
            finally
            {
                this.MenuStrip1.Invoke(new Action(() =>
                {
                    this.MenuStrip1.Enabled = true;
                }));
            }
        }

        private void dataGridViewEvents_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataReload();
        }

        private void ToolStripMenuItemSetting_Click(object sender, EventArgs e)
        {
            var setting = new SettingForm();
            setting.ShowDialog();
        }
    }
}
