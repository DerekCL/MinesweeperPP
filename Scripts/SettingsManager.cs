using System;
using System.Linq;
using Godot;
using MinesweeperPP.Scripts.Core;

namespace MinesweeperPP.Scripts
{
    /// <summary>
    /// Manages game settings and the settings menu interface.
    /// </summary>
    public partial class SettingsManager : Control
    {
        /// <summary>
        /// Gets or sets the Apply button.
        /// </summary>
        required public Button ApplyButton
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Cancel button.
        /// </summary>
        required public Button CancelButton
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the sound volume slider.
        /// </summary>
        required public Slider SoundSlider
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the music volume slider.
        /// </summary>
        required public Slider MusicSlider
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the theme selection dropdown.
        /// </summary>
        required public OptionButton ThemeOption
        {
            get; set;
        }

        private Settings currentSettings;
        private Settings pendingSettings;

        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            this.InitializeControls();
            this.ConnectSignals();
            this.LoadCurrentSettings();
        }

        private void InitializeControls()
        {
            this.ApplyButton = this.GetNode<Button>("%ApplyButton");
            this.CancelButton = this.GetNode<Button>("%CancelButton");
            this.SoundSlider = this.GetNode<Slider>("%SoundSlider");
            this.MusicSlider = this.GetNode<Slider>("%MusicSlider");
            this.ThemeOption = this.GetNode<OptionButton>("%ThemeOption");

            var missingControls = new (Node Control, string Name)[]
            {
                (Control: this.ApplyButton, Name: "ApplyButton"),
                (Control: this.CancelButton, Name: "CancelButton"),
                (Control: this.SoundSlider, Name: "SoundSlider"),
                (Control: this.MusicSlider, Name: "MusicSlider"),
                (Control: this.ThemeOption, Name: "ThemeOption"),
            };

            var missing = missingControls.FirstOrDefault(x => x.Control == null);
            if (missing != default)
            {
                GD.PrintErr($"Failed to initialize {missing.Name}");
                this.GetTree().Quit();
            }
        }

        private void ConnectSignals()
        {
            this.ApplyButton.Pressed += this.OnApplyPressed;
            this.CancelButton.Pressed += this.OnCancelPressed;
            this.SoundSlider.ValueChanged += this.OnSettingChanged;
            this.MusicSlider.ValueChanged += this.OnSettingChanged;
            this.ThemeOption.ItemSelected += this.OnSettingChanged;
        }

        private void LoadCurrentSettings()
        {
            this.currentSettings = SettingsStorage.Load();
            this.pendingSettings = this.currentSettings;
            this.UpdateControlValues(this.currentSettings);
            this.ApplyButton.Disabled = true; // Initialize Apply button as disabled
        }

        private void UpdateControlValues(Settings settings)
        {
            this.SoundSlider.Value = settings.SoundVolume;
            this.MusicSlider.Value = settings.MusicVolume;
            this.ThemeOption.Selected = (int)settings.Theme;
        }

        private void SaveSettings()
        {
            this.pendingSettings = new Settings(
                this.currentSettings.GameConfig, // Keep current game config for now
                (GameTheme)this.ThemeOption.Selected,
                (float)this.SoundSlider.Value,
                (float)this.MusicSlider.Value
            );

            if (SettingsStorage.Save(this.pendingSettings))
            {
                this.currentSettings = this.pendingSettings;
            }
            else
            {
                GD.PrintErr("Failed to save settings");
            }
        }

        /// <summary>
        /// Handles changes to numeric settings (sound/music volume).
        /// </summary>
        /// <param name="value">The new value from the slider.</param>
        private void OnSettingChanged(double value)
        {
            // Enable the Apply button since we have pending changes
            this.ApplyButton.Disabled = false;

            // Validate volume ranges (should be between 0 and 1)
            if (value < 0 || value > 1)
            {
                GD.PrintErr($"Invalid volume value: {{0}}", value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                return;
            }

            const double Epsilon = 0.0001;
            bool hasChanges = Math.Abs(value - this.currentSettings.SoundVolume) > Epsilon ||
                            Math.Abs(value - this.currentSettings.MusicVolume) > Epsilon;

            if (hasChanges)
            {
                this.ApplyButton.Modulate = new Color(1, 1, 0); // Yellow tint to indicate pending changes
            }
        }

        /// <summary>
        /// Handles changes to the theme selection.
        /// </summary>
        /// <param name="index">The selected theme index.</param>
        private void OnSettingChanged(long index)
        {
            // Enable the Apply button since we have pending changes
            this.ApplyButton.Disabled = false;

            // Validate theme index
            if (index < 0 || index >= Enum.GetValues<GameTheme>().Length)
            {
                GD.PrintErr($"Invalid theme index: {{0}}", index.ToString(System.Globalization.CultureInfo.InvariantCulture));
                return;
            }

            // Update the UI to reflect the change
            if (index != (long)this.currentSettings.Theme)
            {
                this.ApplyButton.Modulate = new Color(1, 1, 0); // Yellow tint to indicate pending changes
            }
        }

        private void OnApplyPressed()
        {
            this.SaveSettings();
            this.ApplyButton.Modulate = new Color(1, 1, 1); // Reset button color
            this.ApplyButton.Disabled = true; // Disable button after applying
            this.GetTree().ChangeSceneToFile(ScenePaths.Menu);
        }

        private void OnCancelPressed()
        {
            this.GetTree().ChangeSceneToFile(ScenePaths.Menu);
        }
    }
}
