using ArtNet.Enums;
using ArtNet.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.Packets
{
    public class ArtTodControlPacket : ArtNetPacket
    {
        public ArtTodControlPacket()
            : base(ArtNetOpCodes.TodControl)
        {
        }

        public ArtTodControlPacket(ArtNetRecieveData data)
            : base(data)
        {

        }

        #region Packet Properties

        public byte Net { get; set; }

        public ArtTodControlCommand Command { get; set; }

        public byte Address { get; set; }


        #endregion

        public override void ReadData(ArtNetBinaryReader data)
        {
            base.ReadData(data);

            data.BaseStream.Seek(9, System.IO.SeekOrigin.Current);
            Net = data.ReadByte();
            Command = (ArtTodControlCommand)data.ReadByte();
            Address = data.ReadByte();
        }

        public override void WriteData(ArtNetBinaryWriter data)
        {
            base.WriteData(data);

            data.Write(new byte[9]);
            data.Write(Net);
            data.Write((byte)Command);
            data.Write(Address);
        }


    }
}
