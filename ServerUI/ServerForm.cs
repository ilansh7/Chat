using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

using Protocols;
using ServerLogic;

namespace ServerUI
{
    public partial class ServerForm : Form
    {
        #region Properties

        private LoginForm _serverLoginForm;
        private Server _server;
        private ListViewColumnSorter lvwColumnSorter;
        public ImageList imagelist;

        #endregion

        #region c'tor

        public ServerForm()
        {
            InitializeComponent();
            imagelist = new ImageList();
            Bitmap isActiveImg = new Bitmap(@"..\..\images\Connected.png");
            imagelist.Images.Add(isActiveImg);

            _serverLoginForm = new LoginForm();
            _serverLoginForm.ShowDialog();

            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();
            this.lvUsersList.ListViewItemSorter = lvwColumnSorter;

            if (_serverLoginForm.DialogResult == DialogResult.OK)
            {
                //MessageBox.Show(servLogin.txtIPAddress.Text);
                _server = new Server(IPAddress.Parse(_serverLoginForm.txtIPAddress.Text),
                            (int)_serverLoginForm.txtPort.Value);
                //_server = tmpServer;
                _server.operationMsg += WriteOperationMsg;
                _server.writeConnections += AddClientToList;
                _server.Connect();

                //dtFromDate.ResetText();
                dtFromDate.Value = DateTime.Parse("01/01/2015");
                dtToDate.Value = DateTime.Now;
                InitiateMessageTab();
                //InitiateUsersTab();
            }

            if (_serverLoginForm.DialogResult == DialogResult.Cancel)
            {
                Dispose();
            }
        }

        #endregion

        #region Actions

