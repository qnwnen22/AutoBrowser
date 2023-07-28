using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            SettingsPropertyCollection properties = Properties.Settings.Default.Properties;
            foreach (SettingsProperty property in properties)
            {
                var name = property.Name;
                FieldInfo fieldInfo = this.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo != null)
                {
                    dynamic value = fieldInfo.GetValue(this);

                    Type type = value.GetType();

                    switch (type.Name)
                    {
                        case "CheckBox":
                            value.Checked = (bool)Properties.Settings.Default[name];
                            break;
                        case "TextBox":
                            value.Text = (string)Properties.Settings.Default[name];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void Save()
        {
            SettingsPropertyCollection properties = Properties.Settings.Default.Properties;
            foreach (SettingsProperty property in properties)
            {
                var name = property.Name;
                FieldInfo fieldInfo = this.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
                if (fieldInfo != null)
                {
                    dynamic value = fieldInfo.GetValue(this);

                    Type type = value.GetType();

                    switch (type.Name)
                    {
                        case "CheckBox":
                            Properties.Settings.Default[name] = (bool)value.Checked;
                            break;
                        case "TextBox":
                            Properties.Settings.Default[name] = (string)value.Text;
                            break;
                        default:
                            break;
                    }
                }
            }
            Properties.Settings.Default.Save();
        }

        private void IsSaveAddress_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.IsSaveAddress = (sender as CheckBox).Checked;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("현재 설정을 저장하시겠습니까?",
                                                   "설정",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Save();
                MessageBox.Show("저장되었습니다.");
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            this.Close();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("모든 설정을 기본값으로 복원하시겠습니까?",
                                                   "설정",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Properties.Settings.Default.Reset();
                MessageBox.Show("설정이 초기화 되었습니다.");
            }
            this.Close();
        }
    }
}
