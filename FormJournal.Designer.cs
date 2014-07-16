namespace Alternative
{
    partial class FormJournal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormJournal));
            this.dgvJournal = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbNumCard = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbLinkCard = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUnload = new System.Windows.Forms.Button();
            this.btnYestoday = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvJournal
            // 
            this.dgvJournal.AllowUserToAddRows = false;
            this.dgvJournal.AllowUserToDeleteRows = false;
            this.dgvJournal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJournal.Location = new System.Drawing.Point(15, 94);
            this.dgvJournal.Name = "dgvJournal";
            this.dgvJournal.ReadOnly = true;
            this.dgvJournal.RowHeadersWidth = 24;
            this.dgvJournal.Size = new System.Drawing.Size(519, 186);
            this.dgvJournal.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbNumCard);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbLinkCard);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 76);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Карта:";
            // 
            // tbNumCard
            // 
            this.tbNumCard.Location = new System.Drawing.Point(85, 45);
            this.tbNumCard.Name = "tbNumCard";
            this.tbNumCard.ReadOnly = true;
            this.tbNumCard.Size = new System.Drawing.Size(100, 20);
            this.tbNumCard.TabIndex = 5;
            this.tbNumCard.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Номер карты:";
            // 
            // tbLinkCard
            // 
            this.tbLinkCard.Location = new System.Drawing.Point(85, 19);
            this.tbLinkCard.Name = "tbLinkCard";
            this.tbLinkCard.ReadOnly = true;
            this.tbLinkCard.Size = new System.Drawing.Size(100, 20);
            this.tbLinkCard.TabIndex = 3;
            this.tbLinkCard.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ид. карты:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUnload);
            this.groupBox2.Controls.Add(this.btnYestoday);
            this.groupBox2.Controls.Add(this.btnToday);
            this.groupBox2.Controls.Add(this.dtpTo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dtpFrom);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(226, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(308, 76);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Фильтр:";
            // 
            // btnUnload
            // 
            this.btnUnload.Location = new System.Drawing.Point(227, 17);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(75, 23);
            this.btnUnload.TabIndex = 11;
            this.btnUnload.Text = "Получить";
            this.btnUnload.UseVisualStyleBackColor = true;
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // btnYestoday
            // 
            this.btnYestoday.Location = new System.Drawing.Point(227, 42);
            this.btnYestoday.Name = "btnYestoday";
            this.btnYestoday.Size = new System.Drawing.Size(75, 23);
            this.btnYestoday.TabIndex = 10;
            this.btnYestoday.Text = "За вчера";
            this.btnYestoday.UseVisualStyleBackColor = true;
            this.btnYestoday.Click += new System.EventHandler(this.btnYestoday_Click);
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(146, 42);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(75, 23);
            this.btnToday.TabIndex = 9;
            this.btnToday.Text = "За сегодня";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click_1);
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(146, 19);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(75, 20);
            this.dtpTo.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(115, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "по";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(30, 19);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(75, 20);
            this.dtpFrom.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "С";
            // 
            // FormJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 292);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvJournal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormJournal";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Журнал карты";
            this.Load += new System.EventHandler(this.FormJournal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvJournal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbNumCard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbLinkCard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnYestoday;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUnload;
    }
}