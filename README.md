# PROG_6221_POE_Part_1

Creating a chatbot to assist users. The focus is OOP and other C# principles.

Development Log & Decision Tracker  Raphael Cybersecurity Chatbot

This document tracks the full journey from POE analysis to final submission. It combines the initial planning log with the detailed implementation history, showing every major decision and change made during development.

---
Phase 1: Planning & Structure

Decision: Modular structure (not everything in Program.cs)

Reason: POE marking focuses on clean code structure and separation of concerns.  
Result: `Program.cs` only starts the chatbot; all logic lives in dedicated classes.

Decision: Use a 4‑file architecture

Reason: Avoid dumping logic into one file and keep responsibilities clear.  
Final structure:**

| File | Responsibility |
|------|----------------|
| `Program.cs` | Entry point – creates bot and calls `Start()` |
| `CyberBot.cs` | Core conversation flow, name validation, keyword matching |
| `DisplayHelper.cs` | All console visuals: colours, ASCII art, typing effect, error prompts |
| `AudioHelper.cs` | WAV playback with defensive file handling |

Decision: Use automatic properties

Reason: Explicit POE requirement.  
Implemented in:`AudioHelper.GreetingPath`, `CyberBot.BotName`, `CyberBot.UserName`, `DisplayHelper.TypingDelay`.

---

Phase 2: Skeleton Commit (Commit 1)

- Created all four files with method stubs and pseudocode comments inside.
- No functional code yet – pure planning structure.

**Commit message:** `Initial commit: project skeleton with pseudocode placeholders`

---

Phase 3: Audio System (`AudioHelper.cs`)

Decision: Use `System.Media.SoundPlayer`

Reason: Simple built‑in WAV playback suitable for console apps. Matches POE requirement for voice greeting on launch.

Problem: `"greeting.wav"` path unreliable

Relative paths depend on the working directory, causing file‑not‑found errors.

Fix: Use `AppContext.BaseDirectory`

```csharp
GreetingPath = Path.Combine(AppContext.BaseDirectory, "greeting.wav");
```

Decision: Defensive file check

Reason: Program must not crash if WAV is missing.  
Implementation: `File.Exists()` check before attempting playback.

Decision: Use `PlaySync()`

Reason: Ensures audio finishes before the UI begins. Acceptable for a startup greeting.

Decision: Narrow exception handling

Reason: Avoid catching `Exception` blindly.  
Final catch: `InvalidOperationException` (specific to playback failures).

Decision: Use automatic property for rubric compliance

```csharp
public string GreetingPath { get; set; }
```

Decision: Add `[SupportedOSPlatform("windows")]` attribute

Reason: Silences CA1416 warnings and documents Windows‑only dependency.

Commit message: `feat: implement AudioHelper with defensive file handling and automatic property`

---

Phase 4: Console UI System (`DisplayHelper.cs`)

Decision: All UI output must be in `DisplayHelper`

Reason: Separation of concerns. Keeps `CyberBot` logic clean and focused.

Decision: Add ASCII art header

Reason: Required by POE brief. Displays "RAPHAEL" logo in green.

Decision: Add divider formatting

Reason: Improves readability during conversation.

Decision: Implement typing effect

Reason: POE requires typing simulation.

Decision: Typing delay configurable using automatic property

```csharp
public int TypingDelay { get; set; } = 30;
```

Decision: Consistent cyber‑themed colours

| Colour | Usage |
|--------|-------|
| Green | Header / system messages |
| Cyan | Bot responses / welcome |
| Yellow | User prompts |
| Red | Invalid input / errors |
| DarkGray | Dividers |

Decision: Add `ShowNameError()` method

Reason: Moves name‑validation error UI out of `CyberBot`, maintaining separation of concerns.

Commit message: `feat: add DisplayHelper with ASCII logo, typing effect, and colour formatting`

---

Phase 5: Chatbot Core Logic (`CyberBot.cs`)

Decision: `CyberBot` controls full conversation flow

Locked sequence:
1. Play greeting audio
2. Show ASCII header
3. Show welcome message
4. Ask for user name (with validation loop)
5. Run conversation loop
6. Show goodbye message

