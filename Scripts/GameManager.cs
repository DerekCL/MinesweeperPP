using System;
using System.Linq;
using Godot;
using MinesweeperPP.Scripts.Core;

namespace MinesweeperPP.Scripts
{
    /// <summary>
    /// Manages the core game logic for Minesweeper, including game state, tile revelation, and win/loss conditions.
    /// </summary>
    public partial class GameManager : Node2D
    {
        /// <summary>
        /// Gets or sets the grid manager responsible for handling tile layout and mine placement.
        /// </summary>
        required public GridManager GridManager
        {
            get; set;
        }

        /// <summary>
        /// Gets the current state of the game.
        /// </summary>
        public GameState GameState => this.gameState;

        /// <summary>
        /// Gets the elapsed time since the game started.
        /// </summary>
        public GameTime ElapsedTime => this.elapsedTime;

        /// <summary>
        /// Gets the number of flags remaining to be placed.
        /// </summary>
        public FlagCount RemainingFlags => this.remainingFlags;

        /// <summary>
        /// Delegate for handling game state change events.
        /// </summary>
        /// <param name="state">The new game state as an integer.</param>
        public delegate void GameStateChanged(int state);

        /// <summary>
        /// Delegate for handling elapsed time change events.
        /// </summary>
        /// <param name="seconds">The elapsed time in seconds.</param>
        public delegate void ElapsedTimeChanged(double seconds);

        /// <summary>
        /// Delegate for handling remaining flags change events.
        /// </summary>
        /// <param name="flags">The number of flags remaining.</param>
        public delegate void RemainingFlagsChanged(int flags);

        /// <summary>
        /// Delegate for handling game reset events.
        /// </summary>
        public delegate void GameReset();

        /// <summary>
        /// Delegate for handling game pause events.
        /// </summary>
        public delegate void GamePaused();

        /// <summary>
        /// Delegate for handling game resume events.
        /// </summary>
        public delegate void GameResumed();

        private GameState gameState;
        private GameTime elapsedTime;
        private FlagCount remainingFlags;
        private bool isFirstClick;
        private GridPosition firstClickPosition;

        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// Initializes the game manager and loads configuration.
        /// </summary>
        public override void _Ready()
        {
            var gridNode = this.GetNode<Node2D>("%Grid");
            if (gridNode == null)
            {
                var children = this.GetChildren();
                var childNames = string.Join(", ", children.Select(c => c.Name));
                GD.PrintErr($"Grid node not found in scene hierarchy. Available children: {childNames}");
                this.GetTree().Quit();
                return;
            }

            if (gridNode is not GridManager gridManager)
            {
                GD.PrintErr($"Grid node does not have GridManager script attached");
                this.GetTree().Quit();
                return;
            }

            this.GridManager = gridManager;

            // Connect tile click signals
            gridManager.TileLeftClicked += this.OnTileLeftClicked;
            gridManager.TileRightClicked += this.OnTileRightClicked;

            // Get configuration from scene metadata
            var metadata = this.GetTree().Root.GetMeta("GameConfig");
            if (metadata.VariantType == Variant.Type.Nil)
            {
                GD.Print("No game configuration found, using beginner settings");
                this.NewGame(GameConfiguration.CreateBeginner());
                return;
            }

            var configDict = metadata.As<Godot.Collections.Dictionary>();
            if (configDict == null || configDict.Count == 0)
            {
                GD.PrintErr("Invalid game configuration format");
                this.NewGame(GameConfiguration.CreateBeginner());
                return;
            }

            try
            {
                var dimensions = new GameDimensions(
                    configDict["rows"].As<int>(),
                    configDict["columns"].As<int>()
                );
                var mineCount = new MineCount(configDict["mines"].As<int>(), dimensions);
                var config = new GameConfiguration(dimensions, mineCount);
                this.NewGame(config);
            }
            catch (Exception e)
            {
                GD.PrintErr($"Error initializing game: {e.Message}");
                this.NewGame(GameConfiguration.CreateBeginner());
            }
        }

        /// <summary>
        /// Called every frame to update game state.
        /// Updates the game timer when in Playing state.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous frame in seconds.</param>
        public override void _Process(double delta)
        {
            if (this.gameState == GameState.Playing)
            {
                this.UpdateTimer(delta);
            }
        }

        /// <summary>
        /// Starts a new game with the specified configuration.
        /// Initializes the grid, resets game state, and prepares for first move.
        /// </summary>
        /// <param name="config">The game configuration defining grid size and mine count.</param>
        public void NewGame(GameConfiguration config)
        {
            this.GridManager.InitializeGrid(config.Dimensions, config.MineCount);
            this.GridManager.PlaceMines();
            this.gameState = GameState.Ready;
            this.elapsedTime = GameTime.Zero;
            this.remainingFlags = new FlagCount(config.MineCount.Value);
            this.isFirstClick = true;
        }

