using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;

namespace Protocols
{
    [Serializable]
    public class MessageClass
    {
        protected string _sender;
        protected Color _color;
        //protected string _message;
    }

    [Serializable]
    public class ChatMessage : MessageClass
    {
        public int SenderId { get; set; }
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public string Message { get; set; }
        //for private message
        public int ReceiverId { get; set; }
        public string Receiver { get; set; }

        public ChatMessage(string send, Color col, string msg)
        {
            Sender = send;
            Color = col;
            Message = msg;
            Receiver = "";
        }
    }

    [Serializable]
    //for initiating/terminating clients
    public class OperationMessage : MessageClass
    {
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public bool IsAlive { get; set; }
        public int UserId { get; set; }

        public OperationMessage(string send, Color col, bool isAlive)
        {
            Sender = send;
            Color = col;
            IsAlive = isAlive;
        }
    }

    //Hold the list of connected clients
    [Serializable]
    public class StatusMessage : MessageClass
    {
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        //for private message
        public List<string> Clients { get; set; }

        public StatusMessage(string send, Color col, List<string> list)
        {
            Sender = send;
            Color = col;
            Clients = list;
        }
    }

    [Serializable]
    //for User validation
    public class ValidationMessage : MessageClass
    {
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public bool IsValid { get; set; }
        public int UserId { get; set; }

        public ValidationMessage(string send, Color col, bool isValid)
        {
            Sender = send;
            Color = col;
            IsValid = isValid;
        }
    }

    [Serializable]
    //Register new user
    public class RegistrationMessage : MessageClass
    {
        public string UserLogin { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public bool Success { get; set; }

        public IPAddress _ipAddress { get; set; }
        public int _tcpPort { get; set; }

        public RegistrationMessage(string loginName, string userName, string password, string mail)
        {
            UserLogin = loginName;
            UserName = userName;
            Password = password;
            EMail = mail;
            Success = false;
        }
    }

}
