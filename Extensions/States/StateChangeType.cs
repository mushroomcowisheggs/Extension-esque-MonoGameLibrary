using System;

namespace MonoGameLibrary.Extensions.States {
    /// <summary>
    /// Specifies the type of state change operation being requested.
    /// </summary>
    public enum StateChangeType {
        /// <summary>Pushes a new state onto the stack.</summary>
        Push,
        /// <summary>Pops the current state from the stack.</summary>
        Pop,
        /// <summary>Replaces the entire stack with a new state.</summary>
        Change
    }
}