using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Core.Content;
using MonoGameLibrary.Core.Hosting;
using MonoGameLibrary.Core.Primitives;
using MonoGameLibrary.Extensions.Audio;
using MonoGameLibrary.Extensions.Graphics;

namespace MonoGameLibrary.Adapters.MonoGame.Content {
    /// <summary>
    /// A MonoGame implementation of <see cref="IContentService"/>. 
    /// </summary>
    public sealed class ContentService : IContentService {
        private readonly ContentManager _managerContent;
        private readonly Dictionary<Type, object> _loaders;
        private bool _flagDisposed;
        
        /// <summary>
        /// Creates a content service backed by a MonoGame content manager. 
        /// </summary>
        /// <param name="managerContent">The MonoGame ContentManager to use for loading raw assets.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="managerContent"/> is null.</exception>
        public ContentService(ContentManager managerContent) {
            if (managerContent == null) { throw new ArgumentNullException(nameof(managerContent)); }
            _managerContent = managerContent;
            _loaders = new Dictionary<Type, object>();
        }
        
        /// <summary>
        /// Gets the underlying MonoGame ContentManager used by this service.
        /// </summary>
        public ContentManager ContentManager {
            get { return _managerContent; }
        }
        
        /// <inheritdoc />
        public void Register<T>(Func<string, T> loader) where T : class, IAsset {
            if (loader == null) { throw new ArgumentNullException(nameof(loader)); }
            _loaders.Add(typeof(T), loader);
        }
        
        /// <inheritdoc />
        public T Load<T>(string nameAsset) where T : class, IAsset {
            if (nameAsset == null) { throw new ArgumentNullException(nameof(nameAsset)); }
            if (_loaders.TryGetValue(typeof(T), out var stored)) {
                if (stored is Func<string, T> typedLoader) {
                    return typedLoader(nameAsset);
                }
            }
            throw new NotSupportedException(
                $"Asset type {typeof(T)} is not registered. " +
                "Call Register<T> before attempting to load assets of this type."
            );
        }
        
        /// <inheritdoc />
        public void Unload() {
            _managerContent.Unload();
        }
        
        /// <inheritdoc />
        public void Dispose() {
            if (_flagDisposed) {
                return;
            }
            
            _flagDisposed = true;
            try {
                Unload();
            } finally {
                _managerContent.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
