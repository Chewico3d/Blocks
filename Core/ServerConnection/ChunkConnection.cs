using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ProtocolDefinitions;

namespace Core.ServerConnection {
    internal static class ChunkConnection {

        
        internal static void GetChunk(Vector3i IDPos) {

            //here we define the protocol to send
            byte[] DataRequest = new byte[2+4*3];
            DataRequest[0] = (byte)SystemTarget.Chunk;
            DataRequest[1] = (byte)ChunkAction.Get_Chunk;

            BitConverter.GetBytes(IDPos.X).CopyTo(DataRequest, 2);
            BitConverter.GetBytes(IDPos.Y).CopyTo(DataRequest, 2 + 4 * 1);
            BitConverter.GetBytes(IDPos.Z).CopyTo(DataRequest, 2 + 4 * 2);

            ConnectionManager.MainSocket.Send(DataRequest);


        }
    }
}
