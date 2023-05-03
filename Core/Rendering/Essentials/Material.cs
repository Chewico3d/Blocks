using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using System.Diagnostics;

namespace Core.Rendering.Essentials {
    internal class Material {

        internal Shader RefShader { get; private set; }
        private int TransformLocation;

        internal Material(Shader shader) {
            RefShader = shader;

            RefShader.Bind();
            TransformLocation = GL.GetUniformLocation(RefShader.Handle, "transform");
        }

        internal void Use(Vector3 Position) {
            Matrix4 PositionMatrix = Matrix4.CreateTranslation(Position);
            Matrix4 FinalPos = PositionMatrix * Camera.ActiveCamera.CameraMatrix;
            Matrix4 current = Matrix4.Identity;
            RefShader.Bind();
            GL.UniformMatrix4(TransformLocation, true, ref FinalPos);
        }

        //Not implemented yet
        internal class MaterialProperty {
            internal string Name { get; set; }
            internal string Value { get; set; }
            internal int AttribID { get; set; }

        }
    }
}
