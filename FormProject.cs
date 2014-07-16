using Alternative.Model;
using CoreCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alternative
{
    public partial class FrmProject : Form
    {
        #region Constructors

        public FrmProject(Project pr)
        {
            InitializeComponent();

            if (pr == null)
            {
                this.Text = CommonText.PrNewForm;
                btnCreate.Text = CommonText.PrBtnSave;
                _pr = new Project();
            }
            else
            {
                this.Text = String.Format(CommonText.PrEditForm, pr.Name);
                btnCreate.Text = CommonText.PrBtnUpdate;
                _pr = pr;
            }
            FillForm();
        }

        #endregion

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try 
	        {
                this.Cursor = Cursors.WaitCursor;

                if (String.IsNullOrEmpty(tbName.Text))
                    throw new EAltMessage(ErrorMsg.EMsgProjectNameEmpty);
                if (String.IsNullOrEmpty(tbNumber.Text))
                    throw new EAltMessage(ErrorMsg.EMsgProjectNumberEmpty);
                if (String.IsNullOrEmpty(cbJuristic.Text))
                    throw new EAltMessage(ErrorMsg.EMsgJuristicEmpty);
                if (String.IsNullOrEmpty(cbPayment.Text))
                    throw new EAltMessage(ErrorMsg.EMsgPaymentEmpty);
                if (String.IsNullOrEmpty(cbDesign.Text))
                    throw new EAltMessage(ErrorMsg.EMsgDesignEmpty);

                
                _pr.Name = tbName.Text;
                _pr.Number = tbNumber.Text;
                _pr.F_Payment = ((ComboBoxItem)cbPayment.SelectedItem).Id;
                _pr.F_Jurictic = ((ComboBoxItem)cbJuristic.SelectedItem).Id;
                _pr.F_Design = ((ComboBoxItem)cbDesign.SelectedItem).Id;

                if (_pr.LINK == 0)
                    _pr.Save();
                else
                    _pr.Update();

                this.DialogResult = DialogResult.OK;
	        }
	        finally
	        {
                this.Cursor = Cursors.Default;
	        }
            
        }
        private void FrmProject_Resize(object sender, EventArgs e)
        {
            pbFacede.Width = this.Width / 2 - pbFacede.Left - 31;
            pbOutside.Left = pbFacede.Width + 24;
            pbOutside.Width = pbFacede.Width;
        }
        private void cbDesign_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem cbx = cbDesign.Items[cbDesign.SelectedIndex] as ComboBoxItem;
            if (cbDesign.Items.Count > 0 && cbx != null)
                GetPictureForDesign(cbx.Id);
        }
        private void tb_TextChanged(object sender, EventArgs e)
        {
            ControlEnabled();
        }
        private void pbFacede_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ControlEnabled();
        }

        #region Helper

        private void FillForm()
        {
            tbName.Text = _pr.Name;
            tbNumber.Text = _pr.Number;
            _pr.Payment.Open();
            _pr.Design.Open();
            _pr.Juristic.Open();
            
            FillComboBox(cbDesign, (from DataRow dataRow in _pr.Design.DT.Rows
                                    select new ComboBoxItem(
                                          Convert.ToInt32(dataRow["LINK"], CultureInfo.InvariantCulture),
                                          String.Format(CultureInfo.InvariantCulture, "{0}", dataRow["S_NAME"].ToString()
                                      ))).ToArray<ComboBoxItem>());
            string[] cbDesignItems = (from ComboBoxItem cbi in cbDesign.Items where cbi.Id == _pr.F_Design select cbi.Name).ToArray();
            if (cbDesignItems.Count() > 0)
            { 
                cbDesign.SelectedIndex = cbDesign.FindString(cbDesignItems[0]);

                // Подгружаются картинки
                cbDesign_SelectedIndexChanged(null, null);
            }              

            
            FillComboBox(cbJuristic, (from DataRow dataRow in _pr.Juristic.DT.Rows
                                      select new ComboBoxItem(
                                            Convert.ToInt32(dataRow["LINK"], CultureInfo.InvariantCulture),
                                            String.Format(CultureInfo.InvariantCulture, "{0}", dataRow["S_NAME"].ToString()
                                        ))).ToArray<ComboBoxItem>());
            string[] cbJuristicItem = (from ComboBoxItem cbi in cbJuristic.Items where cbi.Id == _pr.F_Jurictic select cbi.Name).ToArray();
            if (cbJuristicItem.Count() > 0)
                cbJuristic.SelectedIndex = cbJuristic.FindString(cbJuristicItem[0]);
            
            FillComboBox(cbPayment, (from DataRow dataRow in _pr.Payment.DT.Rows
                                     select new ComboBoxItem(
                                           Convert.ToInt32(dataRow["LINK"], CultureInfo.InvariantCulture),
                                           String.Format(CultureInfo.InvariantCulture, "{0}", dataRow["S_NAME"].ToString()
                                       ))).ToArray<ComboBoxItem>());
            string[] cbPaymentItems = (from ComboBoxItem cbi in cbPayment.Items where cbi.Id == _pr.F_Payment select cbi.Name).ToArray();
            if (cbPaymentItems.Count() > 0)
                cbPayment.SelectedIndex = cbPayment.FindString(cbPaymentItems[0]);
        }
        private static void FillComboBox(ComboBox comboBox, ComboBoxItem[] comboBoxItemArray)
        {
            comboBox.DisplayMember = "Name";
            comboBox.Items.Clear();
            comboBox.Items.AddRange(comboBoxItemArray);
            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }
        private void GetPictureForDesign(int id)
        {
            Design ds = Design.LoadById(id);
            pbFacede.Image = ds.Facade;
            pbOutside.Image = ds.Outside;
        }
        private void ControlEnabled()
        {
            bool b1 = !String.IsNullOrEmpty(tbName.Text);
            bool b2 = !String.IsNullOrEmpty(cbJuristic.Text);
            bool b3 = !String.IsNullOrEmpty(tbNumber.Text);
            bool b4 = !String.IsNullOrEmpty(cbPayment.Text);
            bool b5 = !String.IsNullOrEmpty(cbDesign.Text);
            bool b6 = pbFacede.Image != null;
            bool b7 = pbOutside.Image != null;
            btnCreate.Enabled = b1 && b2 && b3 && b4 && b5 && b6 && b7;
        }
 

        #endregion

        private Project _pr;

    }
}
