using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Input;
using System.Threading.Tasks;
using OpenTK.Windowing.Common.Input;
using Core.Rendering.Essentials;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Core.Player.Movment {
    internal class FlyMovment {
        internal Vector3 Position = new Vector3(0, 30, 0);
        internal Vector3 Rotation;

        internal void Move() {
            Rotation.X += -MovmentController.mainMovmentController.MouseDelta.Y * MovmentController.mainMovmentController.MouseSensivity;
            Rotation.Y += -MovmentController.mainMovmentController.MouseDelta.X * MovmentController.mainMovmentController.MouseSensivity;

            Camera.ActiveCamera.Rotation = Rotation;

            KeyboardState input = BlocksGame.Instance.KeyboardState;

            Vector3 Front = Vector3.Zero;
            Front.X = (float)Math.Sin(MathHelper.DegreesToRadians(Rotation.Y)) * (float)Math.Cos(MathHelper.DegreesToRadians(Rotation.X));
            Front.Y = -(float)Math.Sin(MathHelper.DegreesToRadians(Rotation.X));
            Front.Z = (float)Math.Cos(MathHelper.DegreesToRadians(Rotation.Y)) * (float)Math.Cos(MathHelper.DegreesToRadians(Rotation.X));
            Front = Vector3.Normalize(Front);

            Vector3 Left = Vector3.Cross(Front, Vector3.UnitY);

            if (input.IsKeyDown(Keys.W)) {
                Vector3 Direction = -Front;
                if(input.IsKeyDown(Keys.LeftShift))
                    Position += Direction * .7f;
                Position += Direction * .02f;
            }

            if (input.IsKeyDown(Keys.S)) {
                Vector3 Direction = Front;
                if (input.IsKeyDown(Keys.LeftShift))
                    Position += Direction * .7f;
                Position += Direction * .02f;
            }

            if (input.IsKeyDown(Keys.D)) {
                Vector3 Direction = -Left;
                if (input.IsKeyDown(Keys.LeftShift))
                    Position += Direction * .7f;
                Position += Direction * .02f;
            }

            if (input.IsKeyDown(Keys.A)) {
                Vector3 Direction = Left;
                if (input.IsKeyDown(Keys.LeftShift))
                    Position += Direction * .7f;
                Position += Direction * .02f;
            }

            Camera.ActiveCamera.Position = Position;


        }
    }
}
