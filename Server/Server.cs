using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using BusinessLogic;
using Protocols;

namespace ServerLogic
{

    public delegate void BroadcastingStrDlg(MessageClass msg);
    public delegate void WriteListDlg();

    // Summary:
    //     Server side object.
    //     Handle server and its methods.
    public class Server : IDisposable
    {
        #region Properties

        public IPAddress _ipAddress { get; set; }
        public int _tcpPort { get; set; }

        private TcpClient _connectedClient;
        private TcpListener _serverListener;

        public List<Communicator> _clientsList { get; private set; }
        public List<Communicator> _validationList { get; private set; }

        public event BroadcastingStrDlg operationMsg;
        public event WriteListDlg writeConnections;

        public UsersLogic userLogic = new UsersLogic();
        public ChatLogic chatLogic = new ChatLogic();
        Thread t;

        #endregion

        #region c'tor

        public Server()
            : this(IPAddress.Parse("127.0.0.1"), 9999)
        {
        }

        public Server(IPAddress ip, int port)
        {
            _ipAddress = ip;
            _tcpPort = port;
            _serverListener = new TcpListener(this._ipAddress, this._tcpPort);
            _clientsList = new List<Communicator>();
            _validationList = new List<Communicator>();
        }

        #endregion

        #region Actions

        public void Connect()
        {
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            //tcpClient = new TcpClient();
            _serverListener.Start();
            userLogic.ResetConnection();
            //ChatMessage cm = new ChatMessage(null, Color.Black, string.Format("Server [{0}:{1}]", this._ipAddress, this._tcpPort));
            ////ServerStatus("Server Started...");
            //ServerStatus(cm);
            ////Console.WriteLine("server started");

            //TcpClient connectedClient = serverListener.AcceptTcpClient();
            t = new Thread(Listener);
            t.Start();
            Trace.WriteLine(string.Format("*** Start Listener ****"));
        }

        public void Listener()
        {
            while (true)
            {
                try
                {
                    _connectedClient = _serverListener.AcceptTcpClient();
                }
                catch (Exception ex)
                {
                    if (ex is System.Net.Sockets.SocketException)
                        break;
                    else
                        throw ex;
                }

                Communicator clientHandler = new Communicator(_connectedClient);
                _validationList.Add(clientHandler);
                clientHandler.validateConnection += ValidateConnections;
                clientHandler.registrationClient += RegistrationClient;
                clientHandler.Start();

                if ((clientHandler.GetMode() == Mode.Validate) || (clientHandler.GetMode() == Mode.Registerd))
                {
                    //_validationList.Remove(clientHandler);
                    //_clientsList.Add(clientHandler);
                    Trace.WriteLine(string.Format("Server.Listener() : {0}", clientHandler.GetMode()));
                    continue;
                }

                clientHandler.broadcastingMsg += BroadcastMessage;
                clientHandler.connectClient += WriteConnections;
                clientHandler.terminateClient += TerminateClient;
                Trace.WriteLine(string.Format("Server...Listener() : {0}", clientHandler.UserId));

                //Announcment for each new client
                ChatMessage cm = new ChatMessage("Server", Color.Black, string.Format("{0} is Connected.", clientHandler.GetName()));
                BroadcastMessage(cm);
                _clientsList.Add(clientHandler);
                userLogic.WriteConnection(clientHandler.UserId, true);
                // updating clint list
                OperationMessage om = new OperationMessage(clientHandler.GetName(), Color.Black, true);
                ServerStatus(om);

                //Distributing list of clients to all
                List<string> listOfClients = new List<string>();
                listOfClients.Add("");
                foreach (var item in _clientsList)
                {
                    listOfClients.Add(item.GetName());
                }
                StatusMessage sm = new StatusMessage("Server", Color.Black, listOfClients);
                BroadcastMessage(sm);
            }
        }

        public void ValidateConnections(MessageClass msg)
        {
            Trace.WriteLine(string.Format("Server : ValidateConnections()"));
            // CHECK IF CILENT IS AUTHORIZED
            if (msg is ValidationMessage)
            {
                var validationMessage = msg as ValidationMessage;
                int IsConnected = 0;

                string userId = userLogic.GetUser(validationMessage.Sender, IsConnected);
                //if ((validationMessage.Sender == "Client_77") || (validationMessage.Sender == "Client_55"))
                if (!string.IsNullOrEmpty(userId))
                {
                    int UserId;
                    if (int.TryParse(userId, out UserId))
                    {
                        validationMessage.IsValid = true;
                        validationMessage.UserId = UserId;
                    }
                }
                Trace.WriteLine(string.Format("\tmsg is ValidationMessage"));
                //_currentHandler.IsAuthorized = true;
                BroadcastValidation(validationMessage);
            }
        }

