using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Represents a position in the Minesweeper grid.
    /// Provides methods for grid position validation and traversal.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct GridPosition
    {
        /// <summary>
        /// Gets an empty grid position that represents an invalid position.
        /// </summary>
        public static GridPosition Empty => new GridPosition(-1, -1);

        /// <summary>
        /// Gets the row index in the grid.
        /// </summary>
        public int Row
        {
            get;
        }

        /// <summary>
        /// Gets the column index in the grid.
        /// </summary>
        public int Col
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridPosition"/> struct.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        public GridPosition(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        /// <summary>
        /// Gets all valid positions in a grid with the specified dimensions.
        /// </summary>
        /// <param name="dimensions">The dimensions of the grid.</param>
        /// <returns>An enumerable of all valid grid positions.</returns>
        public static IEnumerable<GridPosition> GetAllPositions(GameDimensions dimensions)
        {
            for (int row = 0; row < dimensions.Rows; row++)
            {
                for (int col = 0; col < dimensions.Columns; col++)
                {
                    yield return new GridPosition(row, col);
                }
            }
        }

        /// <summary>
        /// Gets all valid positions adjacent to this position in a grid with the specified dimensions.
        /// </summary>
        /// <param name="dimensions">The dimensions of the grid.</param>
        /// <returns>An enumerable of all valid adjacent grid positions.</returns>
        public IEnumerable<GridPosition> GetAdjacentPositions(GameDimensions dimensions)
        {
            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    var pos = new GridPosition(this.Row + r, this.Col + c);
                    if (pos.IsValid(dimensions))
                    {
                        yield return pos;
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether this position is valid within the specified grid dimensions.
        /// </summary>
        /// <param name="dimensions">The dimensions of the grid.</param>
        /// <returns>true if the position is valid; otherwise, false.</returns>
        public bool IsValid(GameDimensions dimensions)
        {
            return this.Row >= 0 && this.Row < dimensions.Rows && this.Col >= 0 && this.Col < dimensions.Columns;
        }

        /// <summary>
        /// Deconstructs this position into its row and column components.
        /// </summary>
        /// <param name="row">When this method returns, contains the row index.</param>
        /// <param name="col">When this method returns, contains the column index.</param>
        public void Deconstruct(out int row, out int col)
        {
            row = this.Row;
            col = this.Col;
        }
    }
}
