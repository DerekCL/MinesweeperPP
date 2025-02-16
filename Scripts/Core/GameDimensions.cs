using System;
using System.Runtime.InteropServices;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Represents the dimensions of a Minesweeper game grid.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct GameDimensions
    {
        /// <summary>
        /// Gets the number of rows in the game grid.
        /// </summary>
        public int Rows
        {
            get;
        }

        /// <summary>
        /// Gets the number of columns in the game grid.
        /// </summary>
        public int Columns
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDimensions"/> struct.
        /// </summary>
        /// <param name="rows">The number of rows in the game grid.</param>
        /// <param name="columns">The number of columns in the game grid.</param>
        /// <exception cref="ArgumentException">Thrown when rows or columns are less than or equal to 0.</exception>
        public GameDimensions(int rows, int columns)
        {
            if (rows <= 0)
            {
                throw new ArgumentException("Rows must be greater than 0", nameof(rows));
            }

            if (columns <= 0)
            {
                throw new ArgumentException("Columns must be greater than 0", nameof(columns));
            }

            this.Rows = rows;
            this.Columns = columns;
        }

        /// <summary>
        /// Gets the total number of tiles in the game grid.
        /// </summary>
        public int TotalTiles => this.Rows * this.Columns;
    }
}
