using System;
using MonoGameLibrary.Core.Diagnostics;
using MonoGameLibrary.Core.Concurrency;
using MonoGameLibrary.Core.Pooling;

namespace MonoGameLibrary.Core.Hosting {
    /// <summary>
    /// Fluent API for configuring and building an <see cref="IGameHost"/>.
    /// </summary>
    public interface IGameBuilder {
        /// <summary>
        /// Registers a service instance, optionally overwriting an existing registration. 
        /// </summary>
        /// <typeparam name="T">The type of the service. </typeparam>
        /// <param name="instance">The service instance. </param>
        /// <param name="flagOverwrite">If <c>true</c>, replaces an existing registration. </param>
        /// <returns>The current <see cref="GameBuilder"/> for chaining. </returns>
        /// <exception cref="InvalidOperationException">Thrown if <paramref name="flagOverwrite"/> is <c>false</c> 
        /// and a service of type <typeparamref name="T"/> is already registered. </exception>
        IGameBuilder RegisterService<T>(T instance, bool flagOverwrite = false) where T : class;
        
        /// <summary>
        /// Adds a module to the host. Duplicate modules are rejected immediately. 
        /// </summary>
        /// <param name="module">The module instance to add. </param>
        /// <returns>The current <see cref="GameBuilder"/> for chaining. </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="module"/> is null. </exception>
        /// <exception cref="InvalidOperationException">Thrown if <paramref name="module"/> has already been added. </exception>
        IGameBuilder AddModule(object module);
        
        /// <summary>
        /// Provides a callback to configure the <see cref="GameHost"/> before modules are added. 
        /// </summary>
        /// <param name="actionConfigure">The action to run against the host. </param>
        /// <returns>The current <see cref="GameBuilder"/> for chaining. </returns>
        IGameBuilder ConfigureHost(Action<GameHost> actionConfigure);
        
        /// <summary>
        /// Registers a logger service. 
        /// </summary>
        /// <param name="logger">The logger implementation. </param>
        /// <param name="flagOverwrite">If <c>true</c>, replaces any previously registered <see cref="ILogger"/>. </param>
        /// <returns>The current <see cref="GameBuilder"/> for chaining. </returns>
        IGameBuilder UseLogger(ILogger logger, bool flagOverwrite = false);
        
        /// <summary>
        /// Registers a profiler service. 
        /// </summary>
        /// <param name="profiler">The profiler implementation. </param>
        /// <param name="flagOverwrite">If <c>true</c>, replaces any previously registered <see cref="IProfiler"/>. </param>
        /// <returns>The current <see cref="GameBuilder"/> for chaining. </returns>
        IGameBuilder UseProfiler(IProfiler profiler, bool flagOverwrite = false);
        
        /// <summary>
        /// Registers a thread pool service. 
        /// </summary>
        /// <param name="poolThread">The thread pool implementation. </param>
        /// <param name="flagOverwrite">If <c>true</c>, replaces any previously registered <see cref="IThreadPool"/>. </param>
        /// <returns>The current <see cref="GameBuilder"/> for chaining. </returns>
        IGameBuilder UseThreadPool(IThreadPool poolThread, bool flagOverwrite = false);
        
        /// <summary>
        /// Registers a cancellation service. 
        /// </summary>
        /// <param name="serviceCancellation">The cancellation service implementation. </param>
        /// <param name="flagOverwrite">If <c>true</c>, replaces any previously registered <see cref="ICancellationService"/>. </param>
        /// <returns>The current <see cref="GameBuilder"/> for chaining. </returns>
        IGameBuilder UseCancellationService(ICancellationService serviceCancellation, bool flagOverwrite = false);
        
        /// <summary>
        /// Registers a loading progress service. 
        /// </summary>
        /// <param name="progressLoading">The loading progress implementation. </param>
        /// <param name="flagOverwrite">If <c>true</c>, replaces any previously registered <see cref="ILoadingProgress"/>. </param>
        /// <returns>The current <see cref="GameBuilder"/> for chaining. </returns>
        IGameBuilder UseLoadingProgress(ILoadingProgress progressLoading, bool flagOverwrite = false);
        
        /// <summary>
        /// Registers an object pool factory. 
        /// </summary>
        /// <param name="factory">The pool factory implementation. </param>
        /// <param name="flagOverwrite">If <c>true</c>, replaces any previously registered <see cref="IObjectPoolFactory"/>. </param>
        /// <returns>The current <see cref="GameBuilder"/> for chaining. </returns>
        IGameBuilder UseObjectPoolFactory(IObjectPoolFactory factoryPool, bool flagOverwrite = false);
        
        /// <summary>
        /// Retrieves a registered service instance of the specified type. 
        /// </summary>
        /// <typeparam name="T">The service type to retrieve. </typeparam>
        /// <returns>The registered service instance. </returns>
        /// <exception cref="InvalidOperationException">Thrown if the service is not registered. </exception>
        T GetService<T>() where T : class;
        
        /// <summary>
        /// Tries to retrieve a registered service instance of the specified type. 
        /// </summary>
        /// <typeparam name="T">The service type to retrieve. </typeparam>
        /// <param name="instance">The registered service instance if present; otherwise null.</param>
        /// <returns><c>true</c> if the service is registered; otherwise <c>false</c>.</returns>
        bool TryGetService<T>(out T instance) where T : class;
        
        /// <summary>
        /// Clears all registered services from the builder. 
        /// This does not affect modules already added. 
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if <see cref="Build"/> has already been called or is in progress. 
        /// </exception>
        void ClearServices();
        
        /// <summary>
        /// Builds the <see cref="IGameHost"/> with all configured services and modules. 
        /// This method can only be called once per builder instance. 
        /// </summary>
        /// <returns>An initialized <see cref="IGameHost"/>. </returns>
        /// <exception cref="InvalidOperationException">Thrown if <see cref="Build"/> has already been called. </exception>
        IGameHost Build();
    }
}