using Protocols;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ClientUI
{
    public partial class LoginForm : Form
    {
        #region Properties

        private TcpClient _tcpCheckConnection;
        private BinaryFormatter _binaryFormatter;

        #endregion

        #region c'tor

        public LoginForm()
        {
            InitializeComponent();
            FillColorList();
            Random rnd = new Random();
            cbColor.SelectedIndex = rnd.Next(1, cbColor.Items.Count);
            string num = rnd.Next(1, 99).ToString();
            num = (num.Length == 1) ? "0" + num : num;
            txtLoginName.Text = "Client_" + num;
            lblLoginStatus.Text = "";// "Invalid Login name / Password";
        }

        #endregion

        #region Form Methods

        private void FillColorList()
        {
            foreach (System.Reflection.PropertyInfo prop in typeof(Color).GetProperties())
            {
                if (prop.PropertyType.FullName == "System.Drawing.Color")
                    cbColor.Items.Add(prop.Name);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.OK;
            lblLoginStatus.Text = "";
            _tcpCheckConnection = new TcpClient();
            _binaryFormatter = new BinaryFormatter();

            try
            {
                _tcpCheckConnection.Connect(IPAddress.Parse(txtIPAddress.Text), (int)numPort.Value);
                using (NetworkStream _networkStream = _tcpCheckConnection.GetStream())
                {
                    _binaryFormatter.Serialize(_networkStream, new ValidationMessage(txtLoginName.Text, Color.FromName(cbColor.Text), false));
                    var msg = (ValidationMessage)_binaryFormatter.Deserialize(_networkStream);
                    if (msg.IsValid)
                    {
                        txtUserId.Text = msg.UserId.ToString();
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        lblLoginStatus.ForeColor = Color.Red;
                        lblLoginStatus.BackColor = Color.White;
                        lblLoginStatus.Text = "Invalid Login name / Password";
                        //btnRegister.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is SocketException)
                {
                    CloseLoginForm();
                    return;
                }
                throw ex;
            }
            CloseLoginForm();
            //DialogResult = DialogResult.OK;
        }

        private void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.FromName(cbColor.Text);
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistrationMessage rMsg = new RegistrationMessage(txtLoginName.Text, txtLoginName.Text, "", "");
            rMsg._ipAddress = IPAddress.Parse(txtIPAddress.Text);
            rMsg._tcpPort = (int)numPort.Value;
            RegistrationForm clientRegistration = new RegistrationForm(rMsg);
            clientRegistration.ShowDialog();
        }

        #endregion

        #region d'tor

        public void CloseLoginForm()
        {
            if (_tcpCheckConnection != null)
                if (_tcpCheckConnection.Connected)
                    _tcpCheckConnection.Close();
        }

        #endregion
    }
}
