# Contributing to MinesweeperPP

First off, thank you for considering contributing to MinesweeperPP! It's people like you that make MinesweeperPP such a great game.

## Code of Conduct

This project and everyone participating in it is governed by our Code of Conduct. By participating, you are expected to uphold this code.

## How Can I Contribute?

### Reporting Bugs

Before creating bug reports, please check the issue list as you might find out that you don't need to create one. When you are creating a bug report, please include as many details as possible:

* Use a clear and descriptive title
* Describe the exact steps to reproduce the problem
* Provide specific examples to demonstrate the steps
* Describe the behavior you observed after following the steps
* Explain which behavior you expected to see instead and why
* Include screenshots if possible
* Include your environment details (OS, Godot version, etc.)

### Suggesting Enhancements

Enhancement suggestions are tracked as GitHub issues. When creating an enhancement suggestion, please include:

* A clear and descriptive title
* A detailed description of the proposed functionality
* Explain why this enhancement would be useful
* List any similar features in other games if you know of any
* Include mockups or sketches if applicable

### Pull Requests

* Follow our coding standards (see Documentation/CodingStandards.md)
* Write tests for new features
* Update documentation for significant changes
* Follow the commit message guidelines
* Include screenshots in your pull request if there are visual changes

## Development Process

1. Fork the repo and create your branch from `main`
2. If you've added code that should be tested, add tests
3. If you've changed APIs, update the documentation
4. Ensure the test suite passes
5. Make sure your code follows our coding standards
6. Issue that pull request!

### Test-Driven Development

We follow a test-driven development approach:

1. Write a failing test that describes the feature/fix
2. Implement the minimum code to make the test pass
3. Refactor while keeping tests green
4. Repeat

### Git Workflow

1. Update your local `main`:
   ```bash
   git checkout main
   git pull upstream main
   ```

2. Create a new branch:
   ```bash
   git checkout -b feature/your-feature-name
   ```

3. Work on your feature:
   * Commit early and often
   * Follow commit message guidelines
   * Keep your branch up to date with main

4. Push your branch:
   ```bash
   git push origin feature/your-feature-name
   ```

5. Create a Pull Request

### Commit Messages

Format:
```
<type>(<scope>): <subject>

<body>

<footer>
```

Types:
* feat: New feature
* fix: Bug fix
* docs: Documentation changes
* style: Code style changes
* refactor: Code refactoring
* test: Test changes
* chore: Build/maintenance changes

Example:
```
feat(game): add difficulty levels

- Add GameDifficulty enum
- Implement difficulty selection UI
- Store user preferences

Closes #123
```

## Documentation

* Update README.md if needed
* Add/update XML documentation for public APIs
* Update technical specifications if needed
* Include code examples for new features

## Review Process

* All submissions require review
* Changes will be tested by automated systems
* Reviews should focus on:
  * Code quality and style
  * Test coverage
  * Documentation completeness
  * Performance implications
  * Security considerations

## Community

* Join our Discord server for discussions
* Participate in issue discussions
* Help others in pull request reviews
* Share your ideas and feedback

## Recognition

Contributors will be added to our README.md and CONTRIBUTORS.md files.

Thank you for your contributions! 
