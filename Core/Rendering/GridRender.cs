using Core.Enviroment;
using Core.Rendering.Essentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Core.Rendering {
    internal static class GridRender {
        internal static SimpMeshInfo Calculate(byte[,,] Voxels) {
            //The result is in bytes and it consumes less memory in GPU
            List<byte> VerticesPositions = new List<byte>();
            List<byte> VerticesAmbientOclusion = new List<byte>();
            List<int> Triangles = new List<int>();

            byte AmbientOclusionValue(int x, int y, int z, int face, int vertex) {
                float AmbientOclusionValue = 1;
                int[][] CheckPositions = CubeInfo.VertexRelativeCubesDirection(face, vertex);

                for(int rep = 0; rep < 3; rep++) {
                    int Ck_X = CheckPositions[rep][0] + x;
                    int Ck_Y = CheckPositions[rep][1] + y;
                    int Ck_Z = CheckPositions[rep][2] + z;

                    if (Voxels[Ck_X, Ck_Y, Ck_Z] == 1) {
                        AmbientOclusionValue -= .2f;
                    }

                }
                byte Value = (byte)(AmbientOclusionValue * 255);

                return Value;
            }

            void CheckPoisition(int x, int y, int z) {
                if (Voxels[x, y, z] != 1)
                    return;

                for (int t = 0; t < 6; t++) {
                    int Ck_X = CubeInfo.CheckPositions[t][0] + x;
                    int Ck_Y = CubeInfo.CheckPositions[t][1] + y;
                    int Ck_Z = CubeInfo.CheckPositions[t][2] + z;

                    if (Voxels[Ck_X, Ck_Y, Ck_Z] != 1) {
                        byte[] AmbientOC = new byte[4];

                        //Calculate the ambient oclusion
                        for (int vertx = 0; vertx < 4; vertx++)
                            AmbientOC[vertx] = AmbientOclusionValue(x, y, z, t, vertx);

                        //Orientation
                        VerticesAmbientOclusion.AddRange(AmbientOC);
                        if (AmbientOC[0] + AmbientOC[3] > AmbientOC[1] + AmbientOC[2]) {
                            Triangles.Add(VerticesPositions.Count / 3 + 0);
                            Triangles.Add(VerticesPositions.Count / 3 + 1);
                            Triangles.Add(VerticesPositions.Count / 3 + 3);

                            Triangles.Add(VerticesPositions.Count / 3 + 3);
                            Triangles.Add(VerticesPositions.Count / 3 + 2);
                            Triangles.Add(VerticesPositions.Count / 3 + 0);
                        } else {
                            Triangles.Add(VerticesPositions.Count / 3 + 0);
                            Triangles.Add(VerticesPositions.Count / 3 + 1);
                            Triangles.Add(VerticesPositions.Count / 3 + 2);

                            Triangles.Add(VerticesPositions.Count / 3 + 2);
                            Triangles.Add(VerticesPositions.Count / 3 + 1);
                            Triangles.Add(VerticesPositions.Count / 3 + 3);
                        }
                        VerticesPositions.AddRange(CubeInfo.GetVerticesPosition(x - 1, y - 1, z - 1, t));

                    }

                }

            }

            for (int x = 1; x < UniversalParameters.ChunkSize[0] + 1; x++)
                for (int y = 1; y < UniversalParameters.ChunkSize[1] + 1; y++)
                    for (int z = 1; z < UniversalParameters.ChunkSize[2] + 1; z++) {

                        //Check if the chunk we need a face
                        CheckPoisition(x, y, z);
                    }

            //textureCordinates
            float[] UVs = new float[VerticesPositions.Count];
            for (int x = 0; x < UVs.Length / (4 * 2); x++) {
                UVs[x * 8 + 0] = 0;
                UVs[x * 8 + 1] = 0;

                UVs[x * 8 + 2] = 0;
                UVs[x * 8 + 3] = 1;

                UVs[x * 8 + 4] = 1;
                UVs[x * 8 + 5] = 0;

                UVs[x * 8 + 6] = 1;
                UVs[x * 8 + 7] = 1;
            }

            SimpMeshInfo meshInfo = new SimpMeshInfo() {
                Vert_Position = VerticesPositions.ToArray(),
                Indices = Triangles.ToArray(),
                Vert_Texture = UVs,
                Vert_AmbientOclusion = VerticesAmbientOclusion.ToArray()
            };
            return meshInfo;

        }


    }
}
