using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Core.Content;
using MonoGameLibrary.Extensions.Audio;

namespace MonoGameLibrary.Adapters.MonoGame.Audio {
    public static class AudioLoaders {
        /// <summary>
        /// Creates a loader for the specified audio asset type.
        /// Supported types: IClipAudio, ITrackAudio.
        /// </summary>
        public static Func<string, IAsset> CreateLoader<T>(ContentManager managerContent) where T : class, IAsset {
            if (typeof(T) == typeof(IClipAudio)) {
                return delegate(string name) { return new ClipAudio(managerContent.Load<SoundEffect>(name)); };
            }
            if (typeof(T) == typeof(ITrackAudio)) {
                return delegate(string name) { return new TrackAudio(managerContent.Load<Song>(name)); };
            }
            throw new NotSupportedException($"Audio loader does not support asset type {typeof(T).FullName}.");
        }
    }
}