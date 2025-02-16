using System;
using Godot;

namespace MinesweeperPP.Scripts.Core
{
    /// <summary>
    /// Handles loading and saving game settings.
    /// </summary>
    public static class SettingsStorage
    {
        private const string SettingsPath = "user://settings.cfg";
        private const string SectionName = "Settings";

        /// <summary>
        /// Loads settings from disk. Returns default settings if no saved settings exist.
        /// </summary>
        /// <returns>The loaded settings.</returns>
        public static Settings Load()
        {
            var config = new ConfigFile();
            var error = config.Load(SettingsPath);

            if (error != Error.Ok)
            {
                return Settings.Default;
            }

            try
            {
                return new Settings(
                    LoadGameConfig(config),
                    LoadTheme(config),
                    LoadFloat(config, "sound_volume", 1.0f),
                    LoadFloat(config, "music_volume", 0.5f)
                );
            }
            catch (Exception e)
            {
                GD.PrintErr($"Error loading settings: {e.Message}");
                return Settings.Default;
            }
        }

        /// <summary>
        /// Saves settings to disk.
        /// </summary>
        /// <param name="settings">The settings to save.</param>
        /// <returns>True if save was successful, false otherwise.</returns>
        public static bool Save(Settings settings)
        {
            var config = new ConfigFile();

            try
            {
                // Game settings
                var gameConfig = settings.GameConfig;
                config.SetValue(SectionName, "rows", gameConfig.Dimensions.Rows);
                config.SetValue(SectionName, "columns", gameConfig.Dimensions.Columns);
                config.SetValue(SectionName, "mines", gameConfig.MineCount.Value);

                // Visual settings
                config.SetValue(SectionName, "theme", (int)settings.Theme);

                // Audio settings
                config.SetValue(SectionName, "sound_volume", settings.SoundVolume);
                config.SetValue(SectionName, "music_volume", settings.MusicVolume);

                var error = config.Save(SettingsPath);
                return error == Error.Ok;
            }
            catch (Exception e)
            {
                GD.PrintErr($"Error saving settings: {e.Message}");
                return false;
            }
        }

        private static GameConfiguration LoadGameConfig(ConfigFile config)
        {
            var rows = (int)config.GetValue(SectionName, "rows", 9);
            var columns = (int)config.GetValue(SectionName, "columns", 9);
            var mines = (int)config.GetValue(SectionName, "mines", 10);

            var dimensions = new GameDimensions(rows, columns);
            var mineCount = new MineCount(mines, dimensions);
            return new GameConfiguration(dimensions, mineCount);
        }

        private static GameTheme LoadTheme(ConfigFile config)
        {
            var themeValue = (int)config.GetValue(SectionName, "theme", (int)GameTheme.Classic);
            return Enum.IsDefined(typeof(GameTheme), themeValue)
                ? (GameTheme)themeValue
                : GameTheme.Classic;
        }

        private static float LoadFloat(ConfigFile config, string key, float defaultValue)
        {
            var value = (float)config.GetValue(SectionName, key, defaultValue);
            return Math.Clamp(value, 0.0f, 1.0f);
        }
    }
}
