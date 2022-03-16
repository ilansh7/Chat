using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

using Protocols;

namespace ClientLogic
{
    public delegate void WritingMessageDlg(MessageClass msg);
    public delegate void TerminateClientDlg();

    // Summary:
    //     Client side object.
    //     Handle client and its methods.
    public class Client : ClientClass, IDisposable
    {
        #region Properties

        public event WritingMessageDlg writingMessage;
        public event TerminateClientDlg terminateClient;
        Thread t;

        #endregion

        #region c'tor

        public Client()
            : this(IPAddress.Parse("127.0.0.1"), 9999, "Ilan", Color.Red, 0)
        {
        }

        public Client(IPAddress ip, int port, string name, Color color, int id)
        {
            _id = id;
            _name = name;
            _ipAddress = ip;
            _color = color;
            _tcpPort = port;

            _tcpClient = new TcpClient();
            _binaryFormatter = new BinaryFormatter();
        }

        #endregion

        #region Methods

        public string GetName()
        {
            return _name;
        }

        public int GetPort()
        {
            return _tcpPort;
        }

        #endregion

        #region Actions

        public void Connect()
        {
            try
            {
                _tcpClient.Connect(_ipAddress, _tcpPort);
            }
            catch (Exception ex)
            {
                // No listener
                if (ex is SocketException)
                {
                    //this.Dispose();
                    _ipAddress = null;
                    _tcpPort = -1;
                    return;
                }
                throw ex;
            }
            _networkStream = _tcpClient.GetStream();
            t = new Thread(ReceiveMessage) { IsBackground = true };
            t.Start();

            OperationMessage connectionMessage = new OperationMessage(this._name, this._color, true);
            connectionMessage.UserId = this._id;
            SendMessage(connectionMessage);
        }


        public void SendMessage(MessageClass msg)
        {
            try
            {
                if (_networkStream.CanWrite)
                {
                    _binaryFormatter.Serialize(_networkStream, msg);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Client.SendMessage : " + string.Format(ex.Message));
                //throw ex;
            }
        }

        public void ReceiveMessage()
        {
            try
            {
                while (_networkStream.CanRead)
                {
                    var msg = (MessageClass)_binaryFormatter.Deserialize(_networkStream);

                    //Chat Message
                    if (msg is ChatMessage)
                    {
                        var MsgFromServer = msg as ChatMessage;
                        if (writingMessage != null)
                            writingMessage(MsgFromServer);
                    }

                    //Status Message - connections list
                    if (msg is StatusMessage)
                    {
                        var ListOfClients = msg as StatusMessage;
                        if (writingMessage != null)
                            writingMessage(ListOfClients);
                    }

                    if (msg is OperationMessage)
                    {
                        var MsgFromServer = msg as OperationMessage;
                        if (writingMessage != null)
                            writingMessage(MsgFromServer);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("ReceiveMessage Exception : {0}", ex.Message));
                Dispose();
                //throw ex;
            }
        }

        #endregion

        #region d'tor

        public void Dispose()
        {
            OperationMessage diconnectionMessage = new OperationMessage(this._name, this._color, false);
            SendMessage(diconnectionMessage);
            if (_networkStream != null)
            {
                _networkStream.Flush();
                _networkStream.Close();
            }
            if (_tcpClient.Connected)
                _tcpClient.Close();
            //this.terminateClient();
            if (t != null && t.IsAlive)
                t.Abort();
            GC.SuppressFinalize(this);
        }

        ~Client()
        {
            Dispose();
        }

        #endregion
    }
}
