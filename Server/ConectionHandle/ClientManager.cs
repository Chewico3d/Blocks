using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.ConectionHandle {
    internal class ClientManager {
        internal Socket MasterSocket;
        List<ClientConnection> Connections = new List<ClientConnection>();

        internal void Start() {
            MasterSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            MasterSocket.Bind(IPEndPoint.Parse("0.0.0.0:212"));
            MasterSocket.Listen(212);
            Console.WriteLine("Lisening to port 212");
            MasterSocket.BeginAccept(new AsyncCallback(ClientConnect), null);
        }

        internal void ClientConnect(IAsyncResult result) {
            Socket client = MasterSocket.EndAccept(result);

            Connections.Add(new ClientConnection(client));
            MasterSocket.BeginAccept(new AsyncCallback(ClientConnect), null);



        }
    }
}
