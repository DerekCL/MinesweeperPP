using System;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Represents the current state of the game.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Game is initialized but first move hasn't been made.
        /// </summary>
        Ready,

        /// <summary>
        /// Game is in progress.
        /// </summary>
        Playing,

        /// <summary>
        /// Game is temporarily suspended.
        /// </summary>
        Paused,

        /// <summary>
        /// Player has successfully revealed all non-mine tiles.
        /// </summary>
        Won,

        /// <summary>
        /// Player has revealed a mine and lost the game.
        /// </summary>
        Lost,
    }
}
