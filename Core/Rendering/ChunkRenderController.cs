using Core.Enviroment;
using Core.Rendering.Essentials;
using Core.Terrain;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Common;

namespace Core.Rendering {
    internal class ChunkRenderController {
        //All chuks that want to be rendered
        internal static List<ChunkRender> ActiveChunks
        = new List<ChunkRender>();

        internal static Vector3 IDCameraPos => new Vector3 (MathF.Floor(Camera.ActiveCamera.Position.X / (float)UniversalParameters.ChunkSize[0]),
            MathF.Floor(Camera.ActiveCamera.Position.Y / (float)UniversalParameters.ChunkSize[1]),
            MathF.Floor(Camera.ActiveCamera.Position.Z / (float)UniversalParameters.ChunkSize[2]));

        Texture tex;
        Shader shad;
        Material material;

        internal ChunkRenderController() {

            tex = new Texture(@"C:\Chewico\Projects\EnviromentPack\Textures\Grass.png");
            tex.Bind(TextureUnit.Texture0);
            shad = new Shader(@"C:\Chewico\Projects\EnviromentPack\Shaders\Simple.vert", @"C:\Chewico\Projects\EnviromentPack\Shaders\Simple.frag");
            shad.Bind();
            shad.SetUniform("texture0", 0);
            shad.Bind();
            material = new Material(shad);
        }

        //Main render input
        static int rot;
        internal void Render() {
            rot++;
            ChunkRender.LoadGPU();

            for (int id = 0; id < ActiveChunks.Count; id++) {
                material.Use(ActiveChunks[id].Position);
                ActiveChunks[id].Render();
            }
        }
    }
}
