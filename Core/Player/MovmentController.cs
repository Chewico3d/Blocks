using Core.Player.Movment;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Player {
    internal class MovmentController {

        internal static MovmentController mainMovmentController => BlocksGame.Instance.playerController.MovmentController;

        internal FlyMovment flyMovment = new FlyMovment();
        internal bool FreeMovment = true;

        internal Vector2 MouseDelta;
        internal float MouseSensivity = .02f;

        Vector2 LastPosition;
        internal void Update() {
            MouseDelta = BlocksGame.Instance.MouseState.Delta;

            if (FreeMovment)
                flyMovment.Move();

        }
    }
}
