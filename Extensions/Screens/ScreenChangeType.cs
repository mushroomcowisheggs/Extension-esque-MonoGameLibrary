using System;

namespace MonoGameLibrary.Extensions.Screens {
    /// <summary>
    /// Specifies the type of screen change operation being requested.
    /// </summary>
    public enum ScreenChangeType {
        /// <summary>Pushes a new screen onto the stack.</summary>
        Push,
        /// <summary>Pops the current screen from the stack.</summary>
        Pop,
        /// <summary>Replaces the entire stack with a new screen.</summary>
        Change
    }
}