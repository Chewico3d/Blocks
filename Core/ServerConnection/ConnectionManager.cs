using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServerConnection {
    internal class ConnectionManager {

        internal Socket TCP_Connection { get; set; }
        internal static Socket MainSocket => BlocksGame.Instance.connectionManager.TCP_Connection;

        byte[] BufferMemort = new byte[1024];

        internal ConnectionManager() {
            TCP_Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            TCP_Connection.Connect(IPEndPoint.Parse("127.0.0.1:212"));

            if (TCP_Connection.Connected) {
                Console.WriteLine("Connected to server");
            } else {
                Console.WriteLine("Failed to connect to server");
                return;
            }

            MainSocket.BeginReceive(BufferMemort, 0, 1024, SocketFlags.None, new AsyncCallback(ReciveBytes), null);

        }

        void ReciveBytes(IAsyncResult result) {
            
            int RecivedBytes = TCP_Connection.EndReceive(result);
            byte[] Message = new byte[RecivedBytes];

            Buffer.BlockCopy(BufferMemort, 0, Message, 0, RecivedBytes);



        }

    }
}
