
namespace AutoBrowser
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemWorkAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemWorkDo = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemWorkSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemWorkLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.buttonReload = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chromiumWebBrowser = new CefSharp.WinForms.ChromiumWebBrowser();
            this.dataGridViewEvents = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemWorkAdd,
            this.ToolStripMenuItemWorkDo,
            this.ToolStripMenuItemWorkSave,
            this.ToolStripMenuItemWorkLoad,
            this.ToolStripMenuItemSetting});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.MenuStrip1.Size = new System.Drawing.Size(1004, 24);
            this.MenuStrip1.TabIndex = 0;
            this.MenuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItemWorkAdd
            // 
            this.ToolStripMenuItemWorkAdd.Name = "ToolStripMenuItemWorkAdd";
            this.ToolStripMenuItemWorkAdd.Size = new System.Drawing.Size(67, 22);
            this.ToolStripMenuItemWorkAdd.Text = "동작추가";
            this.ToolStripMenuItemWorkAdd.Click += new System.EventHandler(this.ToolStripMenuItemWorkAdd_Click);
            // 
            // ToolStripMenuItemWorkDo
            // 
            this.ToolStripMenuItemWorkDo.Name = "ToolStripMenuItemWorkDo";
            this.ToolStripMenuItemWorkDo.Size = new System.Drawing.Size(67, 22);
            this.ToolStripMenuItemWorkDo.Text = "동작실행";
            this.ToolStripMenuItemWorkDo.Click += new System.EventHandler(this.ToolStripMenuItemWorkDo_Click);
            // 
            // ToolStripMenuItemWorkSave
            // 
            this.ToolStripMenuItemWorkSave.Name = "ToolStripMenuItemWorkSave";
            this.ToolStripMenuItemWorkSave.Size = new System.Drawing.Size(67, 22);
            this.ToolStripMenuItemWorkSave.Text = "동작저장";
            this.ToolStripMenuItemWorkSave.Click += new System.EventHandler(this.ToolStripMenuItemWorkSave_Click);
            // 
            // ToolStripMenuItemWorkLoad
            // 
            this.ToolStripMenuItemWorkLoad.Name = "ToolStripMenuItemWorkLoad";
            this.ToolStripMenuItemWorkLoad.Size = new System.Drawing.Size(95, 22);
            this.ToolStripMenuItemWorkLoad.Text = "동작 불러오기";
            this.ToolStripMenuItemWorkLoad.Click += new System.EventHandler(this.ToolStripMenuItemWorkLoad_Click);
            // 
            // ToolStripMenuItemSetting
            // 
            this.ToolStripMenuItemSetting.Name = "ToolStripMenuItemSetting";
            this.ToolStripMenuItemSetting.Size = new System.Drawing.Size(67, 22);
            this.ToolStripMenuItemSetting.Text = "환경설정";
            this.ToolStripMenuItemSetting.Click += new System.EventHandler(this.ToolStripMenuItemSetting_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.textBoxUrl);
            this.panel1.Controls.Add(this.buttonReload);
            this.panel1.Controls.Add(this.buttonForward);
            this.panel1.Controls.Add(this.buttonBack);
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1004, 23);
            this.panel1.TabIndex = 0;
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUrl.Location = new System.Drawing.Point(201, 1);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(800, 21);
            this.textBoxUrl.TabIndex = 3;
            this.textBoxUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUrl_KeyDown);
            // 
            // buttonReload
            // 
            this.buttonReload.Location = new System.Drawing.Point(132, 0);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(67, 23);
            this.buttonReload.TabIndex = 2;
            this.buttonReload.Text = "새로고침";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Location = new System.Drawing.Point(66, 0);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(67, 23);
            this.buttonForward.TabIndex = 1;
            this.buttonForward.Text = "앞으로";
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(0, 0);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(67, 23);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.Text = "뒤로";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton;
            this.MainPanel.Controls.Add(this.panel2);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1004, 537);
            this.MainPanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1004, 510);
            this.panel2.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chromiumWebBrowser);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewEvents);
            this.splitContainer1.Size = new System.Drawing.Size(1004, 510);
            this.splitContainer1.SplitterDistance = 664;
            this.splitContainer1.TabIndex = 0;
            // 
            // chromiumWebBrowser
            // 
            this.chromiumWebBrowser.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chromiumWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.chromiumWebBrowser.Name = "chromiumWebBrowser";
            this.chromiumWebBrowser.Size = new System.Drawing.Size(664, 510);
            this.chromiumWebBrowser.TabIndex = 0;
            // 
            // dataGridViewEvents
            // 
            this.dataGridViewEvents.AllowUserToAddRows = false;
            this.dataGridViewEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEvents.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewEvents.Name = "dataGridViewEvents";
            this.dataGridViewEvents.RowHeadersWidth = 62;
            this.dataGridViewEvents.RowTemplate.Height = 23;
            this.dataGridViewEvents.Size = new System.Drawing.Size(336, 510);
            this.dataGridViewEvents.TabIndex = 0;
            this.dataGridViewEvents.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEvents_CellEndEdit);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.MenuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEvents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemWorkAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetting;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemWorkDo;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser;
        private System.Windows.Forms.DataGridView dataGridViewEvents;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemWorkSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemWorkLoad;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

