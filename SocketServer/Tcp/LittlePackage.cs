using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Tcp
{
    internal class LittlePackage
    {
        public byte[] id;
        public byte state;
        public ulong totalLength;
        public long timeTick;
        public ushort thisLittlePackageLength;
    }
}
