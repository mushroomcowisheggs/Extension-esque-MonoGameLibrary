using System;

namespace MonoGameLibrary.Extensions.Screens {
    /// <summary>
    /// Provides data for screen change events.
    /// </summary>
    public class ScreenChangeEventArguments : EventArgs {
        /// <summary>
        /// Gets the type of screen change being requested.
        /// </summary>
        public ScreenChangeType ChangeType { get; }
        
        /// <summary>
        /// Gets the new screen to be transitioned to, if applicable.
        /// Returns null for Pop operations.
        /// </summary>
        public Screen NewScreen { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenChangeEventArguments"/> class.
        /// </summary>
        /// <param name="typeChange">The type of screen change.</param>
        /// <param name="screenNew">The new screen for Push or Change operations; null for Pop.</param>
        public ScreenChangeEventArguments(ScreenChangeType typeChange, Screen screenNew) {
            ChangeType = typeChange;
            NewScreen = screenNew;
        }
    }
}