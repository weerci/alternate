using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using CoreCommon;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Alternative.DB;

namespace Alternative
{
    [Serializable]
    public partial class FormConnect : Form
    {
        public FormConnect()
        {
            InitializeComponent();
        }

    #region События формы

        private void frmConnect_Load(object sender, EventArgs e)
        {
            LoadSavedLogins();
            EnabledControl();
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ConnectGate.Connect(tbLogin.Text.Trim(), tbPassword.Text.Trim());
                this.DialogResult = DialogResult.OK;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void FormConnect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnConnect_Click(null, null);
            else if (e.KeyCode == Keys.F1)
                CoreCommon.Messages.GetHelp("MF_ID_CONNECT", HelpNavigator.Topic);
        }
        private void tbTextChanged(object sender, EventArgs e)
        {
            EnabledControl();
        }
        
    #endregion

    #region Private
        
        private SaveList _listLogins;
        private void EnabledControl()
        {
            bool b1 = !String.IsNullOrEmpty(tbLogin.Text.Trim());
            bool b2 = !String.IsNullOrEmpty(tbPassword.Text.Trim());

            btnConnect.Enabled = b1 && b2;
        }
        private void LoadSavedLogins()
        {
            _listLogins = (SaveList)CoreSerialize.Deserialize(Path.GetDirectoryName(Application.ExecutablePath) + "\\users.dmp");

            if (_listLogins != null)
                foreach (SaveItem si in _listLogins.ListItems)
                    cmsLogins.Items.Add(si.Name);
        }
        private void cmsLogins_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tbLogin.Text = e.ClickedItem.Text;
            if (tbLogin.CanFocus) tbLogin.Focus();
        }
    #endregion


    }
}
