using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Core.Content;
using MonoGameLibrary.Extensions.Graphics;

namespace MonoGameLibrary.Adapters.MonoGame.Graphics {
    public static class GraphicsLoaders {
        /// <summary>
        /// Creates a loader for the specified graphics asset type.
        /// Supported types: ITwoDimensionalTexture, IFont.
        /// </summary>
        public static Func<string, IAsset> CreateLoader<T>(ContentManager managerContent) where T : class, IAsset {
            if (typeof(T) == typeof(ITwoDimensionalTexture)) {
                return delegate(string name) { return new TwoDimensionalTexture(managerContent.Load<Texture2D>(name)); };
            }
            if (typeof(T) == typeof(IFont)) {
                return delegate(string name) { return new MonoGameLibrary.Adapters.MonoGame.Graphics.SpriteFont(managerContent.Load<Microsoft.Xna.Framework.Graphics.SpriteFont>(name)); };
            }
            if (typeof(T) == typeof(IEffect)) {
                return delegate(string name) { return new Effect(managerContent.Load<Microsoft.Xna.Framework.Graphics.Effect>(name)); };
            }
            throw new NotSupportedException($"Graphics loader does not support asset type {typeof(T).FullName}.");
        }
    }
}