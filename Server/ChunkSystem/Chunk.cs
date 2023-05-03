using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Threading.Tasks;
using System.Numerics;

namespace Server.ChunkSystem {
    internal class Chunk {
        internal byte[] ChunkData = new byte[UniversalParameters.ChunkSize[0] * 
            UniversalParameters.ChunkSize[1] * 
            UniversalParameters.ChunkSize[2]];
        internal iV3 ChunkID = new iV3();

        internal static Chunk GenerateChunk(iV3 chunkID) {
            Chunk chunk = new Chunk(chunkID);
            FastNoise fastNoise = new FastNoise();

            for (int x = 0; x < UniversalParameters.ChunkSize[0]; x++) {
                for (int z = 0; z < UniversalParameters.ChunkSize[2]; z++) {

                    float noiseValue = fastNoise.GetValueFractal(x + chunkID.x * UniversalParameters.ChunkSize[0], z + chunkID.z * UniversalParameters.ChunkSize[2]);
                    noiseValue = noiseValue * 60 + 60;
                    noiseValue += 1;


                    // = 0;
                    for (int y = 0; y < UniversalParameters.ChunkSize[1] + 2; y++) {
                        chunk.ChunkData[x + y * UniversalParameters.ChunkSize[1] +
                            z * UniversalParameters.ChunkSize[2] * UniversalParameters.ChunkSize[1]] = (y > (float)noiseValue) ? (byte)0 : (byte)1;

                    }
                }
            }
            return chunk;

        }

        Chunk (iV3 Position) {
            ChunkID = Position;
        }
        
    }
}
