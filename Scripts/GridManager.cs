using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using MinesweeperPP.Scripts.Core;
using MinesweeperPP.Scripts.Utils;

namespace MinesweeperPP.Scripts
{
    /// <summary>
    /// Manages the game grid, including tile placement, mine generation, and grid operations.
    /// </summary>
    public partial class GridManager : Node2D
    {
        private const int TileSize = 32;

        private GameDimensions dimensions;
        private MineCount mineCount;

        /// <summary>
        /// Gets the dimensions of the game grid.
        /// </summary>
        public GameDimensions Dimensions => this.dimensions;

        /// <summary>
        /// Gets the number of mines in the game grid.
        /// </summary>
        public MineCount MineCount => this.mineCount;

        /// <summary>
        /// Gets or sets the grid of tiles.
        /// </summary>
        required public Tile[,] Grid
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the tile scene to instantiate.
        /// </summary>
        required public PackedScene TileScene
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the container for tile instances.
        /// </summary>
        required public Node2D TileContainer
        {
            get; set;
        }

        /// <summary>
        /// Event handler for when a tile is left-clicked.
        /// </summary>
        /// <param name="row">The row of the clicked tile.</param>
        /// <param name="col">The column of the clicked tile.</param>
        [Signal]
        public delegate void TileLeftClickedEventHandler(int row, int col);

        /// <summary>
        /// Event handler for when a tile is right-clicked.
        /// </summary>
        /// <param name="row">The row of the clicked tile.</param>
        /// <param name="col">The column of the clicked tile.</param>
        [Signal]
        public delegate void TileRightClickedEventHandler(int row, int col);

        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// Initializes the tile scene and container.
        /// </summary>
        public override void _Ready()
        {
            this.TileScene = GD.Load<PackedScene>(ScenePaths.Tile);
            this.TileContainer = this.GetNode<Node2D>("TileContainer");
        }

        /// <summary>
        /// Initializes the game grid with the specified dimensions and mine count.
        /// </summary>
        /// <param name="dimensions">The dimensions of the grid.</param>
        /// <param name="mineCount">The number of mines to place.</param>
        public void InitializeGrid(GameDimensions dimensions, MineCount mineCount)
        {
            this.dimensions = dimensions;
            this.mineCount = mineCount;
            this.Grid = new Tile[dimensions.Rows, dimensions.Columns];
            this.CreateTiles();
            this.UpdateBackgroundSize();
        }

        /// <summary>
        /// Places mines randomly in the grid.
        /// </summary>
        public void PlaceMines()
        {
            var minePositions = this.GetRandomMinePositions();
            this.PlaceMinesAtPositions(minePositions);
            this.CalculateAdjacentMines();
        }

        /// <summary>
        /// Calculates the number of adjacent mines for each tile in the grid.
        /// </summary>
        public void CalculateAdjacentMines()
        {
            foreach (var pos in GridPosition.GetAllPositions(this.dimensions))
            {
                var state = this.GetTileState(pos);
                if (!state.IsMine)
                {
                    int count = this.CountAdjacentMines(pos);
                    state.SetAdjacentMines(count);
                }
            }
        }

        /// <summary>
        /// Reveals a tile at the specified position.
        /// If the tile has no adjacent mines, reveals adjacent tiles recursively.
        /// </summary>
        /// <param name="pos">The position of the tile to reveal.</param>
        public void RevealTile(GridPosition pos)
        {
            if (!pos.IsValid(this.dimensions))
            {
                return;
            }

            var state = this.GetTileState(pos);
            if (!state.CanReveal)
            {
                return;
            }

            state.Reveal();

            if (state.ShouldRevealAdjacent)
            {
                this.RevealAdjacentTiles(pos);
            }
        }

        /// <summary>
        /// Gets the tile at the specified position.
        /// </summary>
        /// <param name="pos">The position of the tile to get.</param>
        /// <returns>The tile at the specified position.</returns>
        /// <exception cref="ArgumentException">Thrown when the position is invalid.</exception>
        public Tile GetTile(GridPosition pos)
        {
            if (!pos.IsValid(this.dimensions))
            {
                throw new ArgumentException("Position is outside the grid bounds.", nameof(pos));
            }

            return this.Grid[pos.Row, pos.Col];
        }

        /// <summary>
        /// Resets all tiles in the grid to their initial state.
        /// </summary>
        public void Reset()
        {
            foreach (var pos in GridPosition.GetAllPositions(this.dimensions))
            {
                this.GetTileState(pos).Reset();
            }
        }

        /// <summary>
        /// Checks if all tiles in the grid match a specified condition.
        /// </summary>
        /// <param name="predicate">The condition to check for each tile.</param>
        /// <returns>true if all tiles match the condition; otherwise, false.</returns>
        public bool AllTilesMatch(Func<TileState, bool> predicate)
        {
            foreach (var pos in GridPosition.GetAllPositions(this.dimensions))
            {
                if (!predicate(this.GetTileState(pos)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Performs an action on each tile in the grid.
        /// </summary>
        /// <param name="action">The action to perform on each tile.</param>
        public void ForEachTile(Action<TileState> action)
        {
            foreach (var pos in GridPosition.GetAllPositions(this.dimensions))
            {
                action(this.GetTileState(pos));
            }
        }

        private TileState GetTileState(GridPosition pos)
        {
            return new TileState(this.Grid[pos.Row, pos.Col]);
        }

        private List<GridPosition> GetRandomMinePositions()
        {
            var positions = GridPosition.GetAllPositions(this.dimensions).ToList();
            positions.Shuffle();
            return positions.Take(this.mineCount.Value).ToList();
        }

        private void PlaceMinesAtPositions(List<GridPosition> positions)
        {
            foreach (var pos in positions)
            {
                this.GetTileState(pos).SetMine();
            }
        }

        private int CountAdjacentMines(GridPosition pos)
        {
            return pos.GetAdjacentPositions(this.dimensions)
                .Count(p => this.GetTileState(p).IsMine);
        }

        private void RevealAdjacentTiles(GridPosition pos)
        {
            foreach (var adjacentPos in pos.GetAdjacentPositions(this.dimensions))
            {
                this.RevealTile(adjacentPos);
            }
        }

        private void CreateTiles()
        {
            // Clear any existing tiles
            foreach (var child in this.TileContainer.GetChildren())
            {
                child.QueueFree();
            }

            foreach (var pos in GridPosition.GetAllPositions(this.dimensions))
            {
                var tileInstance = this.TileScene.Instantiate<Tile>();
                this.TileContainer.AddChild(tileInstance);

                // Position the tile
                tileInstance.Position = new Vector2(pos.Col * TileSize, pos.Row * TileSize);

                // Store reference and connect signals
                this.Grid[pos.Row, pos.Col] = tileInstance;
                tileInstance.LeftClicked += () => this.EmitSignal(SignalName.TileLeftClicked, pos.Row, pos.Col);
                tileInstance.RightClicked += () => this.EmitSignal(SignalName.TileRightClicked, pos.Row, pos.Col);
            }
        }

        private void UpdateBackgroundSize()
        {
            var background = this.GetNode<ColorRect>("Background");
            background.Size = new Vector2(
                this.dimensions.Columns * TileSize,
                this.dimensions.Rows * TileSize
            );
        }
    }
}
