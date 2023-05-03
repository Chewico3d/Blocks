using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.ChunkSystem;
using Server.ConectionHandle;

namespace Server {
    internal static class Program {

        internal static ChunkManager mainChunkManager = new ChunkManager();
        internal static ClientManager clientManager = new ClientManager();

        static void Main(string[] args) {

            clientManager.Start();

            Console.WriteLine("Starting server");

            while (true) {

            }

        }

    }
}
