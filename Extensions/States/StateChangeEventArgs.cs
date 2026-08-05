using System;

namespace MonoGameLibrary.Extensions.States {
    /// <summary>
    /// Provides data for state change events.
    /// </summary>
    public class StateChangeEventArgs : EventArgs {
        /// <summary>
        /// Gets the type of state change being requested.
        /// </summary>
        public StateChangeType ChangeType { get; }
        
        /// <summary>
        /// Gets the new state to be transitioned to, if applicable.
        /// Returns null for Pop operations.
        /// </summary>
        public State NewState { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="StateChangeEventArgs"/> class.
        /// </summary>
        /// <param name="typeChange">The type of state change.</param>
        /// <param name="stateNew">The new state for Push or Change operations; null for Pop.</param>
        public StateChangeEventArgs(StateChangeType typeChange, State stateNew) {
            ChangeType = typeChange;
            NewState = stateNew;
        }
    }
}