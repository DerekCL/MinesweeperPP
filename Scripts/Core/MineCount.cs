using System;
using System.Runtime.InteropServices;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Represents the number of mines in a Minesweeper game.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct MineCount
    {
        /// <summary>
        /// Gets the number of mines.
        /// </summary>
        public int Value
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MineCount"/> struct.
        /// </summary>
        /// <param name="count">The number of mines.</param>
        /// <param name="dimensions">The dimensions of the game grid.</param>
        /// <exception cref="ArgumentException">Thrown when count is negative or greater than or equal to the total number of tiles.</exception>
        public MineCount(int count, GameDimensions dimensions)
        {
            if (count < 0)
            {
                throw new ArgumentException("Mine count cannot be negative", nameof(count));
            }

            if (count >= dimensions.TotalTiles)
            {
                throw new ArgumentException("Mine count cannot be greater than or equal to the total number of tiles", nameof(count));
            }

            this.Value = count;
        }
    }
}
