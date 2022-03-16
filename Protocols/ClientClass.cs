using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Protocols
{
    // Summary:
    //     Client Class.

    public enum Mode
    {
        Created,
        Registerd,
        Validate,
        Verified,
    }

    public class ClientClass
    {
        #region Properties

        protected internal int _id { get; protected set; }
        protected internal string _name { get; set; }
        protected internal Color _color { get; set; }

        protected internal IPAddress _ipAddress { get; set; }
        protected internal int _tcpPort { get; set; }
        protected internal TcpClient _tcpClient;

        protected internal NetworkStream _networkStream;
        protected internal BinaryFormatter _binaryFormatter;

        #endregion
    }
}