        public void RegistrationClient(MessageClass msg)
        {
            Trace.WriteLine(string.Format("Server : RegistrationClient()"));
            // CHECK IF CILENT IS AUTHORIZED
            bool isAutorized = false;
            if (msg is RegistrationMessage)
            {
                var registrationMessage = msg as RegistrationMessage;

                try
                {
                    userLogic.InsertUser(registrationMessage.UserName, registrationMessage.UserLogin);
                    isAutorized = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //if ((registrationMessage.UserLogin == "Client_11") || (registrationMessage.UserLogin == "Client_12"))
                //if (!string.IsNullOrEmpty(xx))
                registrationMessage.Success = isAutorized;
                //client.IsAuthorized = isAutorized;
                Trace.WriteLine(string.Format("\tmsg is RegistrationMessage"));
                //_currentHandler.IsAuthorized = true;
                BroadcastValidation(registrationMessage);
            }
        }

        public void WriteConnections()
        {
            lock (this)
            {
                if (writeConnections != null)
                    writeConnections();
            }
        }

        public void BroadcastMessage(MessageClass msg)
        {
            lock (this)
            {
                foreach (var client in _clientsList)
                {
                    Trace.WriteLine(string.Format("Server : BroadcastMessage(cLIENT)"));
                    client.BroadcastToClient(msg);
                }
                if (msg is ChatMessage)
                {
                    var chatMessage = msg as ChatMessage;
                    Trace.WriteLine("Server.BroadcastMessage() : msg is ChatMessage.");
                    if (chatMessage.Sender != "Server")
                    {
                        string z = chatLogic.InsertMessage(chatMessage.Sender, chatMessage.Message, chatMessage.Receiver);
                    }
                }
            }
        }

        public void BroadcastValidation(MessageClass msg)
        {
            lock (this)
            {
                foreach (var client in _validationList)
                {
                    Trace.WriteLine(string.Format("Server : BroadcastValidation({0})", client.UserId));
                    client.BroadcastToClient(msg);
                }
            }
        }

        public void ServerStatus(MessageClass msg)
        {
            if (operationMsg != null)
            {
                operationMsg(msg);
                Trace.WriteLine(string.Format("status : {0}", msg.ToString()));
            }
        }

        private void TerminateClient(object sender, EventArgs e)
        {
            lock (this)
            {
                var clientHandle = sender as Communicator;
                if (clientHandle != null)
                {
                    //string name = clientHandle.GetName();
                    _clientsList.Remove(clientHandle);
                    clientHandle.Dispose();
                    userLogic.WriteConnection(clientHandle.UserId, false);
                    //RefreshListEvent();
                    ChatMessage cm = new ChatMessage("Server", Color.Black, string.Format("{0} left the ChatRoom.", clientHandle.GetName()));
                    BroadcastMessage(cm);

                    // Disconnect message
                    OperationMessage om = new OperationMessage(clientHandle.GetName(), Color.Black, false);
                    ServerStatus(om);

                    //Updating list of connections
                    List<string> listOfClients = new List<string>();
                    listOfClients.Add("");
                    foreach (var item in _clientsList)
                    {
                        listOfClients.Add(item.GetName());
                    }
                    StatusMessage sm = new StatusMessage("Server", Color.Black, listOfClients);
                    BroadcastMessage(sm);

                    Trace.WriteLine("Client terminate itself");
                }
            }
        }

        public void TerminateClientByIndex(int idx)
        {
            lock (this)
            {
                if (_clientsList[idx] != null)
                {
                    //Notification (as private) about termination of the Server
                    ChatMessage cm = new ChatMessage("*****", Color.Black, "Connection to Server lost.");
                    cm.Receiver = _clientsList[idx].GetName();
                    BroadcastMessage(cm);

                    //Refresh the client list, Since the Server is initiating the termination...
                    //OperationMessage om = new OperationMessage(_clientsList[idx].GetName(), Color.Black, false);
                    //ServerStatus(om);

                    _clientsList.RemoveAt(idx);
                    //clientHandle.Dispose();

                    //EventArgs e = new EventArgs();
                    //TerminateClient(_clientsList[idx], new EventArgs());
                    Trace.WriteLine("Client terminate by Server");
                }
            }
        }

        public void RemoveClientById(int id)
        {
            int count = _clientsList.Count;
            for (int i = 0; i < count; i++)
            {
                if (_clientsList[i].UserId == id)
                {
                    var clientHandle = _clientsList[i] as Communicator;
                    TerminateClient(clientHandle, new EventArgs());
                    //cannot comntinue, index will be out of bound. use recursive call.
                    if (i + 1 < count)
                        RemoveClientById(id);
                    return;
                }
            }
        }


        #endregion

        #region d'tor

        public void Dispose()
        {
            lock (this)
            {
                int count = _clientsList.Count;
                for (int i = 0; i < count; i++)
                {
                    TerminateClientByIndex(0);
                }
            }
            _connectedClient.Close();
            _serverListener.Stop();
            if (t.IsAlive)
                t.Abort();
            GC.SuppressFinalize(this);
        }

        ~Server()
        {
            Dispose();
        }

        #endregion
    }
}
