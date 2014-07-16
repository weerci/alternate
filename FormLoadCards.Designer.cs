namespace Alternative
{
    partial class FormLoadCards
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLoadCards));
            this.label1 = new System.Windows.Forms.Label();
            this.tbProjectNumber = new System.Windows.Forms.TextBox();
            this.tbProjectName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnLoadToBase = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.ofdLoadFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер проекта:";
            // 
            // tbProjectNumber
            // 
            this.tbProjectNumber.Location = new System.Drawing.Point(113, 8);
            this.tbProjectNumber.Name = "tbProjectNumber";
            this.tbProjectNumber.ReadOnly = true;
            this.tbProjectNumber.Size = new System.Drawing.Size(159, 20);
            this.tbProjectNumber.TabIndex = 1;
            this.tbProjectNumber.TabStop = false;
            // 
            // tbProjectName
            // 
            this.tbProjectName.Location = new System.Drawing.Point(113, 34);
            this.tbProjectName.Name = "tbProjectName";
            this.tbProjectName.ReadOnly = true;
            this.tbProjectName.Size = new System.Drawing.Size(159, 20);
            this.tbProjectName.TabIndex = 3;
            this.tbProjectName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя проекта:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Выбрать файл:";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(22, 79);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(221, 20);
            this.tbPath.TabIndex = 0;
            this.tbPath.TextChanged += new System.EventHandler(this.tbPath_TextChanged);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(243, 78);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(29, 22);
            this.btnLoadFile.TabIndex = 1;
            this.btnLoadFile.Text = "...";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnLoadToBase
            // 
            this.btnLoadToBase.Enabled = false;
            this.btnLoadToBase.Location = new System.Drawing.Point(197, 108);
            this.btnLoadToBase.Name = "btnLoadToBase";
            this.btnLoadToBase.Size = new System.Drawing.Size(75, 23);
            this.btnLoadToBase.TabIndex = 3;
            this.btnLoadToBase.Text = "Загрузить";
            this.btnLoadToBase.UseVisualStyleBackColor = true;
            this.btnLoadToBase.Click += new System.EventHandler(this.btnLoadToBase_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Enabled = false;
            this.btnCheck.Location = new System.Drawing.Point(116, 108);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 23);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Проверить";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // FormLoadCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnLoadToBase);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbProjectName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbProjectNumber);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLoadCards";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Загрузка карточек";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbProjectNumber;
        private System.Windows.Forms.TextBox tbProjectName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnLoadToBase;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.OpenFileDialog ofdLoadFile;
    }
}