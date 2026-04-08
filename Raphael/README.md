# Raphael — Cybersecurity Awareness Chatbot

A command-line chatbot built in C# that educates South African citizens on cybersecurity awareness topics including phishing, password safety, and safe browsing.

## Project Structure

| File | Purpose |
|---|---|
| `Program.cs` | Entry point creates CyberBot and starts the application |
| `CyberBot.cs` | Core logic conversation flow, responses, input validation |
| `DisplayHelper.cs` | All console visuals, colours, ASCII art, typing effect |
| `AudioHelper.cs` | Audio playback WAV voice greeting on startup |

## How to Run

1. Clone the repository
2. Open `GreatSage.sln` in Visual Studio
3. Ensure `greeting.wav` is in the project root
4. Press `F5` to build and run

## CI Status

<!-- Add screenshot of green GitHub Actions check here -->

## Requirements

- .NET 6.0 or later
- Visual Studio 2022
- Windows (required for `System.Media.SoundPlayer`)
