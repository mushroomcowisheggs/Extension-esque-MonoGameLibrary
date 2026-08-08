namespace MonoGameLibrary.Extensions.Graphics {
    /// <summary>
    /// Platform-agnostic blend state. Naming mirrors MonoGame's <c>BlendState</c>.
    /// </summary>
    public abstract class BlendState {
        protected BlendState() { }
        
        /// <summary>Standard alpha blending. </summary>
        public static BlendState AlphaBlend { get { return AlphaBlendState.Instance; } }
        
        /// <summary>Additive blending. </summary>
        public static BlendState Additive { get { return AdditiveState.Instance; } }
        
        /// <summary>Opaque (no blending). </summary>
        public static BlendState Opaque { get { return OpaqueState.Instance; } }
        
        /// <summary>Standard alpha blending. </summary>
        public sealed class AlphaBlendState : BlendState {
            internal static readonly AlphaBlendState Instance = new AlphaBlendState();
            private AlphaBlendState() { }
        }
        
        /// <summary>Additive blending. </summary>
        public sealed class AdditiveState : BlendState {
            internal static readonly AdditiveState Instance = new AdditiveState();
            private AdditiveState() { }
        }
        
        /// <summary>Opaque (no blending). </summary>
        public sealed class OpaqueState : BlendState {
            internal static readonly OpaqueState Instance = new OpaqueState();
            private OpaqueState() { }
        }
    }
}