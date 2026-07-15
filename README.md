# ECHO

> **Fight Beside Your Past**

A survivor-like action roguelike built in **Unreal Engine 5** where every 20 seconds the game records your actions and spawns an **Echo** that replays them. Instead of collecting companions, you build an army from your own previous decisions.

---

##  Game Overview

ECHO is a fast-paced survivor-like game focused on one unique mechanic:

> **Your previous self becomes your ally.**

Every 20 seconds the player's movement and attacks are recorded. When the recording ends, an AI-controlled Echo appears and repeats exactly what the player did during that period.

As the run continues, more Echoes join the fight, transforming your own gameplay into an evolving combat strategy.

---

##  Core Features

- Auto-attacking combat
- Endless enemy waves
- Dynamic difficulty progression
- Experience and leveling system
- Random upgrade selection
- Echo recording & playback system
- Boss encounters
- Lightweight UI
- Modular Blueprint architecture

---

#  Gameplay Loop

```text
Start Run
      │
      ▼
Move Around Arena
      │
      ▼
Auto Attack Enemies
      │
      ▼
Collect XP
      │
      ▼
Level Up
      │
      ▼
Choose Upgrade
      │
      ▼
Echo Appears
      │
      ▼
Fight Together
      │
      ▼
Survive Bigger Waves
      │
      ▼
Boss Fight
      │
      ▼
Game Over
```

---

#  Unique Mechanic

Unlike traditional survivor-like games that rely on pets, drones, or summoned companions, ECHO transforms the player's own past actions into gameplay.

Every movement decision matters because it becomes part of the next Echo's behavior.

Good positioning creates powerful allies.

Poor positioning creates weak ones.

---

#  Project Structure

```
Content
│
├── Blueprints
│   ├── Characters
│   ├── Enemies
│   ├── Echo
│   ├── Weapons
│   ├── Managers
│   ├── Spawner
│   ├── Upgrades
│   ├── Boss
│   └── UI
│
├── Widgets
├── Data
├── Audio
├── Maps
└── Materials
```

---

#  Blueprint Architecture

```
BP_GameMode
      │
      ├─────────────┐
      │             │
      ▼             ▼
BP_Player      BP_Spawner
      │             │
      │             ▼
      │        BP_Enemy
      │             │
      ▼             ▼
BP_Projectile  BP_XPOrb
      │
      ▼
BP_EchoManager
      │
      ▼
BP_Echo
```

---

#  Controls

| Action | Input |
|---------|-------|
| Move | WASD |
| Dash | Space |
| Pause | ESC |

Combat is automatic.

---

#  Prototype Scope

Included

- Player movement
- Dash
- Auto attack
- Enemy AI
- Enemy waves
- XP system
- Level progression
- Upgrade selection
- Echo recording
- Echo playback
- Boss fight
- HUD

Not Included

- Multiplayer
- Story
- Inventory
- Crafting
- Save system
- Cosmetics
- Multiple maps

---

#  Development Roadmap

### Day 1

- Player
- Camera
- Enemy AI
- Auto Attack

### Day 2

- XP
- Leveling
- Upgrades
- UI

### Day 3

- Echo Recording
- Echo Playback
- Boss Fight

### Day 4

- Polish
- Audio
- Bug Fixes
- Packaging

---

#  Future Features

- Multiple Echo types
- Weapon evolution
- Character classes
- Permanent progression
- Daily challenges
- Multiplayer co-op
- Mobile version

---

#  AI Usage

AI was used as a development assistant to accelerate implementation by generating boilerplate Blueprint logic, debugging systems, and brainstorming design alternatives.

Core gameplay mechanics, balancing, player experience, and technical architecture were designed, evaluated, and refined through manual iteration.

---

#  Assets

Prototype uses:

- Unreal Engine Starter Content
- Free Epic Marketplace assets
- Quixel Megascans (if required)
- Placeholder UI

No final art is required for the prototype.

---

#  Target Platform

Prototype:

- Windows

Future:

- Android
- iOS
- Steam

---

#  Success Metrics

The prototype is considered successful if players:

- Understand the controls within 30 seconds
- Reach the first Echo
- Play for 8+ minutes on average
- Start a second run immediately after losing

---

#  Developer

Created as a technical game design prototype for the **Lila Games Game Designer Hiring Challenge**.

Built using:

- Unreal Engine 5
- Blueprint Visual Scripting
- GitHub

---

## 📜 License

This repository is intended for evaluation purposes as part of a hiring challenge.
