namespace MonoGameLibrary.Extensions.Graphics {
    /// <summary>
    /// A sampler state. Use static properties to obtain preset instances. 
    /// </summary>
    public abstract class SamplerState {
        /// <summary>Prevents external instantiation. </summary>
        protected SamplerState() { }
        
        /// <summary>Point filtering with clamp addressing. </summary>
        public static SamplerState PointClamp { get { return PointClampState.Instance; } }
        
        /// <summary>Point filtering with wrap addressing. </summary>
        public static SamplerState PointWrap { get { return PointWrapState.Instance; } }
        
        /// <summary>Linear filtering with clamp addressing. </summary>
        public static SamplerState LinearClamp { get { return LinearClampState.Instance; } }
        
        /// <summary>Linear filtering with wrap addressing. </summary>
        public static SamplerState LinearWrap { get { return LinearWrapState.Instance; } }
        
        /// <summary>Point filtering with clamp addressing. </summary>
        public sealed class PointClampState : SamplerState {
            internal static readonly PointClampState Instance = new PointClampState();
            private PointClampState() { }
        }
        
        /// <summary>Point filtering with wrap addressing. </summary>
        public sealed class PointWrapState : SamplerState {
            internal static readonly PointWrapState Instance = new PointWrapState();
            private PointWrapState() { }
        }
        
        /// <summary>Linear filtering with clamp addressing. </summary>
        public sealed class LinearClampState : SamplerState {
            internal static readonly LinearClampState Instance = new LinearClampState();
            private LinearClampState() { }
        }
        
        /// <summary>Linear filtering with wrap addressing. </summary>
        public sealed class LinearWrapState : SamplerState {
            internal static readonly LinearWrapState Instance = new LinearWrapState();
            private LinearWrapState() { }
        }
    }
}