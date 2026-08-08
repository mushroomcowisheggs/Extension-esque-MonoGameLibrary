using System;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Extensions.Audio;

namespace MonoGameLibrary.Adapters.MonoGame.Audio {
    /// <summary>
    /// Wraps a MonoGame <see cref="Song"/> as an <see cref="ITrackAudio"/>.
    /// </summary>
    internal sealed class TrackAudio : ITrackAudio {
        /// <summary>The underlying MonoGame song. </summary>
        internal Song Song { get; }
        
        /// <inheritdoc/>
        public TimeSpan Duration {
            get {
                if (Song == null) {
                    return TimeSpan.Zero;
                }
                return Song.Duration;
            }
        }
        
        /// <summary>Creates a new wrapper around a MonoGame song. </summary>
        public TrackAudio(Song song) {
            if (song == null) {
                throw new ArgumentNullException(nameof(song));
            }
            Song = song;
        }
    }
}