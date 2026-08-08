namespace MonoGameLibrary.Extensions.Graphics {
    /// <summary>
    /// A cube texture resource with 6 faces. 
    /// </summary>
    public interface ICubeTexture : ITexture {
        /// <summary>Gets the size of the cube texture. </summary>
        int Size { get; }
    }
}