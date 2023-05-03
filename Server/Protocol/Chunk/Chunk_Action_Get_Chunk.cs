using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Protocol
// T A XXXX YYYY ZZZZ
// Target is Chunk
// Action is Get chunk
// Var is x y and z in integer

namespace Server.Protocol.Chunk {
    internal class Chunk_Action_Get_Chunk : MessageAction {

        int X, Y, Z;
        internal Chunk_Action_Get_Chunk(byte[] MessageData) {
            if(MessageData.Length != 4 * 3 + 2) {
                ErrorFormat = true;
                ErrorMessage = "Error in Chunk_Action_Get_Chunk, incorrect format";
                return;
            }

            X = BitConverter.ToInt32(MessageData, 0 + 2);
            Y = BitConverter.ToInt32(MessageData, 4 + 2);
            Z = BitConverter.ToInt32(MessageData, 8 + 2);

        }

        protected override void Execution() {
            Console.WriteLine("Sending chunks... " + X + " " + Y + " " + Z);
        }
    }

}
