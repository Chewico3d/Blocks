using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Rendering.Essentials {
    internal class Camera {
        internal Vector3 Position;
        internal Vector3 Rotation;
        internal float Fov = 60;
        internal float AspectRatio => BlocksGame.Instance.AspectRatio == 0? 1 : BlocksGame.Instance.AspectRatio;
        internal float NearField = .1f;
        internal float FarField = 1000;

        internal Matrix4 CameraMatrix;
        static internal Camera ActiveCamera => BlocksGame.Instance.renderController.ActiveCamera;
        
        internal void CalculateCameraMatrix() {
            Matrix4 WorldPos = Matrix4.CreateTranslation(-Position.X, -Position.Y, -Position.Z);
            Matrix4 RotationMatrix = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-Rotation.Y))
                * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-Rotation.X))
                * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-Rotation.Z));
            Matrix4 ViewMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Fov), AspectRatio, NearField,FarField);

            CameraMatrix = WorldPos * RotationMatrix * ViewMatrix;
        }

    }
}
