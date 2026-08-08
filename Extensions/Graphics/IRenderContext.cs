using System;
using MonoGameLibrary.Core;
using MonoGameLibrary.Core.Primitives;

namespace MonoGameLibrary.Extensions.Graphics {
    /// <summary>
    /// Abstraction over a 2D rendering context. 
    /// Manages rendering state (begin/end, clear) but does not directly draw primitives. 
    /// Drawing is delegated to asset objects via double dispatch. 
    /// </summary>
    public interface IRenderContext : IDisposable {
        /// <summary>
        /// Begins a rendering block. All draw operations must occur between <see cref="Begin"/> and <see cref="End"/>. 
        /// </summary>
        /// <param name="stateSampler">Optional sampler state (point, linear, etc.). Defaults to point clamping if not specified. </param>
        /// <param name="stateBlend">Optional blend state. Defaults to alpha blending if not specified. </param>
        void Begin(
            Optional<SamplerState> stateSampler = default, 
            Optional<BlendState> stateBlend = default, 
            Optional<IEffect> effect = default
        );
        
        /// <summary>Ends the rendering block and flushes all buffered draw calls. </summary>
        void End();
        
        /// <summary>Clears the entire render target to the specified given color. </summary>
        void Clear(Color color);
        
        /// <summary>
        /// Accepts a visitor and allows it to perform drawing operations on this context. 
        /// The visitor can safely access the underlying rendering engine through pattern matching. 
        /// </summary>
        /// <param name="visitor">The visitor to accept.</param>
        void Accept(IVisitor visitor);
    }
}