using System;
using System.Runtime.InteropServices;
using Godot;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Represents all game settings with immutable properties.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Settings
    {
        /// <summary>
        /// Gets the default settings.
        /// </summary>
        public static Settings Default => new Settings(
            GameConfiguration.CreateBeginner(),
            GameTheme.Classic,
            1.0f,
            0.5f);

        /// <summary>
        /// Gets the current game configuration.
        /// </summary>
        public GameConfiguration GameConfig
        {
            get;
        }

        /// <summary>
        /// Gets the current theme.
        /// </summary>
        public GameTheme Theme
        {
            get;
        }

        /// <summary>
        /// Gets the sound effects volume (0.0 to 1.0).
        /// </summary>
        public float SoundVolume
        {
            get;
        }

        /// <summary>
        /// Gets the music volume (0.0 to 1.0).
        /// </summary>
        public float MusicVolume
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> struct.
        /// </summary>
        /// <param name="gameConfig">The game configuration to use.</param>
        /// <param name="theme">The visual theme to use.</param>
        /// <param name="soundVolume">The sound effects volume (0.0 to 1.0).</param>
        /// <param name="musicVolume">The music volume (0.0 to 1.0).</param>
        public Settings(
            GameConfiguration gameConfig,
            GameTheme theme,
            float soundVolume,
            float musicVolume)
        {
            this.GameConfig = gameConfig;
            this.Theme = theme;
            this.SoundVolume = Math.Clamp(soundVolume, 0.0f, 1.0f);
            this.MusicVolume = Math.Clamp(musicVolume, 0.0f, 1.0f);
        }
    }
}
