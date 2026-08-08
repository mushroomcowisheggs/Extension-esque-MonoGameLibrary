using System;
using MonoGameLibrary.Core.Lifecycle;
using MonoGameLibrary.Core.Time;

namespace MonoGameLibrary.Extensions.States {
    /// <summary>
    /// Wraps an <see cref="IStateService"/> as a module that can be registered 
    /// with <see cref="GameHost"/>, implementing <see cref="IUpdateable"/>. 
    /// </summary>
    public sealed class StateModule : IUpdateable {
        private readonly IStateService _service;
        private readonly int _order;
        private bool _flagEnabled = true;
        
        public StateModule(IStateService service, int order = 0) {
            if (service == null) {
                throw new ArgumentNullException(nameof(service));
            }
            _service = service;
            _order = order;
        }
        
        public int Order { get { return _order; } }
        public bool Enabled {
            get { return _flagEnabled; }
            set { _flagEnabled = value; }
        }
        
        public void Update(FrameTime timeFrame) {
            if (!_flagEnabled) { return; }
            _service.Update(timeFrame);
        }
    }
}