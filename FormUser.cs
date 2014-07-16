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
    public partial class FormUser : Form
    {
        public FormUser(int id)
        {
            InitializeComponent();

            if (id == 0)
            {
                this.Text = CommonText.UserFormHeaderCreate;
                btnCreate.Text = CommonText.PrBtnSave;
            }
            else
            {
                _user = User.LoadById(id);
                tbLogin.Text = _user.S_NAME;
                tbPrefix.Text = _user.PREF;

                this.Text = CommonText.UserFormHeaderEdit;
                btnCreate.Text = CommonText.PrBtnUpdate;
            }
            FillForm();
        }

        private void ControlEnabled()
        {
            bool b1 = !String.IsNullOrEmpty(tbLogin.Text);
            bool b2 = !String.IsNullOrEmpty(tbPrefix.Text);

            btnCreate.Enabled = b1 && b2;
        }
        private void tb_TextChanged(object sender, EventArgs e)
        {
            ControlEnabled();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                _user.S_NAME = tbLogin.Text;
                _user.S_PASS = tbPassword.Text;
                _user.PREF = tbPrefix.Text;
                _user.F_LOGIN = ((ComboBoxItem)cbLogins.SelectedItem).Id;

                int res = _user.Save();

                if (res != _user.LINK) // Форма была открыта в режиме создания
                {
                    tbLogin.Text = "";
                    tbPassword.Text = "";
                    tbPrefix.Text = "";
                    _user = new User() { LINK = 0 };
                }
                else
                    this.Close();
            

                // Обновляется справочник дизайнов
                if (OnCreateUserEvent != null)
                    OnCreateUserEvent(null, new CreateUserEventArgs(res));
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FormUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnCreate_Click(null, null);
        }

        #region Fields
        
        private User _user = new User();
        public event CreateUserEventHandler OnCreateUserEvent;
        public delegate void CreateUserEventHandler(object sender, CreateUserEventArgs e);
        private static void FillComboBox(ComboBox comboBox, ComboBoxItem[] comboBoxItemArray)
        {
            comboBox.DisplayMember = "Name";
            comboBox.Items.Clear();
            comboBox.Items.AddRange(comboBoxItemArray);
            if (comboBox.Items.Count > 0)
                comboBox.SelectedIndex = 0;
        }
        private void FillForm()
        {
            _user.LOGINS.Open();

            FillComboBox(cbLogins, (from DataRow dataRow in _user.LOGINS.DT.Rows
                                    select new ComboBoxItem(
                                          Convert.ToInt32(dataRow["LINK"], CultureInfo.InvariantCulture),
                                          String.Format(CultureInfo.InvariantCulture, "{0}", dataRow["S_NAME"].ToString()
                                      ))).ToArray<ComboBoxItem>());
            string[] cbLoginsItems = (from ComboBoxItem cbi in cbLogins.Items where cbi.Id == _user.F_LOGIN select cbi.Name).ToArray();
            if (cbLoginsItems.Count() > 0)
                cbLogins.SelectedIndex = cbLogins.FindString(cbLoginsItems[0]);
        }

        #endregion

    }

    public class CreateUserEventArgs
    {
        public CreateUserEventArgs(int insIndex) { Index = insIndex; }
        public int Index { get; private set; } // readonly
    }
}
