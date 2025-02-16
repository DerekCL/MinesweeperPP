# Assets Directory

This directory contains all game assets for MinesweeperPP.

## Directory Structure

```
Assets/
├── Textures/           # Game textures and sprites
│   ├── Tiles/         # Tile textures
│   │   ├── Hidden.png
│   │   ├── Revealed.png
│   │   ├── Mine.png
│   │   └── Flag.png
│   ├── UI/            # UI elements
│   │   ├── Buttons/
│   │   └── Icons/
│   └── Backgrounds/   # Background textures
├── Sounds/            # Audio assets
│   ├── SFX/          # Sound effects
│   │   ├── Reveal.wav
│   │   ├── Explode.wav
│   │   └── Flag.wav
│   └── Music/        # Background music
├── Fonts/            # Typography
│   ├── UI/
│   └── Display/
└── Themes/           # Visual themes
    ├── Classic/
    ├── Modern/
    └── Dark/
```

## Asset Guidelines

### Textures
- Format: PNG (transparent) or JPEG
- Resolution: Power of 2 (e.g., 128x128)
- Compression: Optimized for size/quality
- Naming: lowercase-with-hyphens

### Audio
- SFX Format: WAV (uncompressed)
- Music Format: OGG (compressed)
- Sample Rate: 44.1kHz
- Bit Depth: 16-bit
- Normalize to -3dB

### Fonts
- Format: TTF or OTF
- Include license information
- Support multiple weights
- Consider fallback fonts

## Theme Structure

Each theme folder should contain:
```
ThemeName/
├── theme.json         # Theme configuration
├── colors.json        # Color palette
├── textures/         # Theme-specific textures
└── fonts/           # Theme-specific fonts
```

## Asset Optimization

### Textures
1. Compress appropriately
2. Use texture atlases
3. Optimize for target platform
4. Consider mipmaps

### Audio
1. Trim silence
2. Normalize levels
3. Use appropriate compression
4. Consider streaming for music

### Memory Management
1. Implement asset pooling
2. Use lazy loading
3. Manage resource lifetime
4. Clean up unused assets

## Adding New Assets

1. Place in appropriate subdirectory
2. Follow naming conventions
3. Optimize before committing
4. Update asset registry
5. Document special requirements

## Version Control

### Large Files
- Use Git LFS for:
  - Audio files
  - High-res textures
  - Font files
  - Theme packages

### Asset Tracking
- Track binary files
- Document asset versions
- Maintain change history
- Back up source files

## Licensing

### Requirements
- Document asset sources
- Include license files
- Track usage rights
- Credit requirements

### Asset Types
- Original assets
- Licensed assets
- Third-party assets
- Generated assets

## Quality Control

### Review Process
1. Visual quality check
2. Performance impact
3. Memory usage
4. Platform compatibility
5. License verification

### Testing
- Visual regression tests
- Audio playback tests
- Resource loading tests
- Memory leak checks

## Best Practices

1. Keep assets organized
2. Maintain consistent quality
3. Document modifications
4. Consider performance
5. Follow platform guidelines
6. Regular cleanup unused assets
7. Backup source files 
