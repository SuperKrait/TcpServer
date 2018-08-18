using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Common.ThreadPool;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace SocketServer.Tcp
{
    public class TcpPool
    {
        private ServerInfo serverInfo;
        private string threadId;
        private bool isStart = false;
        public void Init(ServerInfo serverInfo)
        {
            this.serverInfo = serverInfo;
        }

        public void Start()
        {
            ThreadPoolMgrSimple.Start(WaitForTcpClient, 1, SetErrorMsg, SetThreadId);
        }
        private void SetErrorMsg(string errorCode)
        {
            Debug.Print(string.Format("==============TcpPool=========\nSetThreadId={0}\nerrorCode={1}", threadId, errorCode));
        }

        private void SetThreadId(string threadId)
        {
            this.threadId = threadId;
        }

        public void WaitForTcpClient()
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(serverInfo.GetIpEnd());
                listener.Start();
            }
            catch (System.ArgumentNullException e)
            {
                Debug.Print(e.ToString());
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Debug.Print(e.ToString());
            }
            if (listener == null)
            {
                return;
            }
            while (isStart)
            {
                if (listener.Pending())
                {
                    listener.AcceptTcpClient();
                }
                Thread.Sleep(0);
            }
        }

        public void HandOutClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream netStream = client.GetStream();

            while (true)
            {
                

                netStream.BeginRead()
            }
        }        

        public void Destory()
        {

        }
    }
}
