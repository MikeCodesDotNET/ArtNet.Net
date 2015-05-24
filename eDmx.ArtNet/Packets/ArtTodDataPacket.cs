using ArtNet.Enums;
using ArtNet.IO;
using ArtNet.Rdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtNet.Packets
{
    public class ArtTodDataPacket : ArtNetPacket
    {
        public ArtTodDataPacket()
            : base(ArtNetOpCodes.TodData)
        {
            RdmVersion = 1;
            Devices = new List<UId>();
        }

        public ArtTodDataPacket(ArtNetRecieveData data)
            : base(data)
        {

        }

        #region Packet Properties

        public byte RdmVersion { get; set; }

        public byte Port { get; set; }

        public byte Net { get; set; }

        public byte Command { get; set; }

        public byte Universe { get; set; }

        public short UIdTotal { get; set; }

        public byte BlockCount { get; set; }

        public List<UId> Devices { get; set; }


        #endregion

        public override void ReadData(ArtNetBinaryReader data)
        {
            var reader = new ArtNetBinaryReader(data.BaseStream);

            base.ReadData(data);

            RdmVersion = data.ReadByte();
            Port = data.ReadByte();
            data.BaseStream.Seek(7, System.IO.SeekOrigin.Current);
            Net = data.ReadByte();
            Command = data.ReadByte();
            Universe = data.ReadByte();
            UIdTotal = reader.ReadNetwork16();
            BlockCount = data.ReadByte();

            Devices = new List<UId>();
            int count = data.ReadByte();
            for (int n = 0; n < count; n++)
                Devices.Add(reader.ReadUId());
        }

        public override void WriteData(ArtNetBinaryWriter data)
        {
            base.WriteData(data);

            var writer = new ArtNetBinaryWriter(data.BaseStream);

            data.Write(RdmVersion);
            data.Write(Port);
            data.Write(new byte[7]);
            data.Write(Net);
            data.Write(Command);
            data.Write(Universe);
            writer.WriteNetwork(UIdTotal);
            data.Write(BlockCount);
            data.Write((byte)Devices.Count);

            foreach (UId id in Devices)
                writer.Write(id);
        }


    }
}
