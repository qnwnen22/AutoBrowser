﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class MainForm
    {
        public bool SaveFile(string content, string filter)
        {
            bool isSave = false;

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = filter;
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var saveDir = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.SavedDirectory = saveDir;
                Properties.Settings.Default.Save();
                System.IO.File.WriteAllText(saveFileDialog.FileName, content);
                isSave = true;
            }
            return isSave;
        }
        private void ToolStripMenuItemWorkSave_Click(object sender, EventArgs e)
        {
            if (WorkManager.WorkEvents.Count <= 0)
            {
                MessageBox.Show("저장할 동작이 없습니다");
            }
            else
            {
                string json = WorkManager.WorkEvents.ToJson();

                var isSave = SaveFile(json, JsonFileFilter);
                if (isSave)
                {
                    MessageBox.Show("동작을 저장하였습니다");
                }
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
    }
}
