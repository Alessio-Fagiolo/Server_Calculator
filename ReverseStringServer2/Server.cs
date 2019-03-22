using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Calculator
{
    public class Server
    {
        delegate void OperationByte(byte[] ByteArray, EndPoint server);
        Dictionary<byte, OperationByte> CommandDict;
        IPEndPoint ServerEndPoint;
        Socket socket;
        public Server(int port)
        {
            ServerEndPoint = new IPEndPoint(IPAddress.Any, port);
            socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
            CommandDict = new Dictionary<byte, OperationByte>();
            CommandDict[0] = Sum;
            CommandDict[1] = Subtraction;
            CommandDict[2] = Multiplication;
            CommandDict[3] = Division;
        }
        public void Bind()
        {
            socket.Bind(ServerEndPoint);
        }

        public void Process()
        {
            //waiting to receive a packet from the client and send a reply packet with the result
            //associated with the desired operation
            byte[] data = new byte[9];
            EndPoint sender = new IPEndPoint(IPAddress.Any, 9999);
            int rlen = socket.ReceiveFrom(data, ref sender);
            byte command = data[0];
            if (CommandDict.ContainsKey(command))
            {
                CommandDict[command](data, sender);
            }
            else
            {
                byte[] dataEmpty = new byte[0];
                socket.SendTo(dataEmpty, sender);
            }

        }
        public void Sum(byte[] bytearray, EndPoint sender)
        {
            //reads an array of bytes and breaks it down to take the two numbers and adds them
            float n1 = BitConverter.ToSingle(bytearray, 1);
            float n2 = BitConverter.ToSingle(bytearray, 5);
            float result = n1 + n2;
            byte[] resultInBytes = BitConverter.GetBytes(result);
            socket.SendTo(resultInBytes, sender);
        }
        public void Subtraction(byte[] bytearray, EndPoint sender)
        {
            //reads an array of bytes and breaks it down to take the two numbers and subtract them
            float n1 = BitConverter.ToSingle(bytearray, 1);
            float n2 = BitConverter.ToSingle(bytearray, 5);
            float result = n1 - n2;
            byte[] resultInBytes = BitConverter.GetBytes(result);
            socket.SendTo(resultInBytes, sender);
        }
        public void Multiplication(byte[] bytearray, EndPoint sender)
        {
            //reads an array of bytes and breaks it down to take the two numbers and multiplicate them
            float n1 = BitConverter.ToSingle(bytearray, 1);
            float n2 = BitConverter.ToSingle(bytearray, 5);
            float result = n1 * n2;
            byte[] resultInBytes = BitConverter.GetBytes(result);
            socket.SendTo(resultInBytes, sender);
        }
        public void Division(byte[] bytearray, EndPoint sender)
        {
            //reads an array of bytes and breaks it down to take the two numbers and divide them
            float n1 = BitConverter.ToSingle(bytearray, 1);
            float n2 = BitConverter.ToSingle(bytearray, 5);
            float result = n1 / n2;
            byte[] resultInBytes = BitConverter.GetBytes(result);
            socket.SendTo(resultInBytes, sender);
        }

        public void stupidu()
        {
            Console.WriteLine("scioccooooooo");
        }
    }
}
