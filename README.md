# Keepers of The Elements - 2D Platformer Game

[![Unity](https://img.shields.io/badge/Unity-2022.3.20f1+-000000?style=flat&logo=unity&logoColor=white)](https://unity.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-Educational-blue.svg)](LICENSE)

**Keepers of The Elements** is a 2D platformer game developed with the Unity engine as a graduation project for **Marmara University's Computer Programming program**. It offers an engaging action-adventure experience where players battle various enemies, enhance their character's abilities, and explore meticulously designed levels.

## 📖 About the Project

This project is a 2D action-adventure platformer created to provide players with an immersive and enjoyable gameplay experience. The core of the game revolves around combat, character progression, and exploration. Players will navigate through different levels, fighting enemies to gain experience and unlock powerful elemental abilities.

The primary goal was to tackle the complexities of integrating character development and dynamic combat mechanics into a platformer, resulting in a unique game that challenges players to think strategically about their character's growth and combat choices.

**Developed as a graduation project for Marmara University's Computer Programming program.**

## 🛠️ Technology Stack

- **Game Engine:** Unity
- **Programming Language:** C#
- **IDE:** Visual Studio, Visual Studio Code
- **Version Control & Collaboration:** Google Drive
- **Art & Sound Assets:** Various artists from itch.io (see Credits section in-game or project files)

## 📋 Features

### ⚔️ Core Gameplay Features

- **Dynamic Character Control:** Responsive player movement, including jumping, turning, and attacking
- **Enemy AI:** Enemies with independent movement, attack patterns, and player detection
- **Experience & Leveling System:** Gain experience points by defeating enemies to level up
- **Character Progression:** Upon leveling up, players can choose to upgrade their Health Points (HP), Attack Damage, or unlock Elemental Powers
- **Elemental Powers:** Acquire and use powers based on Fire, Water, Air, and Earth. These powers not only affect combat but also change the player's appearance

### 🌟 Elemental Abilities System

The game features a unique elemental system where each element provides different strategic advantages:

- **🔥 Fire Element:** Burns enemies, dealing damage over time and providing offensive capabilities
- **💧 Water Element:** Drains health from enemies and transfers it to the player, offering both offense and healing
- **🌪️ Air Element:** Increases the character's movement speed, providing enhanced mobility and agility
- **🗿 Earth Element:** Provides stone armor that increases defense and damage resistance

Players earn experience points by defeating enemies and can strategically choose which elemental powers to unlock and upgrade, creating different playstyles and approaches to combat.

### 💻 Technical & Design Features

- **Modular Codebase:** The project is structured with independent modules for character control, enemy AI, UI, and more, making it easy to manage and expand
- **Custom Animations:** Detailed animations for the player and enemies, including idle, walk, attack, hit, and death states, managed via Unity's Animator
- **Level Design:** Manually crafted levels using Unity's Tile Palette feature
- **User Interface (UI):** A clean and intuitive UI for the main menu, pause screen (with audio controls), and character development screen
- **Audio Design:** Immersive sound effects for attacks, damage, and other in-game actions, with balanced audio levels using Unity's Audio Mixer

## 🏗️ Project Structure

The project is organized into several key script modules that handle the game's logic.

### Main C# Scripts

```
/Scripts/
├── Player/
│   ├── KnightController.cs     // Manages player movement, animations, and state
│   ├── PlayerExperience.cs     // Handles the player's experience points and leveling up
│   └── TouchingDirections.cs   // Detects collision with ground, walls, etc.
│
├── Enemy/
│   └── EnemyController.cs      // Controls enemy AI, movement, and attack behavior
│
├── Behaviours/
│   ├── SetBoolBehaviour.cs     // Manages boolean parameters in the Animator
│   ├── SetFloatBehaviour.cs    // Manages float parameters for things like attack cooldowns
│   └── FadeRemoveBehaviour.cs  // Creates a fading effect for defeated enemies
│
├── General/
│   ├── Damagable.cs            // Manages health and damage taken for any character
│   ├── Attack.cs               // Defines attack properties like damage and knockback
│   └── HealthBar.cs            // Controls the visual representation of health
```

## 🔧 Installation

### Prerequisites

- Unity Hub
- A compatible version of the Unity Editor (2022.3.20f1 or newer recommended)

### Installation Steps

1. **Clone the repository:**
   ```bash
   git clone https://github.com/mertucan/Keepers-Of-The-Elements.git
   ```

2. **Open the project in Unity Hub:**
   - Launch Unity Hub
   - Click on "Open" or "Add project from disk"
   - Navigate to the cloned `Keepers-Of-The-Elements` directory and select it

3. **Run the game:**
   - Once the project is open in the Unity Editor, locate the Main Menu scene in the `Assets/Scenes` folder
   - Open the Main Menu scene
   - Press the Play button at the top of the editor to start the game

## 📷 Screenshots

### In-Game Screenshots

<img width="1910" height="1076" alt="Ekran görüntüsü 2025-07-28 220251" src="https://github.com/user-attachments/assets/67bd08ee-b36b-4e20-b75c-d83f99381262" />
<img width="1917" height="1066" alt="Ekran görüntüsü 2025-07-28 220420" src="https://github.com/user-attachments/assets/e5f1d14b-f43f-48b0-af39-a3e639f33f60" />
<img width="1903" height="1083" alt="Ekran görüntüsü 2025-07-28 220527" src="https://github.com/user-attachments/assets/ab191413-ea42-47db-a058-91e315afc0aa" />

### Upgrade Menu

<img width="1691" height="571" alt="Ekran görüntüsü 2025-07-28 220334" src="https://github.com/user-attachments/assets/80b16896-1cd4-4721-b1c1-bc0a01ab593b" />

### Start Menu

<img width="1910" height="1066" alt="Ekran görüntüsü 2025-07-28 220220" src="https://github.com/user-attachments/assets/435b02e4-83b3-4df8-8276-5179d8c034d3" />

### Pause Menu

<img width="853" height="607" alt="Ekran görüntüsü 2025-07-28 220233" src="https://github.com/user-attachments/assets/2fbeb5da-02fe-46bf-bf91-bb8d8ba70db1" />

## 🎨 Assets & Resources

This project utilizes various high-quality assets from talented artists and developers. Below are the resources used:

### Documentation & References
- [Unity Script Reference](https://docs.unity3d.com/ScriptReference/)
- [Unity Manual](https://docs.unity3d.com/Manual/index.html)

### Art Assets
- [Martial Hero 3](https://luizmelo.itch.io/martial-hero-3) - Character sprites and animations
- [Skeleton Pack](https://jesse-m.itch.io/skeleton-pack) - Enemy skeleton sprites
- [Monsters Creatures Fantasy](https://luizmelo.itch.io/monsters-creatures-fantasy) - Various enemy sprites
- [Starstring Fields](https://trixelized.itch.io/starstring-fields) - Environment and background assets
- [2D Soulslike Character](https://szadiart.itch.io/2d-soulslike-character) - Additional character animations
- [Free Forest Bosses](https://free-game-assets.itch.io/free-forest-bosses-pixel-art-sprite-sheet-pack) - Boss enemy sprites
- [Mud Enemy Sprite](https://lizcheong.itch.io/mud-enemy-sprite#google_vignette) - Earth-based enemy sprites
- [Main Character of the Story](https://kbpixelart.itch.io/main-character-of-the-story) - Additional character assets
- [Pixel Dimensional Portal](https://pixelnauta.itch.io/pixel-dimensional-portal-32x32) - Portal and transition effects
- [Pixel Platformer Castle](https://szadiart.itch.io/pixel-platformer-castle) - Castle and structure assets

Special thanks to all the artists who made their work available for educational and indie game development projects.

## 🚀 Future Work

Based on the initial project goals and outcomes, several potential enhancements have been identified:

- **New Content:** Adding more levels, enemies, bosses, and character abilities to extend the gameplay
- **Multiplayer Mode:** Introducing a cooperative or competitive multiplayer mode
- **Advanced Enemy AI:** Developing more complex and varied behaviors for enemies
- **Mobile Optimization:** Adapting the game for mobile platforms
- **Expanded Language Support:** Adding support for other languages beyond the current English version

## 🎓 Academic Context

This project was developed as a **graduation project for Marmara University's Computer Programming program**, demonstrating the practical application of game development concepts, object-oriented programming principles, and Unity engine capabilities.

## 📄 License

This project was developed for educational purposes as part of a university curriculum.

## 👥 Contributors

- **Mert Uçan** - [GitHub: @mertucan](https://github.com/mertucan)
- **Furkan Altın** - [GitHub: @frknaltin](https://github.com/frknaltin)

## 📞 Contact

For any questions or inquiries about the project, feel free to reach out to the developers through their GitHub profiles.
