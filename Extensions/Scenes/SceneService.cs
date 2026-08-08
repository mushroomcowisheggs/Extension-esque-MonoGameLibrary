using System;
using MonoGameLibrary.Core;
using MonoGameLibrary.Core.Diagnostics;
using MonoGameLibrary.Core.Time;
using MonoGameLibrary.Core.Lifecycle;
using MonoGameLibrary.Core.Hosting;

namespace MonoGameLibrary.Extensions.Scenes {
    /// <summary>
    /// Implements the scene management logic: switching, updating, and drawing the active scene. 
    /// This class is platform-agnostic and does not implement lifecycle interfaces. 
    /// </summary>
    public sealed class SceneService : ISceneService, IDisposable {
        private readonly object _lock = new object();
        private Scene _sceneCurrent;
        private Scene _scenePending;
        private bool _flagDisposed;
        
        /// <summary>
        /// Raised when a scene switch occurs, allowing the game layer to perform
        /// additional setup (e.g., injecting content loading delegate).
        /// </summary>
        public event Action<Scene> SceneSwitched;
        
        /// <summary>
        /// Raised each frame to request drawing of the active scene.
        /// The game layer subscribes and provides the actual rendering.
        /// </summary>
        public event Action<FrameTime> DrawRequested;
        
        /// <inheritdoc />
        public Scene CurrentScene {
            get { lock (_lock) { return _sceneCurrent; } }
        }
        
        /// <inheritdoc />
        public void ChangeScene(Scene scene) {
            if (scene == null) {
                throw new ArgumentNullException(nameof(scene));
            }
            lock (_lock) {
                _scenePending = scene;
            }
        }
        
        /// <inheritdoc />
        public void Update(FrameTime timeFrame) {
            if (_flagDisposed) {
                return;
            }
            
            Scene sceneToActivate = null;
            lock (_lock) {
                if (_scenePending != null) {
                    sceneToActivate = _scenePending;
                    _scenePending = null;
                }
            }
            
            if (sceneToActivate != null) {
                if (_sceneCurrent != null) {
                    _sceneCurrent.Dispose();
                }
                _sceneCurrent = sceneToActivate;
                
                // Trigger content loading (delegate set by game layer)
                _sceneCurrent.LoadContent();
                _sceneCurrent.Initialize();
                
                // Notify game layer about the switch
                if (SceneSwitched != null) {
                    SceneSwitched.Invoke(_sceneCurrent);
                }
            }
            
            if (_sceneCurrent != null && _sceneCurrent.Enabled) {
                _sceneCurrent.Update(timeFrame);
            }
        }
        
        /// <inheritdoc />
        public void Draw(FrameTime timeFrame) {
            if (_flagDisposed) {
                return;
            }
            
            if (DrawRequested != null) {
                DrawRequested.Invoke(timeFrame);
            }
        }
        
        /// <summary>
        /// Disposes the service and any active or pending scenes. 
        /// </summary>
        public void Dispose() {
            if (_flagDisposed) {
                return;
            }
            
            if (_sceneCurrent != null) {
                _sceneCurrent.Dispose();
            }
            
            if (_scenePending != null) {
                _scenePending.Dispose();
            }
            _flagDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
