using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace SocketServer.Tcp
{
    public class CustomTcpClient
    {
        private TcpClient tcpClient;
        private bool isStart = false;
        private Queue<byte[]> writeQueue;
        public void Start(TcpClient client)
        {
            this.tcpClient = client;
            NetworkStream netStream = tcpClient.GetStream();
            isStart = true;
            while (isStart)
            {
                if (netStream.CanWrite)
                {
                    //Byte[] sendBytes = Encoding.UTF8.GetBytes("Is anybody there?");
                    //netStream.Write(GetData(), 0, GetData().Length);
                }
                if (netStream.CanRead)
                {
                    // Reads NetworkStream into a byte buffer.
                    byte[] bytes = new byte[tcpClient.ReceiveBufferSize];

                    // Read can return anything from 0 to numBytesToRead. 
                    // This method blocks until at least one byte is read.
                    netStream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);

                    // Returns the data received from the host to the console.
                    string returndata = Encoding.UTF8.GetString(bytes);

                    Console.WriteLine("This is what the host returned to you: " + returndata);

                }

            }
            this.tcpClient.Close();
            netStream.Close();
        }


        private byte[] GetData()
        {
            return writeQueue.Dequeue();
        }

        public void SetData(byte[] data)
        {
            if(isStart)
                lock (writeQueue)
                {
                    writeQueue.Enqueue(data);
                }
        }

        public void Destory()
        {
            isStart = false;
        }
    }
}
