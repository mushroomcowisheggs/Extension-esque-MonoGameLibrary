using System;
using MonoGameLibrary.Core;
using MonoGameLibrary.Core.Diagnostics;
using MonoGameLibrary.Core.Hosting;
using MonoGameLibrary.Core.Lifecycle;
using MonoGameLibrary.Core.Time;
using MonoGameLibrary.Extensions.Input;

namespace MonoGameLibrary.Extensions.Screens {
    /// <summary>
    /// Represents a game screen that can be managed by a screen service.
    /// </summary>
    public abstract class Screen : IDisposable {
        private bool _flagDisposed;
        
        /// <summary>
        /// Gets the content service used by this screen.
        /// </summary>
        protected IContentService ContentService { get; }
        
        /// <summary>
        /// Gets the logger (optional).
        /// </summary>
        protected ILogger Logger { get; }
        
        /// <summary>
        /// Gets the profiler (optional).
        /// </summary>
        protected Optional<IProfiler> Profiler { get; }
        
        /// <summary>
        /// Gets the input service (optional).
        /// </summary>
        protected Optional<IInputService> InputService { get; }
        
        /// <summary>
        /// Occurs when a screen change (push, pop, or change) is requested.
        /// </summary>
        public event EventHandler<ScreenChangeEventArguments> ScreenChangeRequested;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Screen"/> class.
        /// </summary>
        protected Screen(
            IContentService contentService,
            Optional<ILogger> logger = default,
            Optional<IProfiler> profiler = default,
            Optional<IInputService> serviceInput = default
        ) {
            if (contentService == null) {
                throw new ArgumentNullException(nameof(contentService));
            }
            
            ContentService = contentService;
            Logger = logger.HasValue ? logger.Value : NullLogger.Instance;
            Profiler = profiler;
            InputService = serviceInput;
        }
        
        /// <summary>
        /// Called once to load content. Override to load textures, sounds, etc.
        /// </summary>
        public virtual void LoadContent() { }
        
        protected void RequestPush(Screen screenNew) {
            if (ScreenChangeRequested != null) {
                ScreenChangeRequested(this, new ScreenChangeEventArguments(ScreenChangeType.Push, screenNew));
            }
        }
        
        protected void RequestPop() {
            if (ScreenChangeRequested != null) {
                ScreenChangeRequested(this, new ScreenChangeEventArguments(ScreenChangeType.Pop, null));
            }
        }
        
        protected void RequestChange(Screen screenNew) {
            if (ScreenChangeRequested != null) {
                ScreenChangeRequested(this, new ScreenChangeEventArguments(ScreenChangeType.Change, screenNew));
            }
        }
        
        public virtual void HandleInput(FrameTime timeFrame, IInputService input) { }
        
        public virtual bool IsTransparent { get { return false; } }
        public virtual bool IsBlocking { get { return false; } }
        
        public virtual void Enter() { }
        public virtual void Exit() { }
        
        public abstract void Update(FrameTime timeFrame);
        public abstract void Draw(FrameTime timeFrame, IRenderContext contextRender);
        
        protected virtual void Dispose(bool flagDisposing) {
            if (_flagDisposed) { return; }
            if (flagDisposing) {
                if (ContentService is IDisposable disposable) {
                    disposable.Dispose();
                }
            }
            _flagDisposed = true;
        }
        
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}