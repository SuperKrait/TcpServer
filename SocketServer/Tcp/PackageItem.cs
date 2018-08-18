using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SocketServer.Tcp
{
    internal class PackageItem
    {
        public Guid packageId;
        private byte[] data;
        private List<byte[]> packageList = new List<byte[]>();
         

        #region 拆包
        public void SetData(byte[] data)
        {
            this.data = data;
        }

        public int SplitPacket(short count)
        {
            packageList.Clear();
            Guid.NewGuid();
            return 0;
        }

        /// <summary>
        /// 包结构--|id|index（从0开始）|包的状态值（单个包为0，多个包为1）|这个包的总长度length（ulong）|时间戳|包的数据|
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public byte[] GetData(int index)
        {
            return null;
        }
        
        
        #endregion
    }
}
