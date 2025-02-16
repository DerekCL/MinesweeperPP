using System;

namespace MinesweeperPP.Scripts
{
    /// <summary>
    /// Represents the result of attempting to reveal a tile.
    /// </summary>
    public enum RevealResult
    {
        /// <summary>
        /// Tile was successfully revealed and contained no mine.
        /// </summary>
        Safe,

        /// <summary>
        /// Tile contained a mine, resulting in game over.
        /// </summary>
        Mine,

        /// <summary>
        /// Tile could not be revealed (already revealed, flagged, or game not in playing state).
        /// </summary>
        Invalid,
    }
}
