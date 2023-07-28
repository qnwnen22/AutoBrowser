
namespace AutoBrowser
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IsSaveAddress = new System.Windows.Forms.CheckBox();
            this.buttonReLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.IsSavedDirectory = new System.Windows.Forms.CheckBox();
            this.SavedDirectory = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LastAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.LastAddress);
            this.groupBox1.Controls.Add(this.IsSaveAddress);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "브라우저 설정";
            // 
            // IsSaveAddress
            // 
            this.IsSaveAddress.AutoSize = true;
            this.IsSaveAddress.Location = new System.Drawing.Point(6, 20);
            this.IsSaveAddress.Name = "IsSaveAddress";
            this.IsSaveAddress.Size = new System.Drawing.Size(140, 16);
            this.IsSaveAddress.TabIndex = 1;
            this.IsSaveAddress.Text = "마지막 주소 자동저장";
            this.IsSaveAddress.UseVisualStyleBackColor = true;
            this.IsSaveAddress.CheckedChanged += new System.EventHandler(this.IsSaveAddress_CheckedChanged);
            // 
            // buttonReLoad
            // 
            this.buttonReLoad.Location = new System.Drawing.Point(12, 226);
            this.buttonReLoad.Name = "buttonReLoad";
            this.buttonReLoad.Size = new System.Drawing.Size(92, 23);
            this.buttonReLoad.TabIndex = 3;
            this.buttonReLoad.Text = "초기화";
            this.buttonReLoad.UseVisualStyleBackColor = true;
            this.buttonReLoad.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(110, 226);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(298, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "저장";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(414, 226);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "취소";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // IsSavedDirectory
            // 
            this.IsSavedDirectory.AutoSize = true;
            this.IsSavedDirectory.Location = new System.Drawing.Point(7, 20);
            this.IsSavedDirectory.Name = "IsSavedDirectory";
            this.IsSavedDirectory.Size = new System.Drawing.Size(128, 16);
            this.IsSavedDirectory.TabIndex = 4;
            this.IsSavedDirectory.Text = "저장 경로 자동저장";
            this.IsSavedDirectory.UseVisualStyleBackColor = true;
            // 
            // SavedDirectory
            // 
            this.SavedDirectory.Location = new System.Drawing.Point(69, 41);
            this.SavedDirectory.Name = "SavedDirectory";
            this.SavedDirectory.Size = new System.Drawing.Size(396, 21);
            this.SavedDirectory.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.IsSavedDirectory);
            this.groupBox2.Controls.Add(this.SavedDirectory);
            this.groupBox2.Location = new System.Drawing.Point(12, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(477, 121);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "파일 저장";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "현재 경로";
            // 
            // LastAddress
            // 
            this.LastAddress.Location = new System.Drawing.Point(69, 42);
            this.LastAddress.Name = "LastAddress";
            this.LastAddress.Size = new System.Drawing.Size(396, 21);
            this.LastAddress.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "시작 주소";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 261);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonReLoad);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "환경설정";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox IsSaveAddress;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonReLoad;
        private System.Windows.Forms.CheckBox IsSavedDirectory;
        private System.Windows.Forms.TextBox SavedDirectory;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LastAddress;
        private System.Windows.Forms.Label label2;
    }
}