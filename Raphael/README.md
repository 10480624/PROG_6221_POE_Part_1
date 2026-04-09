# Raphael — Cybersecurity Awareness Chatbot

A command-line chatbot built in C# for the **PROG 6221 Portfolio of Evidence (Part 1)** .  
Raphael educates South African citizens on cybersecurity topics including phishing, password safety, malware, and safe browsing.

## 📋 Features

- 🎤 **Voice Greeting** – Plays `greeting.wav` on startup using `System.Media.SoundPlayer`.  
  Uses `AppContext.BaseDirectory` to locate the file reliably.
- 🎨 **ASCII Art Header** – Displays a "RAPHAEL" logo in green with a decorative divider.
- 💬 **Typing Effect** – Configurable `TypingDelay` property controls character output speed (30ms default).
- 👤 **User Personalisation** – Asks for name with a validation loop; refuses empty input.
- 🔐 **Cybersecurity Responses** – Keyword‑matched responses covering:
  - Password safety
  - Phishing (with SA‑specific mentions of banks and SARS)
  - Safe browsing (HTTPS, public Wi‑Fi warnings)
  - Malware and virus protection
  - Purpose and topic help
- ✅ **Input Validation** – Handles empty input gracefully via `DisplayHelper.ShowInvalidInputMessage()`.
- 🧠 **Separation of Concerns** – Logic (`CyberBot`), UI (`DisplayHelper`), and audio (`AudioHelper`) are fully decoupled.
- 🔁 **Continuous Integration** – GitHub Actions workflow verifies the project builds successfully on every push.

## 📁 Project Structure
├── Program.cs # Entry point – creates CyberBot and calls Start()
├── CyberBot.cs # Conversation loop, name validation, keyword matching
├── DisplayHelper.cs # Console colours, ASCII art, typing effect, dividers, error prompts
├── AudioHelper.cs # WAV playback with defensive file checking and automatic property
├── greeting.wav # Voice greeting (PCM WAV format)
└── .github/
└── workflows/
└── dotnet.yml # CI build verification