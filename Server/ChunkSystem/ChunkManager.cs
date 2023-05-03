using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ChunkSystem {
    internal class ChunkManager {
        internal Dictionary<iV3, Chunk> ChunkDatabase = new Dictionary<iV3, Chunk>();

        internal Chunk GetChunk(iV3 chunkID) {
            Chunk target;
            if(ChunkDatabase.TryGetValue(chunkID, out target)) {
                return target;
            }

            target = Chunk.GenerateChunk(chunkID);
            ChunkDatabase.Add(chunkID, target);
            return target;
        }

    }

    internal struct iV3 {
        internal int x, y, z;
        internal iV3(int X = 0, int Y = 0, int Z = 0) {
            x = X; y = Y; z = Z;
        }
    }
}
