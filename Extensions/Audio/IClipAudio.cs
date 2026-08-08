using System;
using MonoGameLibrary.Core.Content;

namespace MonoGameLibrary.Extensions.Audio {
    /// <summary>
    /// Represents a short audio clip that can be played. 
    /// </summary>
    public interface IClipAudio : IAsset {
        /// <summary>Duration of the audio clip. </summary>
        TimeSpan Duration { get; }
    }
}