using Server.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.ConectionHandle {
    internal class ClientConnection {
        internal Socket ClientSocket;
        internal byte[] ReciveBuffer = new byte[1024];
        internal ClientConnection(Socket clientSocket){
            ClientSocket = clientSocket;
            clientSocket.BeginReceive(ReciveBuffer, 0, ReciveBuffer.Length, SocketFlags.None, new AsyncCallback(ReciveInfo), null);

        }

        void ReciveInfo(IAsyncResult data) {
            int Size = ClientSocket.EndReceive(data);
            byte[] Data = new byte[Size];

            Buffer.BlockCopy(ReciveBuffer, 0, Data, 0, Size);
            MessageInfo message = new MessageInfo(Data);

            message.Execute();

            ClientSocket.BeginReceive(ReciveBuffer, 0, ReciveBuffer.Length, SocketFlags.None, new AsyncCallback(ReciveInfo), null);

        }   

    }
}
