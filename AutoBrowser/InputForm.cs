using System;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class InputForm : Form
    {
        private static InputForm instace;
        public static InputForm GetInstance(MainForm mainForm)
        {
            if (instace == null || instace.IsDisposed)
            {
                instace = new InputForm(mainForm);
            }
            return instace;
        }
        public static void Show(MainForm mainForm)
        {
            if (instace?.IsDisposed == false)
            {
                Application.OpenForms[instace.Name].Activate();
            }
            GetInstance(mainForm).Show();
        }


        MainForm worksForm;
        private InputForm(MainForm worksForm)
        {
            this.worksForm = worksForm;
            //Point location = worksForm.Location;
            //location.Offset(this.worksForm.Width, 0);
            //this.Location = location;
            InitializeComponent();
            this.comboBoxEvent.DataSource = Enum.GetValues(typeof(BrowserEvent));
        }

        private void comboBoxEvent_SelectedValueChanged(object sender, EventArgs e)
        {

            ComboBox combox = sender as ComboBox;
            int select = combox.SelectedIndex;
            switch (select)
            {
                case 0:
                    this.textBoxPath.Enabled = true;
                    this.textBoxValue.Enabled = false;
                    break;
                case 2:
                    this.textBoxPath.Enabled = false;
                    this.textBoxValue.Enabled = true;
                    break;
                case 3:
                    this.textBoxPath.Enabled = true;
                    this.textBoxValue.Enabled = false;
                    break;
                case 1:
                case 4:
                default:
                    this.textBoxPath.Enabled = true;
                    this.textBoxValue.Enabled = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BrowserEvent _event = (BrowserEvent)Enum.Parse(typeof(BrowserEvent), comboBoxEvent.SelectedIndex.ToString());

                switch (_event)
                {
                    case BrowserEvent.Click:
                        if (string.IsNullOrWhiteSpace(textBoxPath.Text)) throw new Exception("위치정보가 필요합니다");
                        break;
                    case BrowserEvent.Input:
                        if (string.IsNullOrWhiteSpace(textBoxPath.Text)) throw new Exception("위치정보가 필요합니다");
                        if (string.IsNullOrWhiteSpace(textBoxValue.Text)) throw new Exception("입력 값이 필요합니다");
                        break;
                    case BrowserEvent.Wait:
                        if (string.IsNullOrWhiteSpace(textBoxValue.Text)) throw new Exception("입력 값이 필요합니다");
                        break;
                    case BrowserEvent.Load:
                    case BrowserEvent.Get:
                        if (string.IsNullOrWhiteSpace(textBoxPath.Text)) throw new Exception("위치정보가 필요합니다");
                        break;
                    default:
                        break;
                }

                var workEvent = new WorkEvent(_event, textBoxPath.Text, textBoxValue.Text);
                WorkManager.WorkEvents.Add(workEvent);
                //MessageBox.Show("동작이 추가 되었습니다.");
                //this.comboBoxEvent.SelectedIndex = 0;
                this.textBoxPath.Text = "";
                this.textBoxValue.Text = "";
                worksForm.DataReload();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
