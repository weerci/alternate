namespace Alternative
{
    partial class FrmProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProject));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbOutside = new System.Windows.Forms.PictureBox();
            this.pbFacede = new System.Windows.Forms.PictureBox();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbJuristic = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDesign = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbPayment = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutside)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFacede)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pbOutside);
            this.groupBox1.Controls.Add(this.pbFacede);
            this.groupBox1.Controls.Add(this.tbNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbJuristic);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbDesign);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbPayment);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 387);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // pbOutside
            // 
            this.pbOutside.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOutside.Location = new System.Drawing.Point(284, 141);
            this.pbOutside.Name = "pbOutside";
            this.pbOutside.Size = new System.Drawing.Size(260, 229);
            this.pbOutside.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOutside.TabIndex = 18;
            this.pbOutside.TabStop = false;
            this.pbOutside.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pbFacede_LoadCompleted);
            // 
            // pbFacede
            // 
            this.pbFacede.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbFacede.Location = new System.Drawing.Point(9, 141);
            this.pbFacede.Name = "pbFacede";
            this.pbFacede.Size = new System.Drawing.Size(260, 229);
            this.pbFacede.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFacede.TabIndex = 17;
            this.pbFacede.TabStop = false;
            this.pbFacede.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pbFacede_LoadCompleted);
            // 
            // tbNumber
            // 
            this.tbNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNumber.Location = new System.Drawing.Point(146, 63);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(398, 20);
            this.tbNumber.TabIndex = 11;
            this.tbNumber.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Номер договора:";
            // 
            // cbJuristic
            // 
            this.cbJuristic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbJuristic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJuristic.FormattingEnabled = true;
            this.cbJuristic.Location = new System.Drawing.Point(146, 37);
            this.cbJuristic.Name = "cbJuristic";
            this.cbJuristic.Size = new System.Drawing.Size(398, 21);
            this.cbJuristic.TabIndex = 9;
            this.cbJuristic.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Заказчик:";
            // 
            // cbDesign
            // 
            this.cbDesign.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDesign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDesign.FormattingEnabled = true;
            this.cbDesign.Location = new System.Drawing.Point(146, 114);
            this.cbDesign.Name = "cbDesign";
            this.cbDesign.Size = new System.Drawing.Size(398, 21);
            this.cbDesign.TabIndex = 4;
            this.cbDesign.SelectedIndexChanged += new System.EventHandler(this.cbDesign_SelectedIndexChanged);
            this.cbDesign.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Дизайн:";
            // 
            // cbPayment
            // 
            this.cbPayment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPayment.FormattingEnabled = true;
            this.cbPayment.Location = new System.Drawing.Point(146, 88);
            this.cbPayment.Name = "cbPayment";
            this.cbPayment.Size = new System.Drawing.Size(398, 21);
            this.cbPayment.TabIndex = 2;
            this.cbPayment.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Платежная система:";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(146, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(398, 20);
            this.tbName.TabIndex = 0;
            this.tbName.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Проект:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 409);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Enabled = false;
            this.btnCreate.Location = new System.Drawing.Point(416, 409);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // FrmProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 444);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 355);
            this.Name = "FrmProject";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmProject";
            this.Resize += new System.EventHandler(this.FrmProject_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutside)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFacede)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPayment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDesign;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbJuristic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbOutside;
        private System.Windows.Forms.PictureBox pbFacede;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label label2;

    }
}