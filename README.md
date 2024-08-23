# Garden Survivors
A 2D video game made in MonoGame for the course KI/OONV. It is being made to explore and understand C#, Oject-Oriented Programming, Design Patterns and SOLID principles.

## MonoGame
MonoGame is a cross-platform game development framework for 2D and 3D games (previously known as Microsoft XNA). Games like Stardew Valley and Celeste were made in MonoGame.
It is a framework not an engine, meaning it doesn't have a visual editor like Unity or Godot, but only provides the building blocks (like rendering, input handling, audio, etc.) and leaves much of the architecture, game logic, and higher-level systems up to the developer.

## The game
It is a top down shooter in the style of games like Vampire Survivors. It features a movable player characters, that can shoot enemies. There are 3 types of enemies with varying health, attack and speed. On their death they drop Health collectables,
that replenish your health and Experience orbs, which fill the Experince bar at the top. Upon filling the Exp bar the level increases and enemy spawn rate is increased. The game ends when the player dies.

## Concepts used
- OOP
  - Inheritence, Abstract classes, Interfaces, Generics, Events
- Design Patters
  - Builder - for building a complex object step by step
  - Observer - to loosely couple objects that need to be notified of a change in another object
  - Flyweight - reduce memory usage by having many objects share an immutable state
  - Dependency Injection - inject functionality to objects instead of creating them inside it to facilitate loose coupling, testability, flexibility

![survivors](https://github.com/user-attachments/assets/850add87-bd42-4efe-95a7-63b323d208c1)

Credits:
- Sprites from [craftpix.net](https://craftpix.net/freebies/free-low-level-monsters-pixel-icons-32x32/)
- Other assets by me
