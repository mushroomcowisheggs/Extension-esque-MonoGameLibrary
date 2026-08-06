using System;
using System.Collections.Generic;
using MonoGameLibrary.Core.Time;

namespace MonoGameLibrary.Extensions.States {
    /// <summary>
    /// Default implementation of <see cref="IStateService"/> using a stack.
    /// </summary>
    public sealed class StateService : IStateService {
        private readonly List<IState> _states = new List<IState>();
        
        /// <inheritdoc />
        public IState CurrentState {
            get {
                if (_states.Count > 0) {
                    return _states[_states.Count - 1];
                }
                return null;
            }
        }
        
        /// <inheritdoc />
        public void Push(IState state) {
            if (state == null) {
                throw new ArgumentNullException(nameof(state));
            }
            
            // Suspend current state
            if (_states.Count > 0) {
                _states[_states.Count - 1].Exit();
            }
            
            _states.Add(state);
            state.Enter();
        }
        
        /// <inheritdoc />
        public void Pop() {
            if (_states.Count == 0) {
                return;
            }
            
            IState top = _states[_states.Count - 1];
            top.Exit();
            _states.RemoveAt(_states.Count - 1);
            
            // Resume previous state
            if (_states.Count > 0) {
                _states[_states.Count - 1].Enter();
            }
        }
        
        /// <inheritdoc />
        public void Change(IState state) {
            if (state == null) {
                throw new ArgumentNullException(nameof(state));
            }
            
            // Exit all states
            for (int i = _states.Count - 1; i >= 0; i -= 1) {
                _states[i].Exit();
            }
            
            _states.Clear();
            _states.Add(state);
            state.Enter();
        }
        
        /// <inheritdoc />
        public void Update(FrameTime timeFrame) {
            if (_states.Count > 0) {
                // Update only the topmost state (no transparency concept here)
                _states[_states.Count - 1].Update(timeFrame);
            }
        }
    }
}