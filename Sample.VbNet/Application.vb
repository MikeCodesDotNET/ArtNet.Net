Imports System.Net
Imports ArtNet.Packets
Imports ArtNet.Sockets

Module Module1

    Public Sub Main()

        Dim artnet = New ArtNetSocket()

        artnet.EnableBroadcast = True

        Console.WriteLine(artnet.BroadcastAddress.ToString())
        artnet.Open(IPAddress.Parse("127.0.0.1"), IPAddress.Parse("255.255.255.0"))

        Dim _dmxData As Byte() = New Byte(510) {}

        AddHandler artnet.NewPacket, AddressOf ProcessPacket

        Console.ReadLine()

    End Sub

    Private _dmxData As Byte() = New Byte(510) {}

    Private Sub ProcessPacket(sender As Object, e As NewPacketEventArgs(Of ArtNetPacket))
        If (e.Packet.OpCode = ArtNet.Enums.ArtNetOpCodes.Dmx) Then
            Dim packet = TryCast(e.Packet, ArtNet.Packets.ArtNetDmxPacket)
            Console.Clear()

            If packet.DmxData Is _dmxData Then
                Console.WriteLine("New Packet")
                Dim i As Integer = 0
                While i < packet.DmxData.Length
                    If packet.DmxData(i) <> 0 Then
                        Console.WriteLine(i + " = " + packet.DmxData(i))

                    End If
                    i = i + 1
                End While

                _dmxData = packet.DmxData
            End If
        End If

    End Sub

End Module
