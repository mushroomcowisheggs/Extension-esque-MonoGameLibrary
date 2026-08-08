using MonoGameLibrary.Core.Content;

namespace MonoGameLibrary.Extensions.Graphics {
    /// <summary>
    /// Base contrast for all texture resources. 
    /// </summary>
    public interface ITexture : IAsset {
        /// <summary>Gets the width of the texture in pixels. </summary>
        int Width { get; }
        
        /// <summary>Gets the height of the texture in pixels. </summary>
        int Height { get; }
    }
}