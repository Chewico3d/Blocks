using Server.Protocol.Chunk;
using Server.Protocol.WorldInfo;
using Common.ProtocolDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Protocol
{
    public class MessageInfo
    {
        //System like chunk or other
        internal SystemTarget Target;
        //Based on system
        internal MessageAction Action;

        public MessageInfo(byte[] Data) {

            Target = (SystemTarget)Data[0];


            switch ((SystemTarget)Target) {
                case SystemTarget.Chunk:
                Action = ChunkMessage.MessageAction(Data);
                break;
                case SystemTarget.WorldInfo:
                Action = MessageAction.InvalidMessageAction();
                break;
                default:
                Action = MessageAction.InvalidMessageAction();
                break;
            }

            

        }

        internal void Execute() {
            Action.Execute();
        }
    }
}
