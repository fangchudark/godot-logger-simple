# GodotLoggerSimple

This project demonstrates how to use the Logger system in Godot 4.5 to capture runtime errors in release builds.

## Overview

GodotLoggerSimple is a sample project that showcases custom logger implementation in both C# and GDScript for Godot 4.5. It illustrates how to effectively capture, log, and handle runtime errors that occur in production environments.

## Features

- Custom logger implementation for C# (.NET) projects
- Custom logger implementation for GDScript projects
- Runtime error capturing in release builds
- File-based error logging
- Signal-based error notification system

## Implementation Files

### C# Implementation
- [GameLogger.cs](src/cs/GameLogger.cs) - Custom logger class for C# projects
  - Overrides [_LogError](\src\cs\GameLogger.cs#L14-L50) method to capture detailed error information
  - Overrides [_LogMessage](src\cs\GameLogger.cs#L52-L58) method for general message logging
  - Emits signals when errors or messages are caught
  - Writes error logs to `user://error_log.txt`

### GDScript Implementation
- [game_logger.gd](src/gds/game_logger.gd) - Custom logger script for GDScript projects

## How It Works

The custom [GameLogger](src\cs\GameLogger.cs#L5-L59) class extends Godot's built-in `Logger` class and overrides two key methods:

1. [_LogError](src\cs\GameLogger.cs#L14-L50) - Captures detailed error information including:
   - Function name where error occurred
   - File name and line number
   - Error code and rationale
   - Script backtraces
   - Error type classification

2. [_LogMessage](src\cs\GameLogger.cs#L52-L58) - Handles general log messages with error flag

**Important Notes:**
- C# exceptions are only captured in release builds, not in the editor environment
- [_LogError](src\cs\GameLogger.cs#L14-L50)/[_log_error](src\gds\game_logger.gd#L20-L44) captures all types of Godot errors including script errors, API errors, and manual `push_error`/`push_warning` calls
- [_LogMessage](src\cs\GameLogger.cs#L52-L58)/[_log_message](src\gds\game_logger.gd#L47-L52) captures all console output including `print`, `printerr`, and other standard output messages
- Both [_LogError](src\cs\GameLogger.cs#L14-L50)/[_log_error](src\gds\game_logger.gd#L20-L44) and [_LogMessage](src\cs\GameLogger.cs#L52-L58)/[_log_message](src\gds\game_logger.gd#L47-L52) may be called from multiple threads, so you may need to implement your own thread synchronization mechanisms when overriding these methods


All captured information is saved to a log file and emitted through signals for real-time monitoring.

## Requirements

- Godot 4.5 or later
- .NET 6.0+ for C# version
- GDScript support for GDScript version