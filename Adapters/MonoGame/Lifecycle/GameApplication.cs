using System;
using MonoGameLibrary.Core.Hosting;

namespace MonoGameLibrary.Adapters.MonoGame.Lifecycle {
    /// <summary>
    /// Provides a convenient way to bootstrap a game application with a custom game loop.
    /// </summary>
    public static class GameApplication {
        /// <summary>
        /// Creates a configured IGameHost via the builder, wraps it in a MonoGame integration,
        /// and starts the game loop. This call blocks until the game exits.
        /// </summary>
        /// <param name="actionConfigureServices">Callback to register services and modules.</param>
        /// <returns>The initialized and running IGameHost.</returns>
        /// <exception cref="ArgumentNullException">Thrown if actionConfigureServices is null.</exception>
        public static IGameHost Start(Action<IGameBuilder> actionConfigureServices) {
            if (actionConfigureServices == null) {
                throw new ArgumentNullException(nameof(actionConfigureServices));
            }
            
            var builder = new GameBuilder();
            actionConfigureServices(builder);
            IGameHost host = builder.Build();
            
            var gameMonoGame = new MonoGameIntegrationGame(host);
            gameMonoGame.Run();
            
            return host;
        }
    }
}