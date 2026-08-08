using System;

namespace MonoGameLibrary.Core.Primitives {
    /// <summary>
    /// The 2D vector. 
    /// </summary>
    public struct TwoDimensionalVector {
        /// <summary>X component. </summary>
        public float X;
        
        /// <summary>Y component. </summary>
        public float Y;
        
        /// <summary>Creates a new vector from (x, y) components.</summary>
        public TwoDimensionalVector(float x, float y) {
            X = x;
            Y = y;
        }
        
        /// <summary>A vector with both components set to zero. </summary>
        public static TwoDimensionalVector Zero { get { return new TwoDimensionalVector(0f, 0f); } }
        
        /// <summary>A vector with both components set to one. </summary>
        public static TwoDimensionalVector One { get { return new TwoDimensionalVector(1f, 1f); } }
        
        /// <summary>A unit vector pointing along the X axis. </summary>
        public static TwoDimensionalVector UnitX { get { return new TwoDimensionalVector(1f, 0f); } }
        
        /// <summary>A unit vector pointing along the Y axis. </summary>
        public static TwoDimensionalVector UnitY { get { return new TwoDimensionalVector(0f, 1f); } }
        
        /// <summary>Returns a new vector with the same direction but unit length. </summary>
        public TwoDimensionalVector Normalize() {
            float length = (float)Math.Sqrt(X * X + Y * Y);
            if (length < float.Epsilon) { return Zero; }
            return new TwoDimensionalVector(X / length, Y / length);
        }
        
        /// <summary>Returns the length of this vector.</summary>
        public float Length() { return (float)Math.Sqrt(X * X + Y * Y); }
        
        /// <summary>Component-wise addition.</summary>
        public static TwoDimensionalVector operator +(TwoDimensionalVector a, TwoDimensionalVector b) { return new TwoDimensionalVector(a.X + b.X, a.Y + b.Y); }
        
        /// <summary>Component-wise subtraction.</summary>
        public static TwoDimensionalVector operator -(TwoDimensionalVector a, TwoDimensionalVector b) { return new TwoDimensionalVector(a.X - b.X, a.Y - b.Y); }
        
        /// <summary>Scalar multiplication.</summary>
        public static TwoDimensionalVector operator *(TwoDimensionalVector v, float s) { return new TwoDimensionalVector(v.X * s, v.Y * s); }
        
        /// <summary>Scalar division.</summary>
        public static TwoDimensionalVector operator /(TwoDimensionalVector v, float s) { return new TwoDimensionalVector(v.X / s, v.Y / s); }
    }
}