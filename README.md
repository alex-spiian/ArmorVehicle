# ArmorVehicle

## Project Description
This is a game prototype built in Unity, featuring a clean and modular codebase with solid OOP principles. The project includes:
- Player Movement: Responsive control system for smooth player navigation.
- Enemy AI: Advanced AI for enemy behavior and interaction with the player.
- Object Pooling: Efficient management of objects like enemies or projectiles using the object pool pattern to reduce memory overhead and improve performance.
- State Machine: Utilized to manage different states of enemy and player behaviors, ensuring modular and scalable logic.
- Level Switching: A system to switch between different game levels seamlessly.
- Screens Management: Handling of various game screens (like menus, HUDs, etc.) in a clean manner.
- Visual Effects: Handling of various VFX like damage flashes and knockback effects.

All systems are implemented with solid object-oriented principles, ensuring maintainable, extensible, and optimized code.

---

## Demo

[![Watch the demo](https://img.youtube.com/vi/ijGTCpfkbQI/0.jpg)](https://www.youtube.com/embed/ijGTCpfkbQI)

---
## Technology Stack

- **VContainer**: Used for dependency injection to manage object creation and lifecycles.  
- **UniTask**: Simplified asynchronous programming for better performance and cleaner code.  
- **Splines**: Utilized for creating and managing curved paths for object movement.  
- **Cinemachine**: Provides advanced camera controls and smooth tracking.  
- **DOTween**: Handles animations and effects, such as damage feedback and object movement.  
- **State Machine**: Custom implementation for managing behaviors of enemies.  

---

## How to Run the Project
1. Open the project in Unity (version `2022.3.40f1`).  
2. Load the `game`.  
3. Play the game to test the prototype.  
