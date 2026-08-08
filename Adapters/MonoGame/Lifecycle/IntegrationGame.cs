using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Core.Content;
using MonoGameLibrary.Core.Hosting;
using MonoGameLibrary.Core.Time;
using MonoGameLibrary.Extensions.Graphics;

namespace MonoGameLibrary.Adapters.MonoGame.Lifecycle {
    /// <summary>
    /// Internal Game subclass that bridges MonoGame's Game loop to
    /// MonoGameLibrary's IGameHost lifecycle.
    /// </summary>
    internal sealed class IntegrationGame : Game {
        private readonly IGameHost _host;
        private GraphicsDeviceManager _graphics;
        private bool _flagIsInitialized;
        
        /// <summary>
        /// Initializes a new instance and stores the host reference.
        /// </summary>
        /// <param name="host">The game host to drive.</param>
        /// <exception cref="ArgumentNullException">Thrown if host is null.</exception>
        public IntegrationGame(IGameHost host) {
            if (host == null) {
                throw new ArgumentNullException(nameof(host));
            }
            
            _host = host;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize() {
            base.Initialize();
        }
        
        protected override void LoadContent() {
            base.LoadContent();
            
            // Retrieve IContentService from host's service registry (must be registered by user)
            if (!_host.Services.TryGet(out IContentService serviceContent)) {
                throw new InvalidOperationException(
                    "IContentService must be registered via GameBuilder.RegisterService before building the host."
                );
            }
            
            _host.Initialize(serviceContent);
            _flagIsInitialized = true;
        }
        
        protected override void Update(GameTime timeGame) {
            base.Update(timeGame);
            
            if (!_flagIsInitialized) {
                return;
            }
            
            FrameTime timeFrame = new FrameTime(timeGame.TotalGameTime, timeGame.ElapsedGameTime);
            _host.Update(timeFrame);
        }
        
        protected override void Draw(GameTime timeGame) {
            base.Draw(timeGame);
            
            if (!_flagIsInitialized) {
                return;
            }
            
            FrameTime timeFrame = new FrameTime(timeGame.TotalGameTime, timeGame.ElapsedGameTime);
            _host.Draw(timeFrame);
        }
        
        protected override void Dispose(bool flagDisposing) {
            if (flagDisposing) {
                _host.Dispose();
            }
            
            base.Dispose(flagDisposing);
        }
    }
}