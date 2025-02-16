using System;
using Godot;
using MinesweeperPP.Scripts.Core;

namespace MinesweeperPP.Scripts
{
    /// <summary>
    /// Manages the game's user interface elements and interactions.
    /// </summary>
    public partial class UIManager : Control
    {
        /// <summary>
        /// Gets or sets the label displaying the elapsed game time.
        /// </summary>
        required public Label TimerLabel
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the label displaying the number of remaining flags.
        /// </summary>
        required public Label MineCounter
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the label displaying the game status.
        /// </summary>
        required public Label GameStatus
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the button for resetting the game.
        /// </summary>
        required public Button ResetButton
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the button for returning to the main menu.
        /// </summary>
        required public Button MenuButton
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the reference to the game manager.
        /// </summary>
        required public GameManager GameManager
        {
            get; set;
        }

        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// Initializes UI controls and connects signals.
        /// </summary>
        public override void _Ready()
        {
            this.InitializeControls();
            this.ConnectSignals();
        }

        /// <summary>
        /// Called every frame to update the UI elements.
        /// Updates timer display, mine counter, and game status.
        /// </summary>
        /// <param name="delta">Time elapsed since the previous frame.</param>
        public override void _Process(double delta)
        {
            this.UpdateUI();
        }

        private void InitializeControls()
        {
            this.TimerLabel = this.GetNode<Label>("%Timer");
            this.MineCounter = this.GetNode<Label>("%MineCounter");
            this.GameStatus = this.GetNode<Label>("%GameStatus");
            this.ResetButton = this.GetNode<Button>("%ResetButton");
            this.MenuButton = this.GetNode<Button>("%MenuButton");

            // Find GameManager by going up to Game node and then down to GameManager
            var gameNode = this.GetNode<Control>("/root/Game");
            if (gameNode == null)
            {
                GD.PrintErr("Game node not found in scene tree");
                this.GetTree().Quit();
                return;
            }

            this.GameManager = gameNode.GetNode<GameManager>("%GameManager");
            if (this.GameManager == null)
            {
                GD.PrintErr("GameManager not found in Game scene");
                this.GetTree().Quit();
            }
        }

        private void ConnectSignals()
        {
            this.ResetButton.Pressed += this.OnResetPressed;
            this.MenuButton.Pressed += this.OnMenuPressed;
        }

        private void OnResetPressed()
        {
            if (this.GameManager != null)
            {
                this.GameManager.Reset();
            }
        }

        private void OnMenuPressed()
        {
            // Use the ScenePaths constant for consistency
            this.GetTree().ChangeSceneToFile(ScenePaths.Menu);
        }

        private void UpdateUI()
        {
            this.UpdateTimer();
            this.UpdateMineCounter();
            this.UpdateGameStatus();
        }

        private void UpdateTimer()
        {
            this.TimerLabel.Text = this.GameManager.ElapsedTime.ToString();
        }

        private void UpdateMineCounter()
        {
            this.MineCounter.Text = this.GameManager.RemainingFlags.ToString();
        }

        private void UpdateGameStatus()
        {
            this.GameStatus.Text = this.GameManager.GameState switch
            {
                GameState.Ready => "Ready",
                GameState.Playing => "Playing",
                GameState.Paused => "Paused",
                GameState.Won => "You Won!",
                GameState.Lost => "Game Over",
                _ => "Unknown"
            };
        }
    }
}
