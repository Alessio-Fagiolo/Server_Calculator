using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Calculator
{
    public class Client
    {
        IPEndPoint serverEndPoint;
        Socket socket;
        private MemoryStream stream;
        private BinaryWriter writer;
        public Client(IPAddress ServerIp , int port)
        {
            serverEndPoint = new IPEndPoint(ServerIp, port);
            socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public void SendPacket(byte command, float n1, float n2)
        {
            //create a packet of 9 bytes where the first one is the command
            //the second and the third are the float numbers;
            //this packet will be send to the Server.
            byte[] packetToSend = new byte[9];
            writer.Write(command);
            writer.Write(n1);
            writer.Write(n2);
            packetToSend = stream.ToArray();
            socket.SendTo(packetToSend, serverEndPoint);

        }
        public float receive()
        {
            //receives a response packet from the server with the result of the operation
            //associated with the command byte.
            byte[] response = new byte[4];
            int rlen = socket.Receive(response);
            return BitConverter.ToSingle(response, 0);
        }
    }
}
