using System;
using System.Runtime.InteropServices;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Represents a configuration for a Minesweeper game, including dimensions and mine count.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct GameConfiguration
    {
        /// <summary>
        /// Gets the dimensions of the game grid.
        /// </summary>
        public GameDimensions Dimensions
        {
            get;
        }

        /// <summary>
        /// Gets the number of mines in the game.
        /// </summary>
        public MineCount MineCount
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameConfiguration"/> struct.
        /// </summary>
        /// <param name="dimensions">The dimensions of the game grid.</param>
        /// <param name="mineCount">The number of mines in the game.</param>
        public GameConfiguration(GameDimensions dimensions, MineCount mineCount)
        {
            this.Dimensions = dimensions;
            this.MineCount = mineCount;
        }

        /// <summary>
        /// Creates a beginner-level game configuration (9x9 grid with 10 mines).
        /// </summary>
        /// <returns>A beginner-level game configuration.</returns>
        public static GameConfiguration CreateBeginner()
        {
            var dimensions = new GameDimensions(9, 9);
            var mineCount = new MineCount(10, dimensions);
            return new GameConfiguration(dimensions, mineCount);
        }

        /// <summary>
        /// Creates an intermediate-level game configuration (16x16 grid with 40 mines).
        /// </summary>
        /// <returns>An intermediate-level game configuration.</returns>
        public static GameConfiguration CreateIntermediate()
        {
            var dimensions = new GameDimensions(16, 16);
            var mineCount = new MineCount(40, dimensions);
            return new GameConfiguration(dimensions, mineCount);
        }

        /// <summary>
        /// Creates an expert-level game configuration (16x30 grid with 99 mines).
        /// </summary>
        /// <returns>An expert-level game configuration.</returns>
        public static GameConfiguration CreateExpert()
        {
            var dimensions = new GameDimensions(16, 30);
            var mineCount = new MineCount(99, dimensions);
            return new GameConfiguration(dimensions, mineCount);
        }
    }
}
