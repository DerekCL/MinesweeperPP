# MinesweeperPP

A modern implementation of the classic Minesweeper game using C# and Godot 4, featuring enhanced gameplay mechanics, customizable themes, and advanced features.

## 🎮 Features

### Core Gameplay (✅ Completed)
- Classic Minesweeper mechanics with modern enhancements
- First-click safety guarantee
- Recursive tile revelation
- Flag placement and validation
- Timer and mine counter
- Pause functionality

### Coming Soon
- Multiple difficulty levels (Beginner, Intermediate, Expert)
- Custom game configuration
- High score system
- Game statistics
- Save/load functionality
- Animations and sound effects
- Theme customization
- Mobile support
- Accessibility features
- Daily challenges
- Achievements
- Online leaderboards

## 🚀 Getting Started

### Prerequisites
- [Godot 4.x](https://godotengine.org/download) with .NET support
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- Visual Studio 2022 or JetBrains Rider (recommended)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/MinesweeperPP.git
   cd MinesweeperPP
   ```

2. Open the project in Godot:
   - Launch Godot
   - Click "Import"
   - Navigate to the cloned repository
   - Select `project.godot`
   - Click "Import & Edit"

3. Build the solution:
   ```bash
   dotnet build
   ```

### Running Tests
```bash
dotnet test
```

## 🏗️ Project Structure

```
MinesweeperPP/
├── Assets/           # Game assets (images, sounds)
├── Documentation/    # Technical specifications and docs
├── Scenes/          # Godot scene files
├── Scripts/         # C# source code
│   ├── Core/        # Core game logic
│   ├── UI/          # UI-related code
│   └── Utils/       # Utility classes
└── Tests/           # Test files
```

## 🛠️ Development

### Code Style
- Follow C# coding conventions
- Use meaningful and descriptive names
- Keep methods focused and small
- Document public APIs with XML comments
- Follow the project's coding standards (see Documentation/CodingStandards.md)

### Workflow
1. Create a new branch for your feature/fix
2. Write tests first (TDD approach)
3. Implement the feature/fix
4. Ensure all tests pass
5. Update documentation if needed
6. Submit a pull request

### Commit Messages
Format:
```
<type>(<scope>): <subject>

<body>

<footer>
```

Types:
- feat: New feature
- fix: Bug fix
- docs: Documentation changes
- style: Code style changes
- refactor: Code refactoring
- test: Test changes
- chore: Build/maintenance changes

## 📝 Documentation

- [Project Overview](Documentation/ProjectOverview.md)
- [Phase 1: Core Implementation](Documentation/Phase1_TechnicalSpec.md)
- [Phase 2: Game Features](Documentation/Phase2_GameFeatures_TechnicalSpec.md)
- [Phase 3: Enhanced UX](Documentation/Phase3_EnhancedUX_TechnicalSpec.md)
- [Phase 4: Advanced Features](Documentation/Phase4_AdvancedFeatures_TechnicalSpec.md)
- [Coding Standards](Documentation/CodingStandards.md)

## 🧪 Testing

The project uses NUnit for testing. Key test categories:
- Unit tests for core game logic
- Integration tests for component interaction
- UI automation tests
- Performance benchmarks

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a pull request

Please read our [Contributing Guidelines](CONTRIBUTING.md) for details.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors

- Your Name - Initial work

## 🙏 Acknowledgments

- Original Minesweeper game
- Godot Engine community
- Contributors and testers 
