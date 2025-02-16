using System.Collections.Generic;
using Godot;

namespace MinesweeperPP.Scripts
{
    /// <summary>
    /// Represents a single tile in the Minesweeper game grid.
    /// Handles the tile's state (mine, revealed, flagged) and visual representation.
    /// </summary>
    public partial class Tile : Node2D
    {
        /// <summary>
        /// Maps the number of adjacent mines to their display colors.
        /// </summary>
        private static readonly Dictionary<int, Color> NumberColors = new Dictionary<int, Color>()
        {
            { 1, new Color(0, 0.6f, 1) },     // Blue
            { 2, new Color(0, 0.8f, 0) },     // Green
            { 3, new Color(1, 0, 0) },        // Red
            { 4, new Color(0, 0, 0.8f) },     // Dark Blue
            { 5, new Color(0.8f, 0, 0) },     // Dark Red
            { 6, new Color(0, 0.8f, 0.8f) },  // Cyan
            { 7, new Color(0.5f, 0, 0.5f) },  // Purple
            { 8, new Color(0.5f, 0.5f, 0.5f) }, // Gray
        };

        /// <summary>
        /// Default color for numbers not in the NumberColors dictionary.
        /// </summary>
        private static readonly Color DefaultNumberColor = new Color(1, 1, 1); // White

        /// <summary>
        /// Gets a color for displaying the given number of adjacent mines.
        /// </summary>
        /// <param name="number">The number of adjacent mines.</param>
        /// <returns>The color to use for displaying the number.</returns>
        private static Color GetNumberColor(int number) =>
            NumberColors.TryGetValue(number, out var color) ? color : DefaultNumberColor;

        private bool isMine;
        private bool isRevealed;
        private bool isFlagged;
        private int adjacentMines;

        private Label? numberLabel;
        private ColorRect? background;
        private ColorRect? flagSprite;
        private ColorRect? mineSprite;
        private Control? clickArea;

        /// <summary>
        /// Signals that the tile has been left-clicked.
        /// </summary>
        [Signal]
        public delegate void LeftClickedEventHandler();

        /// <summary>
        /// Signals that the tile has been right-clicked.
        /// </summary>
        [Signal]
        public delegate void RightClickedEventHandler();

        /// <summary>
        /// Gets a value indicating whether this tile contains a mine.
        /// </summary>
        public bool IsMine => this.isMine;

        /// <summary>
        /// Gets a value indicating whether this tile has been revealed.
        /// </summary>
        public bool IsRevealed => this.isRevealed;

        /// <summary>
        /// Gets a value indicating whether this tile has been flagged.
        /// </summary>
        public bool IsFlagged => this.isFlagged;

        /// <summary>
        /// Gets the number of mines adjacent to this tile.
        /// </summary>
        public int AdjacentMines => this.adjacentMines;

        /// <summary>
        /// Initializes the tile's components and sets up input handling.
        /// </summary>
        public override void _Ready()
        {
            if (!this.InitializeNodes())
            {
                return;
            }

            // Connect input signals
            this.clickArea!.GuiInput += this.OnGuiInput;

            // Hide number initially
            this.numberLabel!.Visible = false;
            this.UpdateVisualState();
        }

        /// <summary>
        /// Sets this tile to contain a mine.
        /// </summary>
        public void SetMine()
        {
            this.isMine = true;
            this.UpdateVisualState();
        }

        /// <summary>
        /// Sets the number of mines adjacent to this tile.
        /// </summary>
        /// <param name="count">The number of adjacent mines.</param>
        public void SetAdjacentMines(int count)
        {
            this.adjacentMines = count;
            this.UpdateVisualState();
        }

        /// <summary>
        /// Reveals the tile's contents.
        /// </summary>
        public void Reveal()
        {
            this.isRevealed = true;
            this.UpdateVisualState();
        }

        /// <summary>
        /// Toggles the flagged state of the tile if it hasn't been revealed.
        /// </summary>
        public void ToggleFlag()
        {
            if (!this.isRevealed)
            {
                this.isFlagged = !this.isFlagged;
                this.UpdateVisualState();
            }
        }

        /// <summary>
        /// Resets the tile to its initial state.
        /// </summary>
        public void Reset()
        {
            this.isMine = false;
            this.isRevealed = false;
            this.isFlagged = false;
            this.adjacentMines = 0;
            this.UpdateVisualState();
        }

        private bool InitializeNodes()
        {
            this.numberLabel = this.GetNode<Label>("%Number");
            this.background = this.GetNode<ColorRect>("Background");
            this.flagSprite = this.GetNode<ColorRect>("%FlagSprite");
            this.mineSprite = this.GetNode<ColorRect>("%MineSprite");
            this.clickArea = this.GetNode<Control>("ClickArea");

            if (!this.ValidateNodes())
            {
                GD.PrintErr("Failed to initialize one or more required nodes in Tile");
                return false;
            }

            return true;
        }

        private bool ValidateNodes()
        {
            return this.numberLabel != null
                && this.background != null
                && this.flagSprite != null
                && this.mineSprite != null
                && this.clickArea != null;
        }

        private void OnGuiInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseButton && mouseButton.Pressed)
            {
                switch (mouseButton.ButtonIndex)
                {
                    case MouseButton.Left:
                        this.EmitSignal(SignalName.LeftClicked);
                        break;
                    case MouseButton.Right:
                        this.EmitSignal(SignalName.RightClicked);
                        break;
                }
            }
        }

        private void UpdateVisualState()
        {
            if (!this.ValidateNodes())
            {
                return;
            }

            this.UpdateBackgroundColor();
            this.UpdateFlagVisibility();
            this.UpdateMineVisibility();
            this.UpdateNumberDisplay();
        }

        private void UpdateBackgroundColor()
        {
            this.background!.Color = this.isRevealed
                ? new Color(0.3f, 0.3f, 0.3f)
                : new Color(0.2f, 0.2f, 0.2f);
        }

        private void UpdateFlagVisibility()
        {
            this.flagSprite!.Visible = this.isFlagged && !this.isRevealed;
        }

        private void UpdateMineVisibility()
        {
            this.mineSprite!.Visible = this.isRevealed && this.isMine;
        }

        private void UpdateNumberDisplay()
        {
            bool shouldShowNumber = this.isRevealed && !this.isMine && this.adjacentMines > 0;
            this.numberLabel!.Visible = shouldShowNumber;

            if (shouldShowNumber)
            {
                this.numberLabel!.Text = this.adjacentMines.ToString(System.Globalization.CultureInfo.InvariantCulture);
                this.numberLabel!.AddThemeColorOverride("font_color", GetNumberColor(this.adjacentMines));
            }
        }
    }
}
