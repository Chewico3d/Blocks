using Core.Terrain;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Core.Rendering.Essentials
{
    internal class ObjectRender
    {
        //General Buffer
        int VertexBuffer;
        //Attributes
        int VertexArrayObject;
        int ElementBuffer;

        int TriangleCount;
        bool Destroyed = false;

        internal ObjectRender(MeshInfo mesh)
        {
            byte[] VertexPack = mesh.GetCompactVertexArray();
            TriangleCount = mesh.Indices.Length;
            VertexBuffer = GL.GenBuffer();
            //Create the buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexPack.Length * sizeof(byte), VertexPack, BufferUsageHint.StaticDraw);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            //-Vertices-
            //Position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), mesh.PossitionOffset);
            GL.EnableVertexAttribArray(0);
            //Texture
            if (mesh.Vert_Texture != null)
            {
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), mesh.TextureOffset);
                GL.EnableVertexAttribArray(1);
            }
            if (mesh.Vert_Color != null)
            {
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.UnsignedByte, false, 3 * sizeof(byte), mesh.ColorOffset);
                GL.EnableVertexAttribArray(2);
            }
            if (mesh.Vert_Normal != null)
            {
                GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), mesh.NormalOffset);
                GL.EnableVertexAttribArray(3);
            }

            //Triangles
            ElementBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, mesh.Indices.Length * sizeof(int), mesh.Indices, BufferUsageHint.DynamicDraw);

        }
        internal ObjectRender(SimpMeshInfo mesh) {
                
            TriangleCount = mesh.Indices.Length;
            byte[] VertexPack = mesh.GetCompactVertexArray();
            VertexBuffer = GL.GenBuffer();
            //Create the buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexPack.Length * sizeof(byte), VertexPack, BufferUsageHint.StaticDraw);
        
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
        
            //-Vertices-
            //Position
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.UnsignedByte, false, 3 * sizeof(byte), mesh.PossitionOffset);
            GL.EnableVertexAttribArray(0);
            //Texture
            if (mesh.Vert_Texture != null) {
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), mesh.TextureOffset);
                GL.EnableVertexAttribArray(1);
            }
            if (mesh.Vert_Color != null) {
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.UnsignedByte, false, 3 * sizeof(byte), mesh.ColorOffset);
                GL.EnableVertexAttribArray(2);
            }
            if (mesh.Vert_Normal != null) {
                GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Byte, false, 3 * sizeof(sbyte), mesh.NormalOffset);
                GL.EnableVertexAttribArray(3);
            }
            if (mesh.Vert_AmbientOclusion != null) {
                GL.VertexAttribPointer(4, 1, VertexAttribPointerType.UnsignedByte, false, 1 * sizeof(byte), mesh.AmbientOclusionOffset);
                GL.EnableVertexAttribArray(4);
            }

            //Triangles
            ElementBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, mesh.Indices.Length * sizeof(int), mesh.Indices, BufferUsageHint.DynamicDraw);
        
        }

        internal void Destroy() {
            if (Destroyed)
                return;
            Destroyed = true;
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(ElementBuffer);
        }

        ~ObjectRender() {
            if (Destroyed)
                return;
            Destroyed = true;
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DeleteBuffer(ElementBuffer);
        }

        internal void Render()
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer);
            GL.DrawElements(PrimitiveType.Triangles, TriangleCount, DrawElementsType.UnsignedInt, 0);
        }

    }
}
