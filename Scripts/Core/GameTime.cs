using System;
using System.Runtime.InteropServices;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Represents the elapsed time in a Minesweeper game.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct GameTime
    {
        /// <summary>
        /// Gets the elapsed time.
        /// </summary>
        public TimeSpan Value
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameTime"/> struct.
        /// </summary>
        /// <param name="time">The elapsed time.</param>
        /// <exception cref="ArgumentException">Thrown when time is negative.</exception>
        public GameTime(TimeSpan time)
        {
            if (time < TimeSpan.Zero)
            {
                throw new ArgumentException("Game time cannot be negative", nameof(time));
            }

            this.Value = time;
        }

        /// <summary>
        /// Gets a <see cref="GameTime"/> instance representing zero elapsed time.
        /// </summary>
        public static GameTime Zero => new GameTime(TimeSpan.Zero);

        /// <summary>
        /// Adds the specified number of seconds to the current game time.
        /// </summary>
        /// <param name="seconds">The number of seconds to add.</param>
        /// <returns>A new <see cref="GameTime"/> instance with the added seconds.</returns>
        public GameTime Add(double seconds)
        {
            return new GameTime(this.Value.Add(TimeSpan.FromSeconds(seconds)));
        }

        /// <summary>
        /// Converts the game time to a string representation.
        /// </summary>
        /// <returns>A string representation of the game time in seconds (padded to 3 digits).</returns>
        public override string ToString()
        {
            return $"{(int)this.Value.TotalSeconds:D3}";
        }
    }
}
