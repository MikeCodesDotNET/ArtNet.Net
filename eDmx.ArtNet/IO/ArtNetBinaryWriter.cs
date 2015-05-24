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
    public class ArtNetBinaryWriter : BinaryWriter
    {
        public ArtNetBinaryWriter(Stream output)
            : base(output)
        {
        }

        public void WriteNetwork(byte value)
        {
            base.Write(IPAddress.HostToNetworkOrder(value));
        }

        public void WriteNetwork(short value)
        {
            base.Write(IPAddress.HostToNetworkOrder(value));
        }

        public void WriteNetwork(int value)
        {
            base.Write(IPAddress.HostToNetworkOrder(value));
        }

        public void WriteNetwork(string value, int length)
        {
            Write(Encoding.UTF8.GetBytes(value.PadRight(length, (char)0x0)));
        }

        public void Write(UId value)
        {
            WriteNetwork((short)value.ManufacturerId);
            WriteNetwork((int)value.DeviceId);
        }
    }
}
