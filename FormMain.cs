using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreCommon;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;
using Alternative.Model;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using Alternative.DB;
using System.Management;

namespace Alternative
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        #region Обработка событий формы

         private void FormMain_Load(object sender, EventArgs e)
        {
            dgvDesign.EditingControlShowing += dgvDesign_EditingControlShowing;
            dgvPyment.EditingControlShowing += dgvDesign_EditingControlShowing;
            dgvJuristic.EditingControlShowing += dgvDesign_EditingControlShowing;
            dgvStatus.EditingControlShowing += dgvDesign_EditingControlShowing;
            dgvOperation.EditingControlShowing += dgvDesign_EditingControlShowing;
            dgvUsers.EditingControlShowing += dgvDesign_EditingControlShowing;                 
            
            // Текущая коннекция
            tsslServer.Text = WFSql.DB.ServerName;
            tsslBase.Text = WFSql.DB.DbName;
            tsslUser.Text = WFSql.DB.UserLogin;
            
            //TODO Загрузка таблицы разрещений

            //    tcMain.TabPages.Remove(tpAdmin);

        }
        private void dgvDesign_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dg = sender as DataGridView;
            if (dg != null && dg.CurrentCell.ColumnIndex != 0)
            {
                TextBox tb = (TextBox)e.Control;
                tb.TextChanged += new EventHandler(tb_TextChanged);
            }
        }
        private void tb_TextChanged(object sender, EventArgs e)
        {
            EnabledDictionarySaveCancelButton(true);
        }
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
        private void tsmiClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tsmiHelpHelp_Click(object sender, EventArgs e)
        {
            Messages.GetHelp("Introduction", HelpNavigator.Topic);
        }
        private void tvAdminTasks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (e.Node.Name)
                {
                    case "ndKey":
                        tcAdminTasks.SelectTab("tbCreateKeys");
                        break;
                    case "ndDictionary":
                        tcAdminTasks.SelectTab("tbDictionaries");
                        new Dictionary(DicType.DESIGN).Open(dgvDesign);
                        ((DataTable)dgvDesign.DataSource).RowDeleted += FormMain_RowDeleted;
                        break;
                    case "ndProject":
                        tcAdminTasks.SelectTab("tbProjects");
                        Project.List(dgvProjects);
                        break;
                    default:
                        break;
                }

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab.Name == "tpAdmin")
            {
                tvAdminTasks.SelectedNode = tvAdminTasks.Nodes["ndProject"];
                Project.List(dgvProjects);
            }
        }

        #endregion 

        #region Вкладка Администрирование ключей

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Messages.GetHelp("Introduction", HelpNavigator.Topic);
        }
        private void btnNewLoginKey_Click_1(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //try
            //{
            //    if (MessageBox.Show(CommonText.MsgDeleteLoginKey, CommonText.CapDeleteProject, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //    {
            //        string s = Security.GetSelfHash();
            //        ConnectGate.NewConnectKey(WFSql.DB.ServerName, WFSql.DB.DbName, "login", 
            //            s.Substring(Convert.ToInt32(nudVI.Value, CultureInfo.InvariantCulture), 16),
            //            s.Substring(Convert.ToInt32(nudKey.Value, CultureInfo.InvariantCulture), 16), 
            //            nudVI.Value, nudKey.Value);
            //        MessageBox.Show(CommonText.MsgChangedLoginKey, CommonText.CapDeleteProject, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //finally
            //{
            //    this.Cursor = Cursors.Default;
            //}
        }
        private void btnCurHashKey_Click(object sender, EventArgs e)
        {
            //tbLoginKey.Text = ConnectGate.LoadConnectKey();
        }

        #endregion Администрирование ключей

        #region Работа со справочниками

        private void tcDictionaries_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcDictionaries.SelectedIndex)
            {
                case 0:
                    new Dictionary(DicType.DESIGN).Open(dgvDesign);
                    break;
                case 1:
                    new Dictionary(DicType.PAYMENTS).Open(dgvPyment);
                    ((DataTable)dgvPyment.DataSource).RowDeleted += FormMain_RowDeleted;
                    break;
                case 2:
                    new Dictionary(DicType.JURISTIC).Open(dgvJuristic);
                    ((DataTable)dgvJuristic.DataSource).RowDeleted += FormMain_RowDeleted;
                    break;
                case 3:
                    new Dictionary(DicType.STATUS).Open(dgvStatus);
                    ((DataTable)dgvStatus.DataSource).RowDeleted += FormMain_RowDeleted;
                    break;
                case 4:
                    new Dictionary(DicType.OPERATION).Open(dgvOperation);
                    ((DataTable)dgvOperation.DataSource).RowDeleted += FormMain_RowDeleted;
                    break;
                case 5:
                    new Dictionary(DicType.USERS).Open(dgvUsers);
                    break;
                default:
                    break;
            }
            EnabledDictionarySaveCancelButton(false);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (tcDictionaries.SelectedIndex)
                {
                    case 1:
                        Dictionary.DeletePayment((DataTable)dgvPyment.DataSource);
                        Dictionary.InsertPayment((DataTable)dgvPyment.DataSource);
                        Dictionary.UpdatePayment((DataTable)dgvPyment.DataSource);
                        ((DataTable)dgvPyment.DataSource).AcceptChanges();
                        EnabledDictionarySaveCancelButton(false);
                        break;
                    case 2:
                        Dictionary.DeleteJuristic((DataTable)dgvJuristic.DataSource);
                        Dictionary.InsertJuristic((DataTable)dgvJuristic.DataSource);
                        Dictionary.UpdateJuristic((DataTable)dgvJuristic.DataSource);
                        ((DataTable)dgvJuristic.DataSource).AcceptChanges();
                        EnabledDictionarySaveCancelButton(false);
                        break;
                    case 3:
                        Dictionary.DeleteStatus((DataTable)dgvStatus.DataSource);
                        Dictionary.InsertStatus((DataTable)dgvStatus.DataSource);
                        Dictionary.UpdateStatus((DataTable)dgvStatus.DataSource);
                        ((DataTable)dgvStatus.DataSource).AcceptChanges();
                        EnabledDictionarySaveCancelButton(false);
                        break;
                    case 4:
                        Dictionary.DeleteOperation((DataTable)dgvOperation.DataSource);
                        Dictionary.InsertOperation((DataTable)dgvOperation.DataSource);
                        Dictionary.UpdateOperation((DataTable)dgvOperation.DataSource);
                        ((DataTable)dgvOperation.DataSource).AcceptChanges();
                        EnabledDictionarySaveCancelButton(false);
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void tsbDesignCreate_Click(object sender, EventArgs e)
        {
            FormDesign fd = new FormDesign(0);
            fd.OnCreateDesignEvent += fd_OnCreateDesignEvent;
            fd.ShowDialog();
        }
        private void dgvDesign_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDesign.SelectedRows.Count > 0)
            { 
                FormDesign fd = new FormDesign(Convert.ToInt32(dgvDesign.SelectedRows[0].Cells["LINK"].Value, CultureInfo.InvariantCulture));
                fd.OnCreateDesignEvent += fd_OnCreateDesignEvent;
                fd.ShowDialog();
            }
        }
        void fd_OnCreateDesignEvent(object sender, CreateDesignEventArgs e)
        {
            new Dictionary(DicType.DESIGN).Open(dgvDesign);
            DataTable dt = dgvDesign.DataSource as DataTable;
            if (dt != null && e != null)
            {
                dt.DefaultView.Sort = "LINK";
                int idx = dt.DefaultView.Find(e.Index);
                if (idx != -1)
                    dgvDesign.CurrentCell = dgvDesign.Rows[idx].Cells[dgvDesign.FirstDisplayedCell.ColumnIndex];
            }
        }
        private void tsbDesignDelete_Click(object sender, EventArgs e)
        {
            if (dgvDesign.SelectedRows.Count > 0)
                if (MessageBox.Show(CommonText.MsgDictionaryElementDel, CommonText.ObjectDel, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { 
                    Design.Delete(dgvDesign.SelectedRows.Cast<DataGridViewRow>().Select(n=>Convert.ToInt32(n.Cells["LINK"].Value, CultureInfo.InvariantCulture)).ToArray());
                    new Dictionary(DicType.DESIGN).Open(dgvDesign);
                }
        }
        private void dgvDesign_SelectionChanged(object sender, EventArgs e)
        {
            bool b1 = dgvDesign.SelectedRows.Count > 0;
            tsbDesignEdit.Enabled = b1;
            tsbDesignDelete.Enabled = b1;
        }
        private void tsbUserCreate_Click(object sender, EventArgs e)
        {
            FormUser fd = new FormUser(0);
            fd.OnCreateUserEvent += fd_OnCreateUserEvent;
            fd.ShowDialog();
        }
        private void fd_OnCreateUserEvent(object sender, CreateUserEventArgs e)
        {
            new Dictionary(DicType.USERS).Open(dgvUsers);
            DataTable dt = dgvUsers.DataSource as DataTable;
            if (dt != null && e != null)
            {
                dt.DefaultView.Sort = "LINK";
                int idx = dt.DefaultView.Find(e.Index);
                if (idx != -1)
                    dgvUsers.CurrentCell = dgvUsers.Rows[idx].Cells[dgvUsers.FirstDisplayedCell.ColumnIndex];
            }

        }
        private void tsbUserEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                FormUser fd = new FormUser(Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["LINK"].Value, CultureInfo.InvariantCulture));
                fd.OnCreateUserEvent += fd_OnCreateUserEvent;
                fd.ShowDialog();
            }
        }
        private void tsbUserDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
                if (MessageBox.Show(CommonText.MsgDictionaryElementDel, CommonText.ObjectDel, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    User.Delete(dgvUsers.SelectedRows.Cast<DataGridViewRow>().Select(n => Convert.ToInt32(n.Cells["LINK"].Value, CultureInfo.InvariantCulture)).ToArray());
                    new Dictionary(DicType.USERS).Open(dgvUsers);
                }
        }
        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            bool b1 = dgvUsers.SelectedRows.Count > 0;
            tsbUserEdit.Enabled = b1;
            tsbUserDelete.Enabled = b1;
        }
        private void dgvDictionary_Validating(object sender, CancelEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv != null)
            {
                string[] str = new string[] { "dgvDesign", "dgvPyment", "dgvJuristic", "dgvStatus", "dgvOperation", "dgvUsers" };
                if (str.Any(n => n == dgv.Name) && btnSave.Enabled && !OnSaveCancel())
                    if (MessageBox.Show(CommonText.MsgUnsaveData, CommonText.CapMsgUnsaveData, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                        e.Cancel = true;
            }
        }
        private bool OnSaveCancel()
        {
            Point cursorPoint = Cursor.Position;
            Point lSave = btnSave.PointToScreen(Point.Empty);
            Point lCancel = btnCancel.PointToScreen(Point.Empty);
            bool onSave = cursorPoint.X > lSave.X && cursorPoint.X < lSave.X + btnSave.Width && cursorPoint.Y > lSave.Y && cursorPoint.Y < lSave.Y + btnSave.Height;
            bool onCancel = cursorPoint.X > lCancel.X && cursorPoint.X < lCancel.X + btnSave.Width && cursorPoint.Y > lCancel.Y && cursorPoint.Y < lCancel.Y + btnSave.Height;
            return onSave || onCancel;
        }

        #endregion

        #region Работа с проектами

        private void tsbCreateProject_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (new FrmProject(null).ShowDialog() == DialogResult.OK)
                    Project.List(dgvProjects);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void tsbDelProject_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (MessageBox.Show(CommonText.MsgDeleteProject, CommonText.CapDeleteProject, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Project.Delete(dgvProjects);
                    Project.List(dgvProjects);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void tsbEditProject_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dgvProjects.SelectedRows.Count > 0)
                {
                    if (new FrmProject(Project.Load(Convert.ToInt64(dgvProjects.SelectedRows[0].Cells["LINK"].Value))).ShowDialog() == DialogResult.OK)
                        Project.List(dgvProjects);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void tsbLoadCards_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (dgvProjects.SelectedRows.Count > 0)
                {
                    FormLoadCards flc = new FormLoadCards(Project.Load(Convert.ToInt64(dgvProjects.SelectedRows[0].Cells["LINK"].Value)));
                    flc.OnLoadCardEvent += dgvProjects_SelectionChanged;
                    flc.ShowDialog();
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void tsbCardUnLoad_Click(object sender, EventArgs e)
        {
            if (dgvProjects.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(CommonText.MsgCardOfProjectDel, CommonText.ObjectDel, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        Int64 pr = Convert.ToInt64(dgvProjects.SelectedRows[0].Cells["LINK"].Value);
                        Card.CardUnload(pr);
                        Card.List(pr, dgvCards);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
        private void tsbCardUnloadToPress_Click(object sender, EventArgs e)
        {
            if (dgvProjects.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(CommonText.MsgCardUloadToPress, CommonText.ObjectDel, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        Int64 pr = Convert.ToInt64(dgvProjects.SelectedRows[0].Cells["LINK"].Value);
                        int idx = dgvProjects.SelectedRows[0].Index;
                        Card.CardUnloadToPress(pr);

                        //Перегрузить проекты и спозиционировать курсор на выбранном
                        Project.List(dgvProjects);
                        dgvProjects.CurrentCell = dgvProjects.Rows[idx].Cells[dgvProjects.FirstDisplayedCell.ColumnIndex];
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
        private void dgvProjects_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProjects.SelectedRows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    Int64 pr = Convert.ToInt64(dgvProjects.SelectedRows[0].Cells["LINK"].Value, CultureInfo.InvariantCulture);
                    Card.List(pr, dgvCards);
                    ControlProjectEnabled();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        private void ControlProjectEnabled()
        {
            bool b1 = dgvProjects.SelectedRows[0].Cells["LABEL"].Value.ToString() == "Выгружен в типографию";
            bool b2 = dgvCards.SelectedRows.Count > 0;
            bool b3 = dgvProjects.SelectedRows.Count > 0;

            tsbCardUnLoad.Enabled = b2 && !b1;
            tsbEditProject.Enabled = b3 && !b1;
            tsbDelProject.Enabled = b3 && !b1;
            tsbLoadCards.Enabled = b3 && !b1;
            tsbCardUnloadToPress.Enabled = b2 && !b1;
        }
        private void dgvCards_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dgvCards.SelectedRows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                try 
	            {
                    Card card = Card.Load(Convert.ToInt64(dgvCards.SelectedRows[0].Cells["LINK"].Value, CultureInfo.InvariantCulture));
                    new FormJournal(card).ShowDialog();
	            }
	            finally
	            {
                    this.Cursor = Cursors.Default;
	            }
            }
        }
        private void dgvProjects_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           tsbEditProject_Click(null, null);
        }

        #endregion

        #region Работа с карточками
        
        private void btnFind_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int cardNumber = String.IsNullOrEmpty(tbCardNumber.Text.Trim()) ? -1 : Convert.ToInt32(tbCardNumber.Text, CultureInfo.InvariantCulture);
                double d_face = String.IsNullOrEmpty(tbCardFace.Text.Trim()) ? -1 : Convert.ToDouble(tbCardFace.Text, CultureInfo.InvariantCulture);

                Card.Find(tbProjectNumber.Text, cardNumber, d_face, tbPinCode.Text, dgvFindCards);
                enabledPay();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void tbCardFace_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift || Routins.IsNotDecimalKey(e.KeyCode))
            {
                e.SuppressKeyPress = true;
                Messages.ShowTip(tbCardNumber, CommonText.EMsgCheckValue, ErrorMsg.EMsgDigitOnly, ToolTipIcon.Error, 1200);
            }
        }
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (tcMain.SelectedIndex == 0)
            { 
                if (e.KeyCode == Keys.Enter)
                    btnFind_Click(null, null);
            }
        }
        private void tbCardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift || Routins.IsNotNumberKey(e.KeyCode))
            {
                e.SuppressKeyPress = true;
                Messages.ShowTip(tbCardNumber, CommonText.EMsgCheckValue, ErrorMsg.EMsgDigitOnly, ToolTipIcon.Error, 1200);
            }
        }
        private void dgvFindCards_DoubleClick(object sender, EventArgs e)
        {
            if (dgvFindCards.SelectedRows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    Card card = Card.Load(Convert.ToInt64(dgvFindCards.SelectedRows[0].Cells["LINK"].Value, CultureInfo.InvariantCulture));
                    new FormJournal(card).ShowDialog();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        private void btnAction_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                Card card = Card.Load(Convert.ToInt64(dgvFindCards.SelectedRows[0].Cells["LINK"].Value, CultureInfo.InvariantCulture));
                new FormPay(card, tbPinCode.Text).ShowDialog();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }
        private void enabledPay()
        {
            bool b1 = dgvFindCards.SelectedRows.Count > 0;
            btnPay.Enabled = b1;
        }

        #endregion

        #region Helper

        private void EnabledDictionarySaveCancelButton(bool flag)
        {
            btnSave.Enabled = flag;
            btnCancel.Enabled = flag;
        }
        private void FormMain_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            EnabledDictionarySaveCancelButton(true);
        }
        private void ClearMainTcUser()
        {
            tbProjectNumber.Text = "";
            tbCardNumber.Text = "";
            tbCardFace.Text = "";
            tbPinCode.Text = "";
            if (dgvFindCards.DataSource != null)
            {
                dgvFindCards.DataSource = null;
                dgvFindCards.Invalidate();
            }
            
        }
        
        #endregion

        private void tsmiReconnect_Click(object sender, EventArgs e)
        {
//            ManagementObjectSearcher _sysInfoSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0}", "Win32_OperatingSystem"));
            ManagementObjectSearcher _sysInfoSearcher = new ManagementObjectSearcher(string.Format("SELECT * FROM {0}", "Win32_SystemDevices"));
            List<string> ls = new List<string>();

            foreach (ManagementObject managementObject in _sysInfoSearcher.Get())
            {
                foreach (PropertyData prop in managementObject.Properties)
                    ls.Add(String.Format("{0}: {1}", prop.Name, prop.Value));
            }
            File.WriteAllText(@"c:\temp\sys.txt", ls.Aggregate((workingSentence, next) => next + Environment.NewLine + workingSentence));
        }


    }

    public class SysInfoResult
    {
        private readonly string _name;
        private readonly List<string> _nodes = new List<string>();
        private readonly List<SysInfoResult> _childResults = new List<SysInfoResult>();

        public SysInfoResult(string name)
        {
            _name = name;
        }

        public void AddNode(string node)
        {
            _nodes.Add(node);
        }

        public void AddChildren(IEnumerable<SysInfoResult> children)
        {
            ChildResults.AddRange(children);
        }

        public List<string> Nodes
        {
            get { return _nodes; }
        }

        private void Clear()
        {
            _nodes.Clear();
        }

        private void AddRange(IEnumerable<string> nodes)
        {
            _nodes.AddRange(nodes);
        }

        public string Name
        {
            get { return _name; }
        }

        public List<SysInfoResult> ChildResults
        {
            get { return _childResults; }
        }

        public SysInfoResult Filter(string[] filterStrings)
        {
            var filteredNodes = (from node in ChildResults[0].Nodes
                                 from filter in filterStrings
                                 where node.Contains(filter + " = ")	//TODO a little too primitive
                                 select node).ToList();

            ChildResults[0].Clear();
            ChildResults[0].AddRange(filteredNodes);
            return this;
        }
    }
}
