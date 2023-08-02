using AutoBrowser.Handler;
using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
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
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            this.Text += $" {version}";
            this.Icon = Properties.Resources.lamyLogo;

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.LastAddress))
                BrowserLoad("http://lamysolution.com/");
            else
                BrowserLoad(Properties.Settings.Default.LastAddress);

            this.chromiumWebBrowser.LoadHandler = new MainLoadHandler(this);
            this.chromiumWebBrowser.LifeSpanHandler = new LifeSpanHandler();
            this.chromiumWebBrowser.MenuHandler = new ContextMenuHandler();
            bindings = new BindingList<WorkEvent>(WorkManager.WorkEvents);
            this.dataGridViewEvents.DataSource = bindings;
            this.dataGridViewEvents.Columns[0].ReadOnly = true;
            this.comboBoxEvent.DataSource = Enum.GetValues(typeof(BrowserEvent));

        }

        private void comboBoxEvent_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox combox = sender as ComboBox;
            var select = combox.SelectedItem.ToString();
            BrowserEvent browserEvent = (BrowserEvent)Enum.Parse(typeof(BrowserEvent), select);

            switch (browserEvent)
            {
                case BrowserEvent.Click:
                case BrowserEvent.Load:
                case BrowserEvent.Text:
                case BrowserEvent.Image:
                case BrowserEvent.Link:
                    this.textBoxPath.Enabled = true;
                    this.textBoxValue.Enabled = false;
                    break;
                case BrowserEvent.Input:
                    this.textBoxPath.Enabled = true;
                    this.textBoxValue.Enabled = true;
                    break;
                case BrowserEvent.Wait:
                    this.textBoxPath.Enabled = false;
                    this.textBoxValue.Enabled = true;
                    break;
                default:
                    this.textBoxPath.Enabled = false;
                    this.textBoxValue.Enabled = false;
                    break;
            }
            this.textBoxPath.Text = "";
            this.textBoxValue.Text = "";
        }

        private void textBoxValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back) return;
            if (this.comboBoxEvent.SelectedIndex == 2)
            {
                var tb = sender as RichTextBox;
                if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9]"))
                {
                    MessageBox.Show("대기는 숫자만 입력이 가능합니다");
                    tb.Text = "";
                }
            }
        }

        private void buttonWorkAdd_Click(object sender, EventArgs e)
        {
            try
            {
                BrowserEvent _event = (BrowserEvent)Enum.Parse(typeof(BrowserEvent), comboBoxEvent.SelectedIndex.ToString());

                switch (_event)
                {
                    case BrowserEvent.Click:
                    case BrowserEvent.Load:
                    case BrowserEvent.Text:
                    case BrowserEvent.Image:
                    case BrowserEvent.Link:
                        if (string.IsNullOrWhiteSpace(textBoxPath.Text)) throw new Exception("위치정보가 필요합니다");
                        break;
                    case BrowserEvent.Input:
                        if (string.IsNullOrWhiteSpace(textBoxPath.Text)) throw new Exception("위치정보가 필요합니다");
                        if (string.IsNullOrWhiteSpace(textBoxValue.Text)) throw new Exception("입력 값이 필요합니다");
                        break;
                    case BrowserEvent.Wait:
                        if (string.IsNullOrWhiteSpace(textBoxValue.Text)) throw new Exception("입력 값이 필요합니다");
                        break;

                    default:
                        break;
                }

                var workEvent = new WorkEvent(_event, textBoxPath.Text, textBoxValue.Text);
                WorkManager.WorkEvents.Add(workEvent);
                this.textBoxPath.Text = "";
                this.textBoxValue.Text = "";
                DataReload();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }



        private async void buttonWorkDo_Click(object sender, EventArgs e)
        {
            this.MenuStrip1.Enabled = false;
            int? idx = null;
            try
            {
                var sb = new StringBuilder();
                foreach (WorkEvent item in WorkManager.WorkEvents)
                {
                    idx = WorkManager.WorkEvents.IndexOf(item);
                    string html = null;
                    await this.chromiumWebBrowser.WaitLoadingAsync();

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

                        case BrowserEvent.Text:
                            if (string.IsNullOrWhiteSpace(item.Path))
                            {
                                throw new Exception("값은 비어있을 수 없습니다.");
                            }
                            item.Path = item.Path.Trim();
                            html = await this.chromiumWebBrowser.GetSourceAsync();
                            List<string> values = item.Path.Split("\n");
                            for (int i = 0; i < values.Count; i++)
                            {
                                string value = values[i];
                                if (string.IsNullOrWhiteSpace(value)) continue;
                                if (i != 0)
                                {
                                    sb.Append("\t");
                                }
                                string text = html.SelectNode(value, HtmlType.InnerText);
                                if (string.IsNullOrWhiteSpace(text))
                                {
                                    text = "";
                                }
                                text = HttpUtility.HtmlDecode(text).Trim();
                                text = $"\"{text}\"";
                                sb.Append(text);
                            }
                            sb.Append("\n");
                            break;

                        case BrowserEvent.Image:
                            html = await this.chromiumWebBrowser.GetSourceAsync();
                            string src = html.SelectNode(item.Path, "src");
                            sb.AppendLine(src);
                            break;

                        case BrowserEvent.Link:
                            html = await this.chromiumWebBrowser.GetSourceAsync();
                            string aTagXPath = $"{item.Path}//a";
                            var hrefs = html.SelectNodes(aTagXPath, "href");
                            if (hrefs == null) continue;
                            foreach (var href in hrefs)
                            {
                                sb.AppendLine(href);
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
                        if (Properties.Settings.Default.UseClipboard)
                        {
                            SetClipboardText(scrapTexts);
                        }
                        else
                        {
                            SaveFile(scrapTexts, TxtFileFilter);
                        }
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

        private void devToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.chromiumWebBrowser.ShowDevTools();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((Keys.F12) == keyData)
            {
                this.chromiumWebBrowser.ShowDevTools();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
