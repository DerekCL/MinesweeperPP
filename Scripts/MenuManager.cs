using System.Diagnostics.CodeAnalysis;
using Godot;
using MinesweeperPP.Scripts.Core;

namespace MinesweeperPP.Scripts
{
    /// <summary>
    /// Manages the main menu interface and game initialization.
    /// </summary>
    public partial class MenuManager : Control
    {
        /// <summary>
        /// Gets or sets the button for starting a new game with beginner difficulty.
        /// </summary>
        required public Button BeginnerButton
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the button for starting a new game with intermediate difficulty.
        /// </summary>
        required public Button IntermediateButton
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the button for starting a new game with expert difficulty.
        /// </summary>
        required public Button ExpertButton
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the button for opening the custom game configuration dialog.
        /// </summary>
        required public Button CustomGameButton
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the button for opening the settings menu.
        /// </summary>
        required public Button SettingsButton
        {
            get; set;
        }

        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// Initializes menu buttons and connects signals.
        /// </summary>
        public override void _Ready()
        {
            this.BeginnerButton = this.GetNode<Button>("CenterContainer/VBoxContainer/DifficultyButtons/BeginnerButton");
            this.IntermediateButton = this.GetNode<Button>("CenterContainer/VBoxContainer/DifficultyButtons/IntermediateButton");
            this.ExpertButton = this.GetNode<Button>("CenterContainer/VBoxContainer/DifficultyButtons/ExpertButton");
            this.CustomGameButton = this.GetNode<Button>("CenterContainer/VBoxContainer/CustomGameButton");
            this.SettingsButton = this.GetNode<Button>("CenterContainer/VBoxContainer/SettingsButton");

            this.ConnectSignals();
        }

        // Placeholder until custom game dialog is implemented in Phase 2
        private static void OnCustomGamePressed()
        {
            GD.Print("Custom game dialog will be implemented in Phase 2");
        }

        private void ConnectSignals()
        {
            this.BeginnerButton.Pressed += () => this.StartGame(GameConfiguration.CreateBeginner());
            this.IntermediateButton.Pressed += () => this.StartGame(GameConfiguration.CreateIntermediate());
            this.ExpertButton.Pressed += () => this.StartGame(GameConfiguration.CreateExpert());
            this.CustomGameButton.Pressed += OnCustomGamePressed;
            this.SettingsButton.Pressed += this.OnSettingsPressed;
        }

        /// <summary>
        /// Starts a new game with the specified configuration.
        /// Must be an instance method to access Godot's scene tree.
        /// </summary>
        /// <param name="config">The game configuration to use.</param>
        private void StartGame(GameConfiguration config)
        {
            var configData = new Godot.Collections.Dictionary
            {
                { "rows", Variant.CreateFrom(config.Dimensions.Rows) },
                { "columns", Variant.CreateFrom(config.Dimensions.Columns) },
                { "mines", Variant.CreateFrom(config.MineCount.Value) },
            };
            this.GetTree().Root.SetMeta("GameConfig", configData);
            this.GetTree().ChangeSceneToFile("res://Scenes/Game.tscn");
        }

        /// <summary>
        /// Opens the settings menu.
        /// Must be an instance method to access Godot's scene tree.
        /// </summary>
        private void OnSettingsPressed()
        {
            this.GetTree().ChangeSceneToFile("res://Scenes/Settings.tscn");
        }
    }
}
