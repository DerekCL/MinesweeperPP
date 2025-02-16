using System.Runtime.InteropServices;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Represents the state of a tile in the Minesweeper grid.
    /// Provides a read-only interface to tile properties and operations.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct TileState
    {
        private readonly Tile tile;

        /// <summary>
        /// Initializes a new instance of the <see cref="TileState"/> struct.
        /// </summary>
        /// <param name="tile">The tile to wrap.</param>
        public TileState(Tile tile)
        {
            this.tile = tile;
        }

        /// <summary>
        /// Gets a value indicating whether the tile can be revealed.
        /// A tile can be revealed if it is not already revealed and not flagged.
        /// </summary>
        public bool CanReveal => !this.IsRevealed && !this.IsFlagged;

        /// <summary>
        /// Gets a value indicating whether revealing this tile should trigger revealing adjacent tiles.
        /// Adjacent tiles should be revealed when this tile has no adjacent mines and is not a mine.
        /// </summary>
        public bool ShouldRevealAdjacent => this.AdjacentMines == 0 && !this.IsMine;

        /// <summary>
        /// Gets a value indicating whether this tile contains a mine.
        /// </summary>
        public bool IsMine => this.tile.IsMine;

        /// <summary>
        /// Gets a value indicating whether this tile has been revealed.
        /// </summary>
        public bool IsRevealed => this.tile.IsRevealed;

        /// <summary>
        /// Gets a value indicating whether this tile has been flagged.
        /// </summary>
        public bool IsFlagged => this.tile.IsFlagged;

        /// <summary>
        /// Gets the number of mines adjacent to this tile.
        /// </summary>
        public int AdjacentMines => this.tile.AdjacentMines;

        /// <summary>
        /// Reveals the tile.
        /// </summary>
        public void Reveal() => this.tile.Reveal();

        /// <summary>
        /// Resets the tile to its initial state.
        /// </summary>
        public void Reset() => this.tile.Reset();

        /// <summary>
        /// Sets this tile as containing a mine.
        /// </summary>
        public void SetMine() => this.tile.SetMine();

        /// <summary>
        /// Sets the number of mines adjacent to this tile.
        /// </summary>
        /// <param name="count">The number of adjacent mines.</param>
        public void SetAdjacentMines(int count) => this.tile.SetAdjacentMines(count);
    }
}
