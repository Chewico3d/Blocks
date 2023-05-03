using Core.Rendering;
using Core.Rendering.Essentials;
using OpenTK.Graphics.OpenGL4;
using Core.Terrain;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Core.Rendering {
    internal class RenderController {

        internal ChunkRenderController chunkRenderController = new ChunkRenderController();
        internal Camera ActiveCamera = new Camera();

        internal RenderController() {
            GL.Enable(EnableCap.CullFace);
        }

        //Here are the render passes
        internal void Render() {
            ActiveCamera.CalculateCameraMatrix();

            chunkRenderController.Render();
            
        }
    }
}