        public void WriteOperationMsg(MessageClass msg)
        {
            if (InvokeRequired)
            {
                Invoke(new BroadcastingStrDlg(WriteOperationMsg), msg);
                return;
            }
            
            if (msg is OperationMessage)
            {
                OperationMessage connectionMessage = msg as OperationMessage;
                //REMOVE CLOSED CLIENT !!!!!
                if (connectionMessage.IsAlive)
                {
                    var li = new ListViewItem(string.Format("[{0}:{1}] Connected at {2}.", connectionMessage.Sender, null, DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
                    lvHistory.Items.Add(li);
                    lvHistory.Items[lvHistory.Items.Count - 1].EnsureVisible();
                }
                else
                {
                    var li = new ListViewItem(string.Format("[{0}:{1}] Disconnected at {2}.", connectionMessage.Sender, null, DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
                    lvHistory.Items.Add(li);
                    lvHistory.Items[lvHistory.Items.Count - 1].EnsureVisible();
                }

                RefreshUsersList();
                AddClientToList();
            }

            if (msg is ChatMessage)
            {
                ChatMessage txtMessage = msg as ChatMessage;
                Text = txtMessage.Message;
            }
        }

        public void AddClientToList()
        {
            if (InvokeRequired)
            {
                Invoke(new WriteListDlg(AddClientToList));
                return;
            }
            lvConnections.Items.Clear();
            foreach (var client in _server._clientsList)
            {
                var listitem = new ListViewItem(client.GetName());
                listitem.SubItems.Add("Connected");
                listitem.SubItems.Add(client._connected.ToString("dd/MM/yyyy HH:mm"));
                lvConnections.Items.Add(listitem);
            }
            //ConnectionMessage cm = new ConnectionMessage(;

            //_server.BroadcastMessage(connectionMessage);

        }

        #endregion

        #region Users Methods

        //public void InitiateUsersTab()
        //{
        //    cbUserLogin.DataSource = _server.userLogic.GetUsersList(true);
        //    cbUserLogin.DisplayMember = "LoginName";
        //    cbUserLogin.ValueMember = "UserId";
        //}

        private void RefreshUsersList()
        {
            int userId = (string.IsNullOrEmpty(txtUserId.Text)) ? 0 : int.Parse(txtUserId.Text);
            string userLogin = (string.IsNullOrEmpty(txtUserLogin.Text)) ? "" : txtUserLogin.Text;

            int IsAliveIndex = 0;
            //DataTable dt = _server.userLogic.GetConnectedUsers();
            DataTable dt = _server.userLogic.GetUsersData(userId, userLogin);
            int fc = dt.Columns.Count;
            //DataSet ds = dt.DataSet;
            lvUsersList.Items.Clear();
            //ListViewItem item;
            foreach (DataRow row in dt.Rows)
            {
                string[] subitems = new string[fc];

                object[] o = row.ItemArray;
                ListViewItem item = new ListViewItem("");
                if (o[IsAliveIndex].ToString().ToLower() == "true")
                    item.ImageIndex = 0;
                    
                for (int i = 1; i < fc; i++)
                {
                    item.SubItems.Add(o[i].ToString());
                }

                //ListViewItem item = new ListViewItem(subitems);
                lvUsersList.LargeImageList = imagelist;
                lvUsersList.SmallImageList = imagelist;
                lvUsersList.Items.Add(item);

                //lvUsersList.Items.Add(new ListViewItem() { ImageIndex = 0 });
            }

            //lvUsersList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            //dt = GetConnectedUsers();
            ////dt.Columns.Add("Icon", typeof(byte[]));
            ////Image img = Image.FromFile(@"..\..\images\Connected.png");
            ////DataRow dr = dt.NewRow();
            ////dr["Icon"] = imageToByteArray(img);
            ////dt.Rows.Add(dr);
            //////DataColumn dc = new DataColumn("Icon", );
            ////////dt.Columns.Add(("UserId");
            //gvUsersList.DataSource = dt;
            ////foreach (DataRow item in gvUsersList.DataSource)
            //{
                
            //}
        }
        
        private void lvUsersList_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lvUsersList.Sort();

        }

        private void lvUsersList_Click(object sender, EventArgs e)
        {
            if (lvUsersList.Items.Count > 0)
            {
                //lvUsersList.Items[0].Selected = true;
                lvUsersList.Select();
                //lvUsersList.HideSelection = false;
                ListViewItem item = lvUsersList.SelectedItems[0];
                txtSelectedUserId.Text = item.SubItems[1].Text;
                txtSelectedUserName.Text = item.SubItems[2].Text;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshUsersList();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSelectedUserName.Text))
            {
                MessageBox.Show("User must be selected.",  "Selct User", MessageBoxButtons.OK);
            }
            else
            {
                DialogResult result = MessageBox.Show("Do you want to delete user " + txtSelectedUserName.Text + " ?", "Confirmation", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    DeleteUser(txtSelectedUserId.Text);
                }
            }
            txtSelectedUserName.Text = "";
            txtSelectedUserId.Text = "";
        }
        //private DataTable GetConnectedUsers()
        //{
        //    return _server.userLogic.GetConnectedUsers();
        //}

        private void DeleteUser(string userId)
        {
            //deleteBook(bid); //delete the record from the Book table
            //gvMessages.Rows.RemoveAt(e.RowIndex); //delete the row from the DataGridView
            _server.RemoveClientById(int.Parse(userId));
            _server.userLogic.DeleteUser(int.Parse(userId));
            RefreshUsersList();
            InitiateMessageTab();
        }

        #endregion

        #region Messages Methods

        private void InitiateMessageTab()
        {
            cbSender.DataSource = _server.userLogic.GetUsersList(true);
            cbSender.DisplayMember = "LoginName";
            cbSender.ValueMember = "UserId";
            cbReceiver.DataSource = _server.userLogic.GetUsersList(true);
            cbReceiver.DisplayMember = "LoginName";
            cbReceiver.ValueMember = "UserId";
        }

        private void btnSearchMsg_Click(object sender, EventArgs e)
        {
            gvMessages.ClearSelection();
            int senderUser = (string.IsNullOrEmpty(cbSender.SelectedValue.ToString())) ? 0 : int.Parse(cbSender.SelectedValue.ToString());
            int receiver = (string.IsNullOrEmpty(cbReceiver.SelectedValue.ToString())) ? 0 : int.Parse(cbReceiver.SelectedValue.ToString());
            gvMessages.DataSource = GetMessages(senderUser, receiver, dtFromDate.Value, dtToDate.Value, txtKeywords.Text);
            this.tabMessages.Focus();
        }

        private DataTable GetMessages(int sender, int receiver, DateTime fromDate, DateTime toDate, string keywords)
        {
            //gvMessages.DataSource = null;
            return _server.chatLogic.GetMessages(sender, receiver, fromDate, toDate, keywords);
        }

        #endregion

        #region Form Methods

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }


       

        #endregion

        #region Controls

        #endregion

        #region d'tor

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Saving history to ile
            string date = DateTime.Now.Year.ToString() +
                ((DateTime.Now.Month.ToString().Length == 1) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString()) +
                ((DateTime.Now.Day.ToString().Length == 1) ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString());

            string fileName = @"..\..\..\ServLog_" + date + ".txt";
            FileStream fs = new FileStream(fileName, FileMode.Append);
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine(string.Format("Server History for {0}", DateTime.Now.ToString()));
            writer.WriteLine("======================================");
            foreach (var item in lvHistory.Items)
            {
                writer.WriteLine(item.ToString());
            }
            writer.WriteLine("\n");
            writer.Close();
            fs.Close();

            DropServer();
        }

        private void DropServer()
        {
            if (_server != null)
            {
                _server.Dispose();
                _server = null; 
                this.Dispose();
            }
        }

        #endregion

        


    }
}
