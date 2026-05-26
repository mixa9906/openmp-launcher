# Architecture Documentation

## Overview

OpenMP Launcher is built using a clean, scalable architecture with clear separation of concerns.

## Project Structure

### Core Layer (`OpenMPLauncher.Core`)
Contains business logic, models, and services independent of UI framework.

**Directories:**
- **Models/** - Data transfer objects and domain models
  - `ServerInfo.cs` - Server status information
  - `NewsArticle.cs` - News content model
  - `UserAccount.cs` - User authentication model
  - `AuthModels.cs` - Authentication DTOs

- **Services/** - Business logic services
  - `IAuthService` - User authentication and account management
  - `IServerService` - Server status and news retrieval
  - `IGameService` - Game installation and launching

- **API/** - API client implementations
  - HTTP client factories
  - API request/response handling

- **Utils/** - Utility functions
  - File operations
  - Hashing and encryption
  - Configuration helpers

### UI Layer (`OpenMPLauncher.UI`)
WPF-based user interface with MVVM pattern.

**Directories:**
- **Views/** - XAML window definitions
  - `LoginWindow.xaml` - Authentication UI
  - `MainWindow.xaml` - Main launcher interface

- **ViewModels/** - MVVM ViewModels
  - `MainViewModel.cs` - Main window logic
  - `LoginViewModel.cs` - Authentication logic
  - `SettingsViewModel.cs` - Settings management

- **Styles/** - XAML resource dictionaries
  - Color schemes
  - Button styles
  - Control templates

- **Resources/** - Static resources
  - Images
  - Icons
  - Fonts

### Updater Module (`OpenMPLauncher.Updater`)
Handles launcher updates and file integrity checks.

### CEF Integration (`OpenMPLauncher.CEF`)
Chromium Embedded Framework integration for modern web UI elements.

## Design Patterns

### MVVM (Model-View-ViewModel)
- Clean separation between UI and business logic
- INotifyPropertyChanged for data binding
- Commands for user interactions

### Dependency Injection
- Microsoft.Extensions.DependencyInjection
- Service registration in App.xaml.cs
- Constructor injection for loose coupling

### Repository Pattern
- Data access abstraction
- Easy testing and mocking
- Support for multiple data sources

### Observer Pattern
- Event-driven communication
- Real-time server status updates
- News notifications

## Data Flow

```
User Interaction (View)
         ↓
    Command/Event
         ↓
    ViewModel
         ↓
    Service Layer
         ↓
    API/Database
```

## Security Features

1. **Authentication**
   - Token-based auth (JWT)
   - Secure token storage
   - Password hashing

2. **File Integrity**
   - SHA256 checksums
   - Signature verification
   - Anti-tampering measures

3. **Communication**
   - HTTPS only
   - Certificate validation
   - Request signing

## Extension Points

1. **Custom Themes** - Modify XAML styles
2. **Additional Services** - Implement `IService` interface
3. **New Windows** - Create XAML + ViewModel pair
4. **API Integration** - Extend API client
5. **Build Configurations** - Add new build types

## Performance Considerations

- **Async Operations** - Non-blocking UI updates
- **Caching** - Downloaded assets cached locally
- **Lazy Loading** - News items loaded on demand
- **Resource Management** - Proper disposal of services
- **Memory Optimization** - Image compression, streaming

## Testing Strategy

- **Unit Tests** - Service layer testing
- **Integration Tests** - API communication
- **UI Tests** - ViewModel logic
- **Mock Data** - Offline testing support

## Scalability

The architecture supports:
- Multiple server support (future enhancement)
- Third-party mod integration
- Plugin system
- Custom authentication providers
- Distributed news sources
