using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Net;
using System.Net.Sockets;

namespace Calculator.Test
{
    public class Test
    {
        [Test]
        public void CheckSum()
        {
            Server server = new Server(80);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 80);
            server.Bind();
            client.SendPacket(0, 2, 2);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(4));
        }
        [Test]
        public void CheckSumWithNegatives()
        {
            Server server = new Server(850);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 850);
            server.Bind();
            client.SendPacket(0, -2, 2);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(0));
        }
        [Test]
        public void CheckSubtraction()
        {
            Server server = new Server(90);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 90);
            server.Bind();
            client.SendPacket(1, 2, 2);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(0));
        }
        [Test]
        public void CheckMultiplication()
        {
            Server server = new Server(100);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 100);
            server.Bind();
            client.SendPacket(2, 5, 2);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(10));
        }
        [Test]
        public void CheckMultiplicationWithNegative()
        {
            Server server = new Server(1200);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 1200);
            server.Bind();
            client.SendPacket(2, -5, 2);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(-10));
        }
        [Test]
        public void CheckDivision()
        {
            Server server = new Server(50);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 50);
            server.Bind();
            client.SendPacket(3, 10, 2);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(5));
        }
        [Test]
        public void CheckWrongCommandpacket()
        {
            Server server = new Server(10);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 10);
            server.Bind();
            client.SendPacket(6, 5, 2);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(0));
        }
        [Test]
        public void CheckEmptypacket()
        {
            Server server = new Server(140);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 140);
            server.Bind();
            client.SendPacket(0, float.NaN, float.NaN);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(float.NaN));
        }
        [Test]
        public void CheckMultiplicationWithNegativeNumbers()
        {
            Server server = new Server(1300);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 1300);
            server.Bind();
            client.SendPacket(2, -5, 2);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(-10));
        }
        [Test]
        public void CheckDivisionWithWithZero()
        {
            Server server = new Server(300);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 300);
            server.Bind();
            client.SendPacket(3, 5, 0);
            server.Process();
            Assert.That(float.IsInfinity(client.receive()), Is.True);
        }
        [Test]
        public void CheckDivisionWithWithnegatives()
        {
            Server server = new Server(3400);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 3400);
            server.Bind();
            client.SendPacket(3, -6, 2);
            server.Process();
            Assert.That(client.receive(), Is.EqualTo(-3));
        }
        [Test]
        public void CheckSumMaxFloat()
        {
            Server server = new Server(40);
            Client client = new Client(IPAddress.Parse("127.1.1.8"), 40);
            server.Bind();
            client.SendPacket(0, float.MaxValue, float.MaxValue);
            server.Process();
            Assert.That(float.IsInfinity(client.receive()), Is.True);
        }
    }
}
