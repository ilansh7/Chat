using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Protocols
{
    public delegate void ValidatingDlg(MessageClass msg);
    public delegate void RegistrationDlg(MessageClass msg);
    public delegate void BroadcastingDlg(MessageClass msg);
    public delegate void ConnectionClientDlg();
    //public delegate EventHandler TerminateClientDlg();

    // Summary:
    //     Server side client object.
    public class Communicator : ClientClass, IDisposable
    {
        #region Properties

        public DateTime _connected { get; set; }
        public int UserId { get; set; }
        private Mode _mode;

        public List<MessageClass> CurrentChat;
        public event ValidatingDlg validateConnection;
        public event RegistrationDlg registrationClient;
        public event BroadcastingDlg broadcastingMsg;
        public event ConnectionClientDlg connectClient;
        public event EventHandler terminateClient;

        #endregion

        #region Methods

        public string GetName()
        {
            return _name;
        }

        public Mode GetMode()
        {
            return _mode;
        }
        
        #endregion

        #region c'tor
        public Communicator(TcpClient client)
        {
            _tcpClient = client;
            _binaryFormatter = new BinaryFormatter();
            _connected = DateTime.Now;
        }
        #endregion

        #region Actions

        public void Start()
        {
            _networkStream = _tcpClient.GetStream();
            var chatMessage = (MessageClass)_binaryFormatter.Deserialize(_networkStream);

            if (chatMessage is ValidationMessage)
            {
                Trace.WriteLine(string.Format("\t(ValidationMessage)"));
                _mode = Mode.Validate;
                //this.validateConnection
                var ValidationMsg = chatMessage as ValidationMessage;
                this._name = ValidationMsg.Sender;
                if (validateConnection != null)
                    validateConnection(ValidationMsg);
                ////Thread.Sleep(1110);
                //return;
                this.UserId = ValidationMsg.UserId;
            }

            if (chatMessage is RegistrationMessage)
            {
                Trace.WriteLine(string.Format("\t(RegistrationMessage)"));
                _mode = Mode.Registerd;
                //this.validateConnection
                var RegistrationMsg = chatMessage as RegistrationMessage;
                this._name = RegistrationMsg.UserLogin;
                if (registrationClient != null)
                    registrationClient(RegistrationMsg);
                ////Thread.Sleep(1110);
                //return;
            }

            if (chatMessage is OperationMessage)
            {
                OperationMessage connectionMessage = chatMessage as OperationMessage;
                this._name = connectionMessage.Sender;
                this.UserId = connectionMessage.UserId;

                Trace.WriteLine(string.Format("*** Client {0} Connected ****", this._name));

                if (connectClient != null)
                    connectClient();
            }

            try
            {
                Thread t = new Thread(HandleMsg) { IsBackground = true };
                t.Start();
            }
            catch (Exception ex)
            {
                //Trace.WriteLine("Exception on BackClient.Start : " + ex.Message);
                throw ex;
            }
        }

        public void HandleMsg()
        {
            //BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                while (_networkStream.CanRead)
                {
                    if (_networkStream == System.IO.Stream.Null)
                        continue;

                    var msg = (MessageClass)_binaryFormatter.Deserialize(_networkStream);

                    if (msg is ChatMessage)
                    {
                        var chatMessage = msg as ChatMessage;
                        if (broadcastingMsg != null)
                            broadcastingMsg(chatMessage);
                        Trace.WriteLine("(HandleMsg) : msg is ChatMessage.");
                    }
                    if (msg is StatusMessage)
                    {
                        var statusMessage = msg as StatusMessage;
                        if (broadcastingMsg != null)
                            broadcastingMsg(statusMessage);
                        Trace.WriteLine("(HandleMsg) : msg is StatusMessage.");
                    }
                    if (msg is OperationMessage)
                    {
                        var operationMessage = msg as OperationMessage;
                        if (!operationMessage.IsAlive)
                        {
                            if (terminateClient != null)
                                terminateClient(this, new EventArgs());
                        }
                        Trace.WriteLine("(HandleMsg) : msg is OperationMessage.");
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception on BackClient.HandleMsg : " + ex.Message);
                if (ex is System.IO.IOException)
                {
                    return;
                }
                if (ex is System.Runtime.Serialization.SerializationException)
                {
                    return;
                }
                throw ex;
            }
        }

        public void BroadcastToClient(MessageClass Msg)
        {
            if (_networkStream.CanWrite)
                try
                {
                    _binaryFormatter.Serialize(_networkStream, Msg);
                }
                catch (Exception ex)
                {
                    if (ex is System.IO.IOException)
                    {
                        return;
                    }
                    throw ex;
                }
        }

        #endregion

        #region d'tor
        public void Dispose()
        {
            _networkStream.Flush();
            _networkStream.Close();
            _tcpClient.Close();
            //if (t.IsAlive)
            //    t.Abort();
            GC.SuppressFinalize(this);
        }

        ~Communicator()
        {
            Dispose();
        }
        #endregion
    }
}