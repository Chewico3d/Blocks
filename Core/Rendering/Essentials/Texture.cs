using System;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace Core.Rendering.Essentials
{
    internal class Texture
    {

        internal int Handle;
        internal Texture(string Path)
        {

            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult image = ImageResult.FromStream(File.OpenRead(Path), ColorComponents.RedGreenBlueAlpha);

            Handle = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, Handle);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        }

        internal void Bind(TextureUnit location = TextureUnit.Texture0) {
            GL.ActiveTexture(location);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

    }
}
