using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

using ClientLogic;
using Protocols;

namespace ClientUI
{
    public partial class ChatForm : Form
    {
        #region Properties

        private Client _client;
        private ChatMessage _broadcastingMessage;
        private ListViewItem lviItem;

        #endregion

        #region c'tor

        public ChatForm()
        {
            InitializeComponent();
            LoginForm clientLogin = new LoginForm();
            clientLogin.ShowDialog();

            if (clientLogin.DialogResult == DialogResult.OK)
            {
                WriteStatus(string.Format("Welcome {0}", clientLogin.txtLoginName.Text));
                _client = new Client(IPAddress.Parse(clientLogin.txtIPAddress.Text),
                                        (int)clientLogin.numPort.Value,
                                        clientLogin.txtLoginName.Text,
                                        Color.FromName(clientLogin.cbColor.Text),
                                        int.Parse(clientLogin.txtUserId.Text));
                _client.writingMessage += WriteMessage;
                _client.terminateClient += DropChatClient;
                _broadcastingMessage = new ChatMessage(clientLogin.txtLoginName.Text,
                                                     Color.FromName(clientLogin.cbColor.Text),
                                                     txtMessage.Text);
                _broadcastingMessage.SenderId = int.Parse(clientLogin.txtUserId.Text);
                _client.Connect();

                if (_client.GetPort() < 0)
                {
                    //Client connection Error
                    //ChatForm_FormClosing(this, new FormClosingEventArgs(CloseReason.UserClosing, false));
                    _client = null;
                    this.Close();
                    //return;
                }
                this.Text = _client.GetName() + " - " + clientLogin.txtIPAddress.Text + ":" + clientLogin.numPort.Value;
                this.BackColor = Color.FromName(clientLogin.cbColor.Text);

            }

            if (clientLogin.DialogResult == DialogResult.Cancel)
            {
                Trace.WriteLine("Client Disconnected");
            }
        }

        #endregion

        #region Actions

        private void WriteStatus(string txt)
        {
            lviItem = new ListViewItem();
            lviItem.SubItems[0].Text = txt;
            lviItem.SubItems[0].Font = new Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lvClient.Items.Add(lviItem);
        }

        //write message to chat form
        private void WriteMessage(MessageClass msg)
        {
            if (InvokeRequired)
            {
                Invoke(new WritingMessageDlg(WriteMessage), msg);
                return;
            }

            if (msg is ChatMessage)
            {
                string sender = "";
                ChatMessage cm = msg as ChatMessage;
                //Trace.WriteLine(string.Format("{0} Wrote : {1}", cm.Sender, cm.Message));

                //private message - wrong recipient
                if ((!string.IsNullOrEmpty(cm.Receiver)) && (cm.Receiver != _client.GetName()) && (cm.Sender != _client.GetName()))
                    return;
                //marking message as private
                if (!string.IsNullOrEmpty(cm.Receiver) && cm.Sender != "*****")
                {
                    sender = cm.Sender + string.Format("][Private to {0}", cm.Receiver);
                }
                else
                {
                    sender = cm.Sender;
                }
                lviItem = new ListViewItem();
                lviItem.SubItems[0].Text = string.Format("[{0}][{1}] >> {2}", DateTime.Now.ToString("HH:mm"), sender, cm.Message);
                lviItem.SubItems[0].ForeColor = cm.Color;
                lvClient.Items.Add(lviItem);
                lvClient.Items[lvClient.Items.Count - 1].EnsureVisible();
                //}
            }

            bool a = FlashWindowHelper.FlashWindowEx(this);

            if (msg is StatusMessage)
            {
                StatusMessage sm = msg as StatusMessage;
                cbClientsList.Items.Clear();
                cbClientsList.Items.AddRange(sm.Clients.ToArray());
            }
                
            //if (msg is OperationMessage)
            //{
            //    OperationMessage om = msg as OperationMessage;
            //    Trace.WriteLine(string.Format("{0} Wrote : {1}", om.Sender, "cm.Message"));

            //    lviItem = new ListViewItem();
            //    lviItem.SubItems[0].Text = string.Format("[{0}][{1}] >> {2}", DateTime.Now.ToString("HH:mm"), om.Sender, "tm.Message");
            //    lviItem.SubItems[0].ForeColor = om.Color;
            //    lvClient.Items.Add(lviItem);
            //    lvClient.Items[lvClient.Items.Count - 1].EnsureVisible();
            //}
        }

        #endregion

        #region Form Methods

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DropChatClient();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _broadcastingMessage.Message = txtMessage.Text;
            if (!string.IsNullOrEmpty(cbClientsList.Text) && cbClientsList.Text != _client.GetName())
            {
                _broadcastingMessage.Receiver = cbClientsList.Text;
            }
            else
            {
                _broadcastingMessage.Receiver = "";
            }
            _client.SendMessage(_broadcastingMessage);
            txtMessage.Clear();
        }

        #endregion

        #region d'tor

        public void DropChatClient()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
                this.Close();
            }
        }

        #endregion
    }
}
