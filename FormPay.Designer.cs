namespace Alternative
{
    partial class FormPay
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPay));
            this.epFormPay = new System.Windows.Forms.ErrorProvider(this.components);
            this.mtbPhone = new System.Windows.Forms.MaskedTextBox();
            this.btnPay = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mtbPinCode = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mtbAmmount = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCardNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.epFormPay)).BeginInit();
            this.SuspendLayout();
            // 
            // epFormPay
            // 
            this.epFormPay.ContainerControl = this;
            // 
            // mtbPhone
            // 
            this.mtbPhone.Location = new System.Drawing.Point(73, 61);
            this.mtbPhone.Mask = "999-000-00-00";
            this.mtbPhone.Name = "mtbPhone";
            this.mtbPhone.Size = new System.Drawing.Size(159, 20);
            this.mtbPhone.TabIndex = 1;
            this.mtbPhone.Validated += new System.EventHandler(this.mtbPhone_Validated);
            // 
            // btnPay
            // 
            this.btnPay.Enabled = false;
            this.btnPay.Location = new System.Drawing.Point(171, 118);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(75, 23);
            this.btnPay.TabIndex = 3;
            this.btnPay.Text = "Оплатить";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Телефон:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Пин-код:";
            // 
            // mtbPinCode
            // 
            this.mtbPinCode.Location = new System.Drawing.Point(73, 35);
            this.mtbPinCode.Mask = "000000000000";
            this.mtbPinCode.Name = "mtbPinCode";
            this.mtbPinCode.Size = new System.Drawing.Size(159, 20);
            this.mtbPinCode.TabIndex = 0;
            this.mtbPinCode.Validated += new System.EventHandler(this.mtbPinCode_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Сумма:";
            // 
            // mtbAmmount
            // 
            this.mtbAmmount.Location = new System.Drawing.Point(73, 87);
            this.mtbAmmount.Name = "mtbAmmount";
            this.mtbAmmount.Size = new System.Drawing.Size(159, 20);
            this.mtbAmmount.TabIndex = 2;
            this.mtbAmmount.Validated += new System.EventHandler(this.mtbAmmount_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Номер:";
            // 
            // tbCardNumber
            // 
            this.tbCardNumber.Enabled = false;
            this.tbCardNumber.Location = new System.Drawing.Point(73, 9);
            this.tbCardNumber.Name = "tbCardNumber";
            this.tbCardNumber.Size = new System.Drawing.Size(159, 20);
            this.tbCardNumber.TabIndex = 0;
            this.tbCardNumber.Validated += new System.EventHandler(this.tbCardNumber_Validated);
            // 
            // FormPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 153);
            this.Controls.Add(this.tbCardNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mtbAmmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mtbPinCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.mtbPhone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Проведение платежа с карты";
            this.Load += new System.EventHandler(this.FormPay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epFormPay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider epFormPay;
        private System.Windows.Forms.MaskedTextBox mtbPhone;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.TextBox tbCardNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mtbAmmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox mtbPinCode;
        private System.Windows.Forms.Label label1;
    }
}