# Extension-esque-MonoGameLibrary
A modular, dependency-injected game framework built on **MonoGame**. Designed for maintainability, testability, and platform abstraction.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)

The MonoGameLibrary provides a clean separation of concerns with Core (platform-agnostic), Adapters (MonoGame bindings), and Extensions (optional modules). It features explicit dependency injection, standardized lifecycle interfaces, and a flexible host system for building maintainable and testable games. 

## Features

- **Core** – Platform-agnostic interfaces (`IContentService`, `IThreadPool`, `ILogger`, etc.)
- **Adapters** – MonoGame bindings (`MonoGameContentService`, `MonoGameAdapter`, `GumService`)
- **Extensions** – Optional modules: Audio, Input, Scenes, Graphics (Sprite, Atlas, Tilemap)
- **Dependency Injection** – Explicit constructor injection, no service locator
- **Lifecycle Management** – Standardized `ILoadable`/`IUpdateable`/`IDrawable` with order/visible flags
- **Error Handling** – Centralized `OnError` callback for module exceptions
- **Thread-Safe Host** – `GameHost` with operation counting and safe shutdown

## Getting Started

### Prerequisites
- [.NET 8.0 SDK or higher](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [MonoGame 3.8+](https://www.monogame.net/)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/mushroomcowisheggs/Extension-esque-MonoGameLibrary.git
   ```
2. Add a reference to `MonoGameLibrary.csproj` in your game project.

### Basic Usage (Composition Root)
```csharp
using MonoGameLibrary.Core.Hosting;
using MonoGameLibrary.Adapters.MonoGame;
using MonoGameLibrary.Extensions;

var builder = new GameBuilder();
builder.UseDefaultServices();
builder.UseAudio();
builder.UseInput();

var serviceContent = new MonoGameContentService(Content);
builder.RegisterService<IContentService>(serviceContent);

var host = builder.Build();
host.OnError = delegate(exception, context) { LogError(exception, context); }

// Add modules
host.AddModule(new SceneModule(serviceScene));
// ...

// In your game loop:
_adapter.Update(timeGame);
_adapter.Draw(timeGame);
```

For a full example, see the [DungeonSlime sample](Samples/DungeonSlime).

## Project Structure

```
MonoGameLibrary/
├── Core/                 # Platform-agnostic interfaces and base classes
├── Adapters/             # MonoGame-specific implementations
│   ├── MonoGame/         # Content, Input, Audio, Render context
│   ├── Gum/              # Gum UI integration
│   └── ...
├── Extensions/           # Optional modules
│   ├── Audio/
│   ├── Input/
│   ├── Scenes/
│   ├── Graphics/         # Sprite, Atlas, Tilemap
│   └── ...
└── Samples/
    ├── DungeonSlime/     # Complete game demonstrating the framework
    └── ...
```

## Documentation

Detailed design principles are documented in [`Guidance-en_US.md`](Guidance-en_US.md) and [`Guidance-zh_CN.md`](Guidance-zh_CN.md).  
The [API Reference](docs/API.md) might be developed in the future.

## Contributing

We welcome the community to build upon this work, but please understand that we cannot provide support or accept contributions at this time. If you are interested in using or extending this codebase, you are strongly encouraged to develop on your own copy. 
We hope that this project serves as a solid foundation for your own extension-esque game development efforts. 

## License

This project is licensed under the MIT License – see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Mainly built on [MonoGame](https://www.monogame.net/) and [MonoGame.Samples](https://github.com/MonoGame/MonoGame.Samples)
- Inspired by clean architecture and dependency injection patterns
```
