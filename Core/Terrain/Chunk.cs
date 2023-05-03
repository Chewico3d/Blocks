using Core.Enviroment;
using Core.Rendering;
using Core.Rendering.Essentials;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.ServerConnection;

namespace Core.Terrain
{
    internal class Chunk {
        internal Vector3 IDPos;
        internal byte[,,] Voxels = new byte[UniversalParameters.ChunkSize[0] + 2, UniversalParameters.ChunkSize[1] + 2, UniversalParameters.ChunkSize[2] + 2];

        ChunkRender Render;

        internal Chunk(Vector3 IDPos) {
            //Improve
            this.IDPos = IDPos;
            LoadData();
            Render = new ChunkRender(IDPos);
            Create();

        }
        ~Chunk() {
            Render.RenderDetach();
        }

        internal void LoadData() {
            ChunkConnection.GetChunk((Vector3i)IDPos);
        }
        internal void Create() {
            for(int x = 1; x < 17; x++) {
                for(int y = 1; y < 17; y++) {
                    Voxels[x, 1, y] = 1;
                }
            }
            Render.CalculateChunkBase(Voxels);

        }


    }
}