        /// <summary>
        /// Attempts to reveal a tile at the specified position.
        /// </summary>
        /// <param name="position">The grid position of the tile to reveal.</param>
        /// <returns>The result of the reveal attempt (Safe, Mine, or Invalid).</returns>
        public RevealResult RevealTile(GridPosition position)
        {
            if (this.gameState is GameState.Lost or GameState.Won or GameState.Paused)
            {
                return RevealResult.Invalid;
            }

            var tile = this.GridManager.GetTile(position);

            if (tile.IsFlagged || tile.IsRevealed)
            {
                return RevealResult.Invalid;
            }

            if (this.isFirstClick)
            {
                this.HandleFirstClick(position);
                return RevealResult.Safe;
            }

            this.GridManager.RevealTile(position);

            if (tile.IsMine)
            {
                this.HandleGameOver();
                return RevealResult.Mine;
            }

            if (this.CheckWinCondition())
            {
                this.HandleWin();
            }

            return RevealResult.Safe;
        }

        /// <summary>
        /// Toggles the flag state of a tile at the specified position.
        /// </summary>
        /// <param name="position">The grid position of the tile to toggle.</param>
        /// <returns>True if the flag was toggled successfully, false otherwise.</returns>
        public bool ToggleFlag(GridPosition position)
        {
            if (this.gameState is GameState.Lost or GameState.Won or GameState.Paused)
            {
                return false;
            }

            var tile = this.GridManager.GetTile(position);

            if (tile.IsRevealed)
            {
                return false;
            }

            var currentFlags = this.remainingFlags;

            if (!tile.IsFlagged && currentFlags.Value <= 0)
            {
                return false;
            }

            tile.ToggleFlag();
            this.remainingFlags = tile.IsFlagged ? currentFlags.Decrement() : currentFlags.Increment();
            return true;
        }

        /// <summary>
        /// Pauses the current game.
        /// </summary>
        public void PauseGame()
        {
            if (this.gameState == GameState.Playing)
            {
                this.gameState = GameState.Paused;
            }
        }

        /// <summary>
        /// Resumes the current game from a paused state.
        /// </summary>
        public void ResumeGame()
        {
            if (this.gameState == GameState.Paused)
            {
                this.gameState = GameState.Playing;
            }
        }

        /// <summary>
        /// Resets the current game to its initial state.
        /// </summary>
        public void Reset()
        {
            this.GridManager.Reset();
            this.gameState = GameState.Ready;
            this.elapsedTime = GameTime.Zero;
            this.remainingFlags = new FlagCount(this.GridManager.MineCount.Value);
            this.isFirstClick = true;
        }

        /// <summary>
        /// Gets the tile at the specified position.
        /// </summary>
        /// <param name="position">The grid position to get the tile from.</param>
        /// <returns>The tile at the specified position.</returns>
        public Tile GetTile(GridPosition position)
        {
            return this.GridManager.GetTile(position);
        }

        private void UpdateTimer(double delta)
        {
            this.elapsedTime = this.elapsedTime.Add(delta);
        }

        private void HandleFirstClick(GridPosition position)
        {
            this.firstClickPosition = position;
            this.isFirstClick = false;
            this.gameState = GameState.Playing;
            this.MoveMineFromFirstClick();
            this.GridManager.RevealTile(position);
        }

        private void MoveMineFromFirstClick()
        {
            var tile = this.GridManager.GetTile(this.firstClickPosition);
            if (!tile.IsMine)
            {
                return;
            }

            var nonMinePosition = this.FindFirstTileMatching(t => !t.IsMine && !t.Position.Equals(this.firstClickPosition));
            if (nonMinePosition != null)
            {
                this.GridManager.GetTile(this.firstClickPosition).Reset();
                this.GridManager.GetTile(nonMinePosition.Value).SetMine();
                this.GridManager.CalculateAdjacentMines();
            }
        }

        private void HandleGameOver()
        {
            this.gameState = GameState.Lost;
            this.RevealAllMines();
        }

        private void HandleWin()
        {
            this.gameState = GameState.Won;
        }

        private void RevealAllMines()
        {
            this.ForEachTile(tile =>
            {
                if (tile.IsMine)
                {
                    tile.Reveal();
                }
            });
        }

        private bool CheckWinCondition()
        {
            return !this.AnyTileMatching(tile => !tile.IsMine && !tile.IsRevealed);
        }

        private void ForEachTile(Action<Tile> action)
        {
            var dimensions = this.GridManager.Dimensions;
            for (int row = 0; row < dimensions.Rows; row++)
            {
                for (int col = 0; col < dimensions.Columns; col++)
                {
                    var pos = new GridPosition(row, col);
                    var tile = this.GridManager.GetTile(pos);
                    action(tile);
                }
            }
        }

        private bool AnyTileMatching(Func<Tile, bool> predicate)
        {
            return this.FindFirstTileMatching(predicate) != null;
        }

        private GridPosition? FindFirstTileMatching(Func<Tile, bool> predicate)
        {
            var dimensions = this.GridManager.Dimensions;
            for (int row = 0; row < dimensions.Rows; row++)
            {
                for (int col = 0; col < dimensions.Columns; col++)
                {
                    var pos = new GridPosition(row, col);
                    var tile = this.GridManager.GetTile(pos);
                    if (predicate(tile))
                    {
                        return pos;
                    }
                }
            }

            return null;
        }

        private void OnTileLeftClicked(int row, int col)
        {
            var position = new GridPosition(row, col);
            this.RevealTile(position);
        }

        private void OnTileRightClicked(int row, int col)
        {
            var position = new GridPosition(row, col);
            this.ToggleFlag(position);
        }
    }
}
