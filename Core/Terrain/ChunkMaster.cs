using Core.Enviroment;
using Core.Rendering;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.Terrain {
    internal class ChunkMaster {
        //Chunks enabled and in render use
        Dictionary<Vector3, Chunk> ActiveChunks = new Dictionary<Vector3, Chunk>();
        //Chunks in memory but not in render or calculate mode
        Dictionary<Vector3, Chunk> DisabledChunks = new Dictionary<Vector3, Chunk>();
        //All chunks in memory
        List<Chunk> ExistingChunks = new List<Chunk>();

        internal ChunkMaster() {
        }

        internal void Update() {
            UpdateChunks();
        }
        void UpdateChunks() {
            int PositionX = (int)ChunkRenderController.IDCameraPos.X;
            int PositionZ = (int)ChunkRenderController.IDCameraPos.Z;
            for (int b = 0; b < LocalVariables.ChukDistance; b++) {
                for(int rep = 0; rep < b * 2 + 1; rep++) {
                    if (ActivateChunk(new Vector3(PositionX, 0, PositionZ)))
                        return;
                    PositionX++;
                }
                for (int rep = 0; rep < b * 2 + 1; rep++) {
                    if (ActivateChunk(new Vector3(PositionX, 0, PositionZ)))
                        return;
                    PositionZ++;
                }

                for (int rep = 0; rep < b * 2 + 2; rep++) {
                    if (ActivateChunk(new Vector3(PositionX, 0, PositionZ)))
                        return;
                    PositionX--;
                }

                for (int rep = 0; rep < b * 2 + 2; rep++) {
                    if (ActivateChunk(new Vector3(PositionX, 0, PositionZ)))
                        return;
                    PositionZ--;
                }
            }

            for (int rep = 0; rep < LocalVariables.ChukDistance * 2 + 1; rep++) {
                if (ActivateChunk(new Vector3(PositionX, 0, PositionZ)))
                    return;
                PositionX++;
            }
        }

        bool ActivateChunk(Vector3 IDPos) {
            Chunk chunk;
            if(ActiveChunks.TryGetValue(IDPos, out chunk)) {

                return false;
            }
            ActiveChunks.Add(IDPos, new Chunk(IDPos));
            return true;

        }

    }
}
