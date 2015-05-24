using ArtNet.Rdm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.IO
{
    public class ArtNetBinaryReader : BinaryReader
    {
        public ArtNetBinaryReader(Stream input)
            : base(input)
        {
        }

        public short ReadNetwork16()
        {
            return (short)IPAddress.NetworkToHostOrder(ReadInt16());
        }

        public int ReadNetwork32()
        {
            return (int)IPAddress.NetworkToHostOrder(ReadInt32());
        }

        public string ReadNetworkString(int length)
        {
            return Encoding.UTF8.GetString(ReadBytes(length));
        }

        public UId ReadUId()
        {
            return new UId((ushort)(int)ReadNetwork16(), (uint)ReadNetwork32());
        }
    }
}
