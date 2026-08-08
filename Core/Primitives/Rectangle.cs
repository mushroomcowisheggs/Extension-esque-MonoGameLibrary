namespace MonoGameLibrary.Core.Primitives {
    /// <summary>
    /// The axis-aligned rectangle. 
    /// </summary>
    public struct Rectangle {
        /// <summary>X coordinate of the top-left corner.</summary>
        public int X;
        
        /// <summary>Y coordinate of the top-left corner.</summary>
        public int Y;
        
        /// <summary>Width of the rectangle.</summary>
        public int Width;
        
        /// <summary>Height of the rectangle.</summary>
        public int Height;
        
        /// <summary>Creates a new rectangle from position and size.</summary>
        public Rectangle(int x, int y, int width, int height) {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        
        /// <summary>Gets the Y coordinate of the bottom edge.</summary>
        public int Bottom { get { return Y + Height; } }
        
        /// <summary>Gets the X coordinate of the right edge.</summary>
        public int Right { get { return X + Width; } }
        
        /// <summary>Checks whether the given point lies inside this rectangle.</summary>
        public bool Contains(int x, int y) { return x >= X && x < Right && y >= Y && y < Bottom; }
    }
}