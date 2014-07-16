namespace Alternative
{
    partial class FormDesign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDesign));
            this.label1 = new System.Windows.Forms.Label();
            this.tbDesignName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pbFacede = new System.Windows.Forms.PictureBox();
            this.pbOutside = new System.Windows.Forms.PictureBox();
            this.btnLoadFacede = new System.Windows.Forms.Button();
            this.btnLoadOutside = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.ofdImage = new System.Windows.Forms.OpenFileDialog();
            this.btnDelFacede = new System.Windows.Forms.Button();
            this.btnDelOfside = new System.Windows.Forms.Button();
            this.cbFormatPic = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbFacede)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutside)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название:";
            // 
            // tbDesignName
            // 
            this.tbDesignName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDesignName.Location = new System.Drawing.Point(78, 6);
            this.tbDesignName.Name = "tbDesignName";
            this.tbDesignName.Size = new System.Drawing.Size(422, 20);
            this.tbDesignName.TabIndex = 1;
            this.tbDesignName.TextChanged += new System.EventHandler(this.tbDesignName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Лицевая сторона:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Оборотная сторона:";
            // 
            // pbFacede
            // 
            this.pbFacede.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbFacede.Location = new System.Drawing.Point(15, 60);
            this.pbFacede.Name = "pbFacede";
            this.pbFacede.Size = new System.Drawing.Size(238, 179);
            this.pbFacede.TabIndex = 4;
            this.pbFacede.TabStop = false;
            // 
            // pbOutside
            // 
            this.pbOutside.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbOutside.Location = new System.Drawing.Point(262, 60);
            this.pbOutside.Name = "pbOutside";
            this.pbOutside.Size = new System.Drawing.Size(238, 179);
            this.pbOutside.TabIndex = 5;
            this.pbOutside.TabStop = false;
            // 
            // btnLoadFacede
            // 
            this.btnLoadFacede.Location = new System.Drawing.Point(116, 34);
            this.btnLoadFacede.Name = "btnLoadFacede";
            this.btnLoadFacede.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFacede.TabIndex = 6;
            this.btnLoadFacede.Text = "...";
            this.btnLoadFacede.UseVisualStyleBackColor = true;
            this.btnLoadFacede.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnLoadOutside
            // 
            this.btnLoadOutside.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadOutside.Location = new System.Drawing.Point(392, 34);
            this.btnLoadOutside.Name = "btnLoadOutside";
            this.btnLoadOutside.Size = new System.Drawing.Size(75, 23);
            this.btnLoadOutside.TabIndex = 7;
            this.btnLoadOutside.Text = "...";
            this.btnLoadOutside.UseVisualStyleBackColor = true;
            this.btnLoadOutside.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(425, 252);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Enabled = false;
            this.btnCreate.Location = new System.Drawing.Point(344, 252);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 9;
            this.btnCreate.Text = "button4";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnDelFacede
            // 
            this.btnDelFacede.Location = new System.Drawing.Point(197, 34);
            this.btnDelFacede.Name = "btnDelFacede";
            this.btnDelFacede.Size = new System.Drawing.Size(27, 23);
            this.btnDelFacede.TabIndex = 10;
            this.btnDelFacede.Text = "X";
            this.btnDelFacede.UseVisualStyleBackColor = true;
            this.btnDelFacede.Click += new System.EventHandler(this.btnDel_Image);
            // 
            // btnDelOfside
            // 
            this.btnDelOfside.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelOfside.Location = new System.Drawing.Point(473, 34);
            this.btnDelOfside.Name = "btnDelOfside";
            this.btnDelOfside.Size = new System.Drawing.Size(27, 23);
            this.btnDelOfside.TabIndex = 11;
            this.btnDelOfside.Text = "X";
            this.btnDelOfside.UseVisualStyleBackColor = true;
            this.btnDelOfside.Click += new System.EventHandler(this.btnDel_Image);
            // 
            // cbFormatPic
            // 
            this.cbFormatPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFormatPic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormatPic.FormattingEnabled = true;
            this.cbFormatPic.Location = new System.Drawing.Point(141, 254);
            this.cbFormatPic.Name = "cbFormatPic";
            this.cbFormatPic.Size = new System.Drawing.Size(121, 21);
            this.cbFormatPic.TabIndex = 12;
            this.cbFormatPic.Visible = false;
            this.cbFormatPic.SelectedIndexChanged += new System.EventHandler(this.cbFormatPic_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Отображение рисунка:";
            this.label4.Visible = false;
            // 
            // FormDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(512, 293);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbFormatPic);
            this.Controls.Add(this.btnDelOfside);
            this.Controls.Add(this.btnDelFacede);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLoadOutside);
            this.Controls.Add(this.btnLoadFacede);
            this.Controls.Add(this.pbOutside);
            this.Controls.Add(this.pbFacede);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDesignName);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(520, 320);
            this.Name = "FormDesign";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormDesign";
            this.Resize += new System.EventHandler(this.FormDesign_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbFacede)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOutside)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDesignName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbFacede;
        private System.Windows.Forms.PictureBox pbOutside;
        private System.Windows.Forms.Button btnLoadFacede;
        private System.Windows.Forms.Button btnLoadOutside;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.OpenFileDialog ofdImage;
        private System.Windows.Forms.Button btnDelFacede;
        private System.Windows.Forms.Button btnDelOfside;
        private System.Windows.Forms.ComboBox cbFormatPic;
        private System.Windows.Forms.Label label4;
    }
}