Decision: Validate user name using a loop

Reason: Stronger validation than silently defaulting to "User". Demonstrates control flow.

Decision: Exit keywords

Bot exits if user types: `exit`, `quit`, or `goodbye`.

Decision: Keyword matching with `.Contains()`

Reason: Simple and appropriate for Part 1. Covers required topics: password, phishing, safe browsing, malware.

Decision: Defensive input cleaning inside `GetResponse()`

```csharp
string cleanInput = input?.ToLower().Trim() ?? string.Empty;
```

Reason: Makes method self‑contained and safe even if called incorrectly.

Decision: South African cybersecurity context included

Examples:
- Phishing warnings mention SARS and South African banks.
- Purpose statement references the South African Cybersecurity Awareness Campaign.

Decision: Use `readonly` helper fields

```csharp
private readonly DisplayHelper _display;
private readonly AudioHelper _audio;
```

Reason: Prevents accidental reassignment and shows disciplined coding.

Problem: Inline `Console.ForegroundColor` inside `AskForUserName()`

Issue: Violated separation of concerns.

Fix: Call `_display.ShowNameError()` instead

Commit message: `feat: implement conversation loop, name validation, and SA-context responses`

---

Phase 6: `DisplayHelper` Refinement (Separation Fix)

- Added `ShowNameError()` method in `DisplayHelper`.
- Updated `CyberBot.AskForUserName()` to use it.

**Commit message:** `refactor: move name error UI to DisplayHelper for cleaner separation`

---

Phase 7: Program Entry (`Program.cs`)

Decision: `Program.cs` remains minimal

Reason: POE expects modular design. The entry point should only start the bot.

Final implementation:**
```csharp
CyberBot bot = new CyberBot();
bot.Start();
```

Commit message: `feat: wire up Program.cs entry point`

---

Phase 8: Continuous Integration (GitHub Actions)

Decision: Use GitHub Actions for automated build checks

Reason: Required by POE for CI marks.

 Problem: Initial `dotnet-desktop` workflow failed


---

Phase 9: Audio Troubleshooting

Runtime Trap: WAV file must copy into output folder

Symptom: App ran but no sound; sometimes `InvalidOperationException` thrown silently.

**Root cause:** WAV file was compressed (not PCM), or file not copied to `bin\Debug\`.

Fix
- Re‑encoded `greeting.wav` as 16‑bit PCM using Audacity.
- In Visual Studio, set **Copy to Output Directory = Copy if newer**.

**Outcome:** Audio plays correctly on Windows; app continues gracefully if file missing or format invalid.

---

Phase 10: Final Polish & Nullable Fixes

- Initialised all automatic properties with `= string.Empty`.
- Used `string?` for `Console.ReadLine()` assignments to avoid CS8600 warnings.
- Added missing `using System.Runtime.Versioning;` in `AudioHelper.cs`.
- Verified build produces **zero warnings** (CA1416 suppressed intentionally).

---

Final File Manifest

| File | Purpose |
|------|---------|
| `Program.cs` | Entry point |
| `CyberBot.cs` | Conversation logic and keyword responses |
| `DisplayHelper.cs` | All console UI (colours, ASCII, typing, errors) |
| `AudioHelper.cs` | WAV playback with error handling |
| `greeting.wav` | PCM voice greeting |
| `.github/workflows/dotnet.yml` | CI build verification |
| `README.md` | Project documentation |

---

Submission Checklist (Part 1)

- [x] Voice greeting (WAV)
- [x] ASCII art logo
- [x] Name input with validation loop
- [x] Predefined cybersecurity responses (SA context)
- [x] Input validation for empty queries
- [x] Enhanced console UI (colours, dividers, typing effect)
- [x] Code structured across 4 classes
- [x] Automatic properties used
- [x] 6+ meaningful GitHub commits
- [x] GitHub Actions CI passing (green checkmark)
- [x] README with CI screenshot
- [x] Unlisted YouTube presentation video

---

Submission Links 

GIT
https://github.com/10480624/PROG_6221_POE_Part_1.git

Youtube
https://youtu.be/IPQkA_z-obw

End of Development Log
```



