# Scripts Directory

This directory contains all C# source code for the MinesweeperPP game.

## Directory Structure

```
Scripts/
├── Core/          # Core game logic and data structures
│   ├── Tile.cs             # Individual tile behavior
│   ├── GridManager.cs      # Grid management and mine placement
│   ├── GameManager.cs      # Game state and rules
│   └── GameConfiguration.cs # Game settings and difficulty
├── UI/           # User interface components
│   ├── UIManager.cs        # Main UI controller
│   ├── ThemeManager.cs     # Theme handling
│   └── AudioManager.cs     # Sound effects and music
└── Utils/        # Utility classes and helpers
    ├── Extensions/         # Extension methods
    ├── Constants.cs        # Game constants
    └── Helpers.cs          # Helper functions
```

## Core Components

### Tile
- Represents a single tile in the game grid
- Manages tile state (revealed, flagged, mined)
- Handles tile interactions

### GridManager
- Manages the game grid
- Handles mine placement and counting
- Provides grid traversal and validation

### GameManager
- Controls game flow and state
- Manages win/loss conditions
- Handles timer and scoring

## UI Components

### UIManager
- Controls UI elements and layout
- Manages user input
- Updates displays (timer, mine counter)

### ThemeManager
- Handles visual themes
- Manages assets and colors
- Controls UI styling

### AudioManager
- Controls sound effects
- Manages background music
- Handles audio settings

## Utils

### Extensions
- List extensions (Shuffle, etc.)
- LINQ extensions
- Type conversions

### Constants
- Game configuration constants
- UI constants
- Default settings

### Helpers
- Common utility functions
- Math helpers
- Validation methods

## Coding Guidelines

1. Follow C# coding conventions
2. Document public APIs with XML comments
3. Keep classes focused and single-responsibility
4. Use dependency injection where appropriate
5. Write unit tests for new functionality

## Adding New Scripts

1. Place in appropriate subdirectory
2. Follow naming conventions
3. Add XML documentation
4. Create corresponding test file
5. Update this README if adding new categories 
