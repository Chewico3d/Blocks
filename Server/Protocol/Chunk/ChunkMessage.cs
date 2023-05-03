using Common.ProtocolDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Protocol.Chunk {
    internal static class ChunkMessage {

        internal static MessageAction MessageAction(byte[] Data) {
            switch ((ChunkAction)Data[1]) {
                case ChunkAction.Get_Chunk:
                return new Chunk_Action_Get_Chunk(Data);
            }
            //not valid data
            return new MessageAction();
        }

    }
}
