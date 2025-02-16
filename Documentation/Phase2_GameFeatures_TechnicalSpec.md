# Phase 2: Game Features Technical Specification

## 1. Difficulty Levels

### Implementation Details
- Create `GameDifficulty` enum with predefined levels:
  ```csharp
  public enum GameDifficulty
  {
      Beginner,     // 9x9 grid, 10 mines
      Intermediate, // 16x16 grid, 40 mines
      Expert,       // 30x16 grid, 99 mines
      Custom        // User-defined dimensions and mine count
  }
  ```
- Add difficulty selection UI in main menu
- Store last used difficulty in user preferences

### Acceptance Criteria
- [ ] All predefined difficulty levels function correctly
- [ ] UI updates to reflect current difficulty
- [ ] Difficulty persists between game sessions
- [ ] Grid resizes appropriately for each difficulty

## 2. Custom Game Configuration

### Implementation Details
- Create `GameConfiguration` class:
  ```csharp
  public class GameConfiguration
  {
      public int Rows { get; set; }
      public int Columns { get; set; }
      public int MineCount { get; set; }
      public bool ValidateConfiguration() { ... }
  }
  ```
- Add custom game dialog with input validation
- Implement configuration persistence

### Acceptance Criteria
- [ ] Users can specify custom grid dimensions (8-30 rows, 8-30 columns)
- [ ] Mine count validation (minimum 10% of tiles, maximum 90%)
- [ ] Configuration persists between sessions
- [ ] Input validation with meaningful error messages

## 3. High Score System

### Implementation Details
- Create `HighScore` class:
  ```csharp
  public class HighScore
  {
      public string PlayerName { get; set; }
      public TimeSpan Time { get; set; }
      public GameDifficulty Difficulty { get; set; }
      public DateTime Date { get; set; }
  }
  ```
- Implement high score persistence using JSON
- Add high score display UI
- Sort scores by difficulty and time

### Acceptance Criteria
- [ ] Records best times for each difficulty level
- [ ] Displays top 10 scores per difficulty
- [ ] Persists between game sessions
- [ ] Custom games excluded from high scores

## 4. Game Statistics

### Implementation Details
- Create `GameStatistics` class:
  ```csharp
  public class GameStatistics
  {
      public int GamesPlayed { get; set; }
      public int GamesWon { get; set; }
      public TimeSpan BestTime { get; set; }
      public double WinPercentage { get; set; }
      public Dictionary<GameDifficulty, DifficultyStats> PerDifficultyStats { get; set; }
  }
  ```
- Track statistics per difficulty level
- Implement statistics persistence
- Add statistics view UI

### Acceptance Criteria
- [ ] Tracks games played, won, and win percentage
- [ ] Records best times per difficulty
- [ ] Calculates and displays win streaks
- [ ] Statistics persist between sessions

## 5. Save/Load Game State

### Implementation Details
- Create `GameState` serialization class:
  ```csharp
  public class SavedGameState
  {
      public GameConfiguration Config { get; set; }
      public TileState[,] Grid { get; set; }
      public TimeSpan ElapsedTime { get; set; }
      public int RemainingFlags { get; set; }
      public DateTime SaveTime { get; set; }
  }
  ```
- Implement auto-save feature
- Add manual save/load functionality
- Create save game browser UI

### Acceptance Criteria
- [ ] Auto-saves game state on exit
- [ ] Allows manual save/load operations
- [ ] Maintains multiple save slots
- [ ] Validates save data integrity

## Timeline
1. Week 1-2: Difficulty Levels & Custom Configuration
2. Week 3-4: High Score System
3. Week 5-6: Game Statistics
4. Week 7-8: Save/Load Functionality

## Dependencies
- Newtonsoft.Json for data serialization
- Godot's built-in file system access
- User preferences system

## Testing Requirements
- Unit tests for all new classes
- Integration tests for save/load functionality
- UI tests for new dialogs and menus
- Performance testing for large grids
- Save data migration tests 
