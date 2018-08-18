using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Tcp
{
    internal class CombinPackage : LittlePackage
    {
        public CombinPackage()
        {
            id = new byte[16];
            package = new CombinItem();
            CombinItemList = new List<CombinItem>();
            CombinIndex = new List<int>();
        }
        private CombinItem package;
        private List<CombinItem> CombinItemList;// = new List<CombinItem>();
        private List<int> CombinIndex;// = new List<int>();
        private long curTotalLength;

        /// <summary>
        /// 返回null为塞包成功，返回数据为塞包失败
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] AddPackage(byte[] data)
        {
            for (int i = 0; i < id.Length; i++)
            {
                if (id[i] != data[i])
                    return data;
            }

            CombinItem item = new CombinItem();
            item.SetItem(data);

            if (CombinIndex.Contains(item.index))
            {
                return null;
            }

            CombinItemList.Add(item);
            return null;
        }
        class CombinItem : IComparable<CombinItem>
        {
            public int index;
            public int length;
            public void SetItem(byte[] data)
            {
                byte[] tmpIndex = new byte[4];
                index = BitConverter.ToInt32(data, 17);
                length = data.Length - 37;
            }

            public int CompareTo(CombinItem other)
            {
                if (this.index < other.index)
                {
                    return -1;
                }
                else
                    return 1;
            }
        }
    }
}
