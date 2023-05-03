using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Rendering.Essentials {
    //This is a version on MeshInfo that uses less memory
    //The Mesh info uses 35 bytes per vertex 
    //This uses 17 bytes per vertex
    internal class SimpMeshInfo {

        internal int[] Indices;
        internal byte[] Vert_Position;
        internal float[] Vert_Texture;
        internal byte[] Vert_Color;
        internal sbyte[] Vert_Normal;
        internal byte[] Vert_AmbientOclusion;

        internal int Vert_Position_Length => Vert_Position.Length;
        internal int Vert_Texture_Length => Vert_Texture == null ? 0 : Vert_Texture.Length;
        internal int Vert_Color_Length => Vert_Color == null ? 0 : Vert_Color.Length;
        internal int Vert_Normal_Length => Vert_Normal == null ? 0 : Vert_Normal.Length;
        internal int Vert_AmbientOclusion_Length => Vert_AmbientOclusion == null ? 0 : Vert_AmbientOclusion.Length;

        internal int PossitionOffset { get { return 0; } }
        internal int TextureOffset { get { return Vert_Position_Length * sizeof(byte); } }
        internal int ColorOffset { get { return TextureOffset + Vert_Texture_Length * sizeof(float); } }
        internal int NormalOffset { get { return ColorOffset + Vert_Color_Length * sizeof(byte); } }
        internal int AmbientOclusionOffset { get { return NormalOffset + Vert_Normal_Length * sizeof(sbyte); } }

        internal byte[] GetCompactVertexArray() {
            byte[] VertexArray = new byte[Vert_Position_Length * sizeof(byte) + Vert_Texture_Length * sizeof(float) + Vert_Color_Length * sizeof(byte) + Vert_Normal_Length * sizeof(sbyte)
                + Vert_AmbientOclusion_Length * sizeof(byte)];

            Buffer.BlockCopy(Vert_Position, 0, VertexArray, PossitionOffset, Vert_Position.Length * sizeof(byte));
            if (Vert_Texture != null)
                Buffer.BlockCopy(Vert_Texture, 0, VertexArray, TextureOffset, Vert_Texture.Length * sizeof(float));
            if (Vert_Color != null)
                Buffer.BlockCopy(Vert_Color, 0, VertexArray, ColorOffset, Vert_Color.Length * sizeof(byte));
            if (Vert_Normal != null)
                Buffer.BlockCopy(Vert_Normal, 0, VertexArray, NormalOffset, Vert_Normal.Length * sizeof(sbyte));
            if (Vert_AmbientOclusion != null)
                Buffer.BlockCopy(Vert_AmbientOclusion, 0, VertexArray, NormalOffset, Vert_AmbientOclusion.Length * sizeof(byte));

            return VertexArray;
        }

    }
}
