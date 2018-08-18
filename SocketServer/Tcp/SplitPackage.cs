using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Tcp
{
    class SplitPackage : LittlePackage
    {
        public int index;
        public SplitPackage(byte[] id, byte state, ulong totalLength, long timeTick, ushort thisLittlePackageLength)// :base(id, state, totalLength, timeTick, thisLittlePackageLength)
        {
            this.id = id;
            this.state = state;
            this.totalLength = totalLength;
            this.timeTick = timeTick;
            this.thisLittlePackageLength = thisLittlePackageLength;
        }
        /// <summary>
        /// 拆分包数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<byte> SplitData(int index, byte[] data)
        {
            this.index = index;
            int tmpIndex = thisLittlePackageLength * index;
            int tmpOver = data.Length - tmpIndex;
            if (tmpOver < 0)
            {
                return null;
            }
            int tmpEnd = tmpIndex + thisLittlePackageLength;
            List<byte> list = GenerateHead();
            if (tmpOver <= thisLittlePackageLength)
            {
                for (int i = tmpIndex; i < tmpOver; i++)
                {
                    list.Add(data[i]);
                }
                for (int i = data.Length - 1; i < tmpEnd; i++)
                {
                    list.Add(0);
                }
            }
            else
            {
                for (int i = tmpIndex; i < tmpEnd; i++)
                {
                    list.Add(data[i]);
                }
            }
            return list;
        }
        private List<byte> GenerateHead()
        {
            List<byte> headList = new List<byte>(37);
            headList.AddRange(id);
            byte[] tmp = BitConverter.GetBytes(index);
            headList.AddRange(tmp);
            tmp = BitConverter.GetBytes(state);
            headList.AddRange(tmp);
            tmp = BitConverter.GetBytes(totalLength);
            headList.AddRange(tmp);
            tmp = BitConverter.GetBytes(timeTick);
            headList.AddRange(tmp);
            return headList;
        }
    }
}
