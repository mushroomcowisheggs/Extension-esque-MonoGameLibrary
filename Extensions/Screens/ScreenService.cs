using System;
using System.Collections.Generic;
using MonoGameLibrary.Core.Lifecycle;
using MonoGameLibrary.Core.Time;

namespace MonoGameLibrary.Extensions.Screens {
    /// <summary>
    /// Default implementation of <see cref="IScreenService"/>.
    /// </summary>
    public sealed class ScreenService : IScreenService {
        private readonly List<Screen> _screens = new List<Screen>();
        private bool _flagIsProcessing;
        private readonly Queue<Action> _queueOperation = new Queue<Action>();
        
        /// <summary>
        /// Raised each frame to request drawing of the active scene.
        /// The game layer subscribes and provides the actual rendering.
        /// </summary>
        public event Action<FrameTime> DrawRequested;
        
        /// <inheritdoc />
        public Screen CurrentScreen { get { return _screens.Count > 0 ? _screens[_screens.Count - 1] : null; } }
        
        private void SubscribeScreen(Screen screen) {
            screen.ScreenChangeRequested += OnScreenChangeRequested;
        }
        
        private void UnsubscribeScreen(Screen screen) {
            screen.ScreenChangeRequested -= OnScreenChangeRequested;
        }
        
        private void OnScreenChangeRequested(object sender, ScreenChangeEventArguments arguments) {
            switch (arguments.ChangeType) {
                case ScreenChangeType.Push:
                Push(arguments.NewScreen);
                break;
                case ScreenChangeType.Pop:
                Pop();
                break;
                case ScreenChangeType.Change:
                Change(arguments.NewScreen);
                break;
            }
        }
        
        /// <inheritdoc />
        public void Push(Screen screen) {
            Action operation = delegate () {
                if (_screens.Count > 0)
                    _screens[_screens.Count - 1].Exit();
                SubscribeScreen(screen);
                _screens.Add(screen);
                screen.Enter();
            };
            QueueOrExecute(operation);
        }
        
        /// <inheritdoc />
        public void Pop() {
            Action operation = delegate () {
                if (_screens.Count > 0) {
                    var top = _screens[_screens.Count - 1];
                    UnsubscribeScreen(top);
                    top.Exit();
                    _screens.RemoveAt(_screens.Count - 1);
                }
                if (_screens.Count > 0) {
                    _screens[_screens.Count - 1].Enter();
                }
            };
            QueueOrExecute(operation);
        }
        
        /// <inheritdoc />
        public void Change(Screen screen) {
            Action operation = delegate () {
                while (_screens.Count > 0) {
                    var top = _screens[_screens.Count - 1];
                    UnsubscribeScreen(top);
                    top.Exit();
                    _screens.RemoveAt(_screens.Count - 1);
                }
                SubscribeScreen(screen);
                _screens.Add(screen);
                screen.Enter();
            };
            QueueOrExecute(operation);
        }
        
        private void QueueOrExecute(Action operation) {
            if (_flagIsProcessing) {
                _queueOperation.Enqueue(operation);
            }
            else {
                operation();
            }
        }
        
        /// <inheritdoc />
        public void Update(FrameTime timeFrame) {
            _flagIsProcessing = true;
            while (_queueOperation.Count > 0) {
                var op = _queueOperation.Dequeue();
                if (op != null) {
                    op.Invoke();
                }
            }
            
            for (int i = _screens.Count - 1; i >= 0; i -= 1) {
                var screen = _screens[i];
                if (screen.InputAction != null) {
                    screen.InputAction.Invoke(timeFrame);
                }
                screen.Update(timeFrame);
                if (screen.IsBlocking) {
                    break;
                }
            }
            _flagIsProcessing = false;
        }
        
        /// <inheritdoc />
        public void Draw(FrameTime timeFrame) {
            if (DrawRequested != null) {
                DrawRequested.Invoke(timeFrame);
            }
        }
    }
}