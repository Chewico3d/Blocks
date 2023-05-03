using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;
using System.Text;
using System.Threading.Tasks;
using Core.Enviroment;
using Core.Rendering.Essentials;
using Common;

namespace Core.Rendering
{
    internal class ChunkRender {
        internal ChunkRender(Vector3 IDPos) {
            ChunkRenderController.ActiveChunks.Add(this);
            Position = new Vector3(IDPos.X * UniversalParameters.ChunkSize[0],
                IDPos.Y * UniversalParameters.ChunkSize[1],
                IDPos.Z * UniversalParameters.ChunkSize[2]);
        }
        internal void RenderDetach() {
            ChunkRenderController.ActiveChunks.Remove(this);
        }

        internal ObjectRender ChunkBase;
        internal Vector3 Position;
        internal byte[,,] voxels;

        internal SimpMeshInfo localMeshInfo = new SimpMeshInfo();
        static Queue<ChunkRender> GL_SetUP = new Queue<ChunkRender>();

        internal static void LoadGPU() {
            if (GL_SetUP.Count == 0)
                return;
            ChunkRender chunk = GL_SetUP.Dequeue();
            chunk.ChunkBase = new ObjectRender(chunk.localMeshInfo);
        }
        
        internal void CalculateChunkBase(byte[,,] Voxels) {

            if (ChunkBase != null)
                ChunkBase.Destroy();
            voxels = Voxels;
            //Generate(null);
            ThreadPool.QueueUserWorkItem(Generate);

        }

        void Generate(object objectState) {
            localMeshInfo = GridRender.Calculate(voxels);
            GL_SetUP.Enqueue(this);
        }

        internal void Render() {
            if (ChunkBase == null)
                return;

            ChunkBase.Render();
            
        }
    }
}
