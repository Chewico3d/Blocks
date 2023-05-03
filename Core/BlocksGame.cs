using Core.Enviroment;
using Core.Player;
using Core.Rendering;
using Core.ServerConnection;
using Core.Terrain;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Core {
    public class BlocksGame : GameWindow {

        internal static BlocksGame Instance;
        //Here we declare the top masters of every section
        internal ChunkMaster chunkMaster;
        internal RenderController renderController;
        internal PlayerController playerController;
        internal EnviromentController enviromentController;
        internal ConnectionManager connectionManager;
        internal float AspectRatio;

        public BlocksGame(int width, int height, string title)
            : base(new GameWindowSettings() { RenderFrequency = 60, UpdateFrequency = 50}
                  , new NativeWindowSettings() { Size = (width, height), Title = title }) {

            CursorState = CursorState.Grabbed;
            Instance = this;

            chunkMaster = new ChunkMaster();
            renderController = new RenderController();
            playerController = new PlayerController();
            enviromentController = new EnviromentController();
            connectionManager = new ConnectionManager();

            GL.Enable(EnableCap.DepthTest);
            VSync = VSyncMode.On;
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            AspectRatio = ((float)width) / ((float)height);
            
        }

        protected override void OnUpdateFrame(FrameEventArgs args) {
            playerController.Update();
            chunkMaster.Update();
            
            base.OnUpdateFrame(args);

        }
         
        protected override void OnRenderFrame(FrameEventArgs args) {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            renderController.Render();
            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e) {
            AspectRatio = (float)e.Size.X / (float)e.Size.Y;
            GL.Viewport(0,0,e.Size.X, e.Size.Y);
            base.OnResize(e);
        }

    }
}