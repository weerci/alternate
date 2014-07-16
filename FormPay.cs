using Alternative.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alternative
{
    public partial class FormPay : Form
    {
        public FormPay(Card card, string pin)
        {
            InitializeComponent();

            this._card = card;
            this._pin = pin;
        }

        private void FormPay_Load(object sender, EventArgs e)
        {
            tbCardNumber.Text = _card.N_NUMBER.ToString();
            mtbPinCode.Text = _pin;

            epFormPay.SetError(mtbAmmount, Card.ValidAmmount(mtbAmmount.Text));
            epFormPay.SetError(mtbPhone, Card.ValidPhone(mtbPhone.Text));
            mtbPinCode_Validated(null, null);
            tbCardNumber_Validated(null, null);
        }

        #region Fields
        
        private Card _card;
        private string _pin;

        #endregion

        private void mtbPhone_Validated(object sender, EventArgs e)
        {
            epFormPay.SetError(mtbPhone, Card.ValidPhone(mtbPhone.Text));
            enableButtonPay();
        }
        private void tbCardNumber_Validated(object sender, EventArgs e)
        {
            epFormPay.SetError(tbCardNumber, Card.ValidCard(tbCardNumber.Text));
            enableButtonPay();
        }
        private void mtbPinCode_Validated(object sender, EventArgs e)
        {
            epFormPay.SetError(mtbPinCode, Card.ValidPin(mtbPinCode.Text));
            enableButtonPay();
        }
        private void mtbAmmount_Validated(object sender, EventArgs e)
        {
            epFormPay.SetError(mtbAmmount, Card.ValidAmmount(mtbAmmount.Text));
            enableButtonPay();
        }

        private void enableButtonPay()
        {
            btnPay.Enabled = String.IsNullOrEmpty(epFormPay.GetError(mtbPhone)) && String.IsNullOrEmpty(epFormPay.GetError(tbCardNumber))
                && String.IsNullOrEmpty(epFormPay.GetError(mtbPinCode)) && String.IsNullOrEmpty(epFormPay.GetError(mtbAmmount));
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Card.Active(_card.LINK, mtbPinCode.Text, mtbAmmount.Text, mtbPhone.Text.Replace("-", ""));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
