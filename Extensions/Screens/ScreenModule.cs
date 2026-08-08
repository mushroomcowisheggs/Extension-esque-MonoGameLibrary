using System;
using MonoGameLibrary.Core.Lifecycle;
using MonoGameLibrary.Core.Time;

namespace MonoGameLibrary.Extensions.Screens {
    /// <summary>
    /// Module that integrates the screen service with the GameHost lifecycle. 
    /// </summary>
    public sealed class ScreenModule : IUpdateable, IDrawable {
        private readonly IScreenService _service;
        private readonly int _order;
        private bool _flagEnabled = true;
        private bool _flagVisible = true;
        
        public ScreenModule(IScreenService service, int order = 0) {
            if (service == null) { throw new ArgumentNullException(nameof(service)); }
            _service = service;
            _order = order;
        }
        
        public int Order { get { return _order; } }
        
        public bool Enabled {
            get { return _flagEnabled; }
            set { _flagEnabled = value; }
        }
        
        public bool Visible {
            get { return _flagVisible; }
            set { _flagVisible = value; }
        }
        
        public void Update(FrameTime timeFrame) {
            if (!_flagEnabled) { return; }
            _service.Update(timeFrame);
        }
        
        public void Draw(FrameTime timeFrame) {
            if (!_flagVisible) { return; }
            _service.Draw(timeFrame);
        }
    }
}