using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;

namespace SocketServer.Tcp
{
    public class ServerInfo
    {
        private IPAddress ipAdd = null;
        private int port = 32001;
        public string ServerIp
        {
            get
            {
                return ipAdd.ToString();
            }
            set
            {
                try
                {
                    ipAdd = IPAddress.Parse(value);
                    lock (lockObj)
                        point = null;
                    return;
                }
                catch (System.FormatException e)
                {
                    Debug.Print(e.ToString());
                }
                catch (System.ArgumentNullException e)
                {
                    Debug.Print(e.ToString());
                }
                ipAdd = IPAddress.Parse("127.0.0.1");
            }
        }
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                if (value >= 0 && value <= 65535)
                {
                    port = value;
                    lock(lockObj)
                        point = null;
                }
            }
        }

        public int MaxTcpCount
        {
            get;
            set;
        }

        private object lockObj = new object();
        private IPEndPoint point = null;
        public IPEndPoint GetIpEnd()
        {
            lock (lockObj)
            {
                if (point == null && ipAdd != null)
                {
                    point = new IPEndPoint(ipAdd, port);
                }
                return point;
            }
        }
    }
}
