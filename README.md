# KarateJudge

Minimal desktop scoreboard and referee control app for traditional karate (WUKF/JKA).  
Built with C# and Avalonia UI.

## Download

[⬇️ Download latest release](https://github.com/Bonislavsky/KarateJudge/releases/latest)

## Features

**Rule Presets**

Available presets (select to auto-configure all match parameters):
- Shobu Ippon — 1 point match
- Shobu Nihon — 2 point match  
- Shobu Sanbon — 3 point match
- Kata — form demonstration mode

*After selecting a preset, individual parameters can be adjusted for a specific match.*

**Match Settings**
- Set a custom point limit or enable Unlimited mode (no point cap)
- Configure match duration or disable the time limit entirely
- Set a default timer reset value between rounds
- Option to auto-clear fighter names between matches
- Upload a tournament or club logo (PNG/JPG, 350×350 px)
- Customize AKA / AO side colors

**Referee Controls**
- Add/remove points
- Foul and penalty logging
- Match clock management
- Hotkey support for Start/Pause (Space)
- Audio alert 15 seconds before match end
- Loud end-of-match signal when time expires

## Screenshots

**Spectator Screen**
![Spectator Screen](https://github.com/user-attachments/assets/412968e0-cae3-48d5-8838-4cdc1588372b)

**Referee Screen**
![Referee Screen](https://github.com/user-attachments/assets/7357c2fc-7365-4b88-b866-cb4ba7b6c0d9)

**Settings Screen**
![Settings Screen](https://github.com/user-attachments/assets/10cb7904-ef7c-4a3f-856d-14f2ed9945c5)


## Tech Stack

- C# / .NET 8
- Avalonia UI (MVVM)

## Getting Started

**Prerequisites:** .NET SDK 8.0+

```bash
git clone https://github.com/Bonislavsky/KarateJudge.git
cd KarateJudge
dotnet run
```

## License

MIT
