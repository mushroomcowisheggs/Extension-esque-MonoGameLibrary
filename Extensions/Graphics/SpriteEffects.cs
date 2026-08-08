using System;

namespace MonoGameLibrary.Extensions.Graphics {
    /// <summary>
    /// Flags describing sprite render effects. 
    /// </summary>
    [Flags]
    public enum SpriteEffects {
        /// <summary>No effects applied.</summary>
        None = 0,
        
        /// <summary>Flip the sprite horizontally.</summary>
        FlipHorizontally = 1,
        
        /// <summary>Flip the sprite vertically.</summary>
        FlipVertically = 2,
    }
}