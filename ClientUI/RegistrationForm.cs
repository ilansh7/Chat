using System;
using System.Drawing;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

using Protocols;

namespace ClientUI
{
    public partial class RegistrationForm : Form
    {
        #region Properties

        private RegistrationMessage _rmsg;
        private TcpClient _tcpRegistrationClient;
        private BinaryFormatter _binaryFormatter;

        #endregion

        #region Initialization

        public RegistrationForm(MessageClass msg)
        {
            _rmsg = msg as RegistrationMessage;
            InitializeComponent();
            txtLoginName.Text = _rmsg.UserLogin;
            txtUserName.Text = _rmsg.UserName;
            if (string.IsNullOrEmpty(_rmsg.UserName))
                txtUserName.Text = _rmsg.UserLogin;
            txtEMail.Text = _rmsg.EMail;
        }

        #endregion

        #region Form Methods

        private void RegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseRegistrationForm();
        }

        #endregion
        
        #region Controls

        private void btnRegister_Click(object sender, EventArgs e)
        {
            lblInfoMsg.Text = "Not Autorized";
            //DialogResult = DialogResult.OK;
            //return;
            _tcpRegistrationClient = new TcpClient();
            _binaryFormatter = new BinaryFormatter();
            _rmsg.UserName = txtUserName.Text;

            try
            {
                _tcpRegistrationClient.Connect(_rmsg._ipAddress, _rmsg._tcpPort);
                using (NetworkStream _networkStream = _tcpRegistrationClient.GetStream())
                {
                    _binaryFormatter.Serialize(_networkStream, _rmsg);
                    bool reply = (_binaryFormatter.Deserialize(_networkStream) as RegistrationMessage).Success;
                    if (reply)
                    {
                        //if (_networkStream != null)
                        //{
                        //    _networkStream.Flush();
                        //    _networkStream.Close();
                        //}
                        //if (_tcpRegistrationClient.Connected)
                        //    _tcpRegistrationClient.Close();
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        lblInfoMsg.ForeColor = Color.Red;
                        lblInfoMsg.Text = "Invalid Login name / Password";
                        btnRegister.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is SocketException)
                {
                    CloseRegistrationForm();
                    return;
                }
                throw ex;
            }
            CloseRegistrationForm();
            //DialogResult = DialogResult.OK;
        }

        #endregion

        #region d'tor

        public void CloseRegistrationForm()
        {
            if (_tcpRegistrationClient != null)
                if (_tcpRegistrationClient.Connected)
                    _tcpRegistrationClient.Close();
        }

        #endregion
    }
}
