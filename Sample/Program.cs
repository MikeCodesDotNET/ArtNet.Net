using System;
using System.Net;
using ArtNet.Sockets;
using ArtNet.Packets;

namespace Sample
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var artnet = new ArtNet.Sockets.ArtNetSocket();
            artnet.EnableBroadcast = true;

            Console.WriteLine(artnet.BroadcastAddress.ToString());
            artnet.Open(IPAddress.Parse("127.0.0.1"), IPAddress.Parse("255.255.255.0"));

            byte[] _dmxData = new byte[511];

            artnet.NewPacket += (object sender, ArtNet.Sockets.NewPacketEventArgs<ArtNet.Packets.ArtNetPacket> e) => 
            {
                if(e.Packet.OpCode == ArtNet.Enums.ArtNetOpCodes.Dmx)
                {  
                    var packet = e.Packet as ArtNet.Packets.ArtNetDmxPacket;
                        Console.Clear();

                        if(packet.DmxData != _dmxData)
                        {
                            Console.WriteLine("New Packet");
                            for(var i = 0; i < packet.DmxData.Length; i++)
                            {
                                if(packet.DmxData[i] != 0)
                                    Console.WriteLine(i + " = " + packet.DmxData[i]);

                            };

                            _dmxData = packet.DmxData;
                        }
                    }
            };
            
            Console.ReadLine();

        }

        byte[] _dmxData = new byte[511];
    }
}
