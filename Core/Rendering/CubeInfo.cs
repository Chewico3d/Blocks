using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Rendering {
    internal static class CubeInfo {
        internal static readonly byte[][] CubeVerticesPosition = {
            //-X
            new byte[] { 
                0,0,1,
                0,1,1,
                0,0,0,
                0,1,0
            },
            //+X
            new byte[] {
                1, 0, 0,
                1, 1, 0,
                1, 0, 1,
                1, 1, 1
            },
            //-Y
            new byte[] {
                0,0,1,
                0,0,0,
                1,0,1,
                1,0,0
            },
            //+Y
            new byte[] {
                0,1,0,
                0,1,1,
                1,1,0,
                1,1,1
            },
            //-Z
            new byte[] {
                0,0,0,
                0,1,0,
                1,0,0,
                1,1,0
            },
            //+Z
            new byte[] {
                1,0,1,
                1,1,1,
                0,0,1,
                0,1,1
            }
        };
        internal static readonly int[][] CheckPositions = {
            new int[] {-1, 0, 0},
            new int[] { 1, 0, 0},
            new int[] { 0,-1, 0},
            new int[] { 0, 1, 0},
            new int[] { 0, 0,-1},
            new int[] { 0, 0, 1}
        };
        internal static readonly int[] Triangles = { 0, 1, 2, 2, 1, 3 };

        internal static byte[] GetVerticesPosition(int xOffset, int yOffset, int zOffset, int t) {
            byte[] Result = new byte[4 * 3];
            for(int x = 0; x < 4; x++) {
                Result[x * 3 + 0] = (byte)(xOffset + CubeVerticesPosition[t][x * 3 + 0]);
                Result[x * 3 + 1] = (byte)(yOffset + CubeVerticesPosition[t][x * 3 + 1]);
                Result[x * 3 + 2] = (byte)(zOffset + CubeVerticesPosition[t][x * 3 + 2]);
            }
            return Result;
        }

        //Its a complex script that aims to get the relative position of the chunks that it has to check if its empty
        //To calculate the ambient oclusion
        internal static int[][] VertexRelativeCubesDirection(int face, int vertex) {
            //Here we have the face direction
            int[] Up = new int[3];
            Up = CheckPositions[face];

            int[] Direction1 = new int[3];
            int[] Direction2 = new int[3];

            //Here we calculate the first direction to look at
            for(int rep = 0; rep < 2; rep++) {
                if (Up[rep] == 0 ) {
                    if (CubeVerticesPosition[face][vertex * 3 + rep] == 0)
                        Direction1[rep] = -1;
                    else
                        Direction1[rep] = 1;
                    break;
                }

            }
            //Here we calculate the second direction to look at
            for (int rep = 1; rep < 3; rep++) {
                if (Up[rep] == 0 && Direction1[rep] == 0) {
                    if (CubeVerticesPosition[face][vertex * 3 + rep] == 0)
                        Direction2[rep] = -1;
                    else
                        Direction2[rep] = 1;
                    break;
                }
            }

            //Now we have the up position, the first and second direction
            //We combine them
            int[][] Result = new int[3][] {
                new int[3] {
                    Up[0] + Direction1[0], Up[1] + Direction1[1], Up[2] + Direction1[2]
                },
                new int[3] {
                    Up[0] + Direction2[0], Up[1] + Direction2[1], Up[2] + Direction2[2]
                },
                new int[3] {
                    Up[0] + Direction2[0] + Direction1[0], Up[1] + Direction2[1] + Direction1[1], Up[2] + Direction2[2] + Direction1[2]
                }
            };


            return Result;
        }
    }
}
