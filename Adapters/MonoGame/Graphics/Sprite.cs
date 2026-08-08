using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Core.Primitives;
using MonoGameLibrary.Extensions.Graphics;

namespace MonoGameLibrary.Adapters.MonoGame.Graphics {
    /// <summary>
    /// A simple sprite wrapper around a texture region. 
    /// </summary>
    public class Sprite {
        /// <summary>
        /// Gets or sets the region this sprite renders. 
        /// </summary>
        public TextureRegion Region { get; set; }
        
        /// <summary>
        /// Gets or sets the tint color. 
        /// </summary>
        public Microsoft.Xna.Framework.Color Color { get; set; } = Microsoft.Xna.Framework.Color.White;
        
        /// <summary>
        /// Gets or sets the scale. 
        /// </summary>
        public Vector2 Scale { get; set; } = Vector2.One;
        
        /// <summary>
        /// Gets or sets the origin. 
        /// </summary>
        public Vector2 Origin { get; set; } = Vector2.Zero;
        
        /// <summary>
        /// Gets the width in pixels. 
        /// </summary>
        public float Width { get {
            if (Region == null) {
                return 0f;
            } else {
                return Region.Width * Scale.X;
            }
        } }
        
        /// <summary>
        /// Gets the height in pixels. 
        /// </summary>
        public float Height { get {
            if (Region == null) {
                return 0f;
            } else {
                return Region.Height * Scale.Y;
            }
        } }
        
        /// <summary>
        /// Creates a new sprite. 
        /// </summary>
        public Sprite() {
        }
        
        /// <summary>
        /// Creates a new sprite with the provided region. 
        /// </summary>
        public Sprite(TextureRegion region) {
            Region = region;
        }
        
        /// <summary>
        /// Draws the sprite with the supplied sprite batch. 
        /// </summary>
        /// <param name="contextRender">The RenderContext instance used for draw calls. </param>
        /// <param name="position">The xy-coordinate position to render this sprite at. </param>
        public void Draw(IRenderContext contextRender, Vector2 position) {
            if (contextRender == null || Region == null || Region.Texture == null) {
                return;
            }
            
            var color = new Core.Primitives.Color(Color.R, Color.G, Color.B, Color.A);
            var vectorOrigin = new TwoDimensionalVector(Origin.X, Origin.Y);
            var vectorScale = new TwoDimensionalVector(Scale.X, Scale.Y);
            var vectorPosition = new TwoDimensionalVector(position.X, position.Y);
            
            var rectangleSource = new Core.Primitives.Rectangle(
                Region.SourceRectangle.X,
                Region.SourceRectangle.Y,
                Region.SourceRectangle.Width,
                Region.SourceRectangle.Height
            );
            
            Region.Texture.DrawInto(
                contextRender,
                vectorPosition,
                new OptionalValue<Core.Primitives.Rectangle>(rectangleSource),
                color,
                0f,
                vectorOrigin,
                vectorScale,
                MonoGameLibrary.Extensions.Graphics.SpriteEffects.None,
                0f
            );
        }
    }
}
