**Dungeon Crawler – Lab 2**
A simplified Dungeon Crawler built as a C# console application. Players explore a predefined dungeon, fight enemies, and experience a turn-based roguelike inspired game using core object-oriented principles. This project was developed as Lab 2 in the programming program, with a focus on object-oriented design.

## Game Overview


**Features**
- Object-Oriented Design:
 Abstract base class LevelElement with derived LivingElement for all entities having health, movement, and combat (Player, Rat, Snake).
 Specific classes for walls and enemies with unique behaviors.

## Gameplay & Combat


- Level Loading:
 Parses dungeon layout from text file and dynamically creates game objects.


- Turn-Based Game Loop:
 Player and enemies take sequential turns with console rendering each turn.


- Fog of War:
 Player vision limited by radius; walls remain visible once discovered; enemies vanish outside vision.


- Combat System:
 Dice-based attack and defense mechanics with damage calculation, counterattacks, and death handling.


- Enemy behaviour:
 Rats move randomly; snakes move away when player is near.

## Game Over

**Technical Summary**
- OOP Concepts:
 Utilizes abstraction, inheritance, and polymorphism with abstract classes and overridden methods (Update(), ShouldAttack()).


- Encapsulation:
 Classes manage their own state and behavior (position, HP, dice rolls, movement, combat).


- Collections:
 Uses List<LevelElement> to manage game entities and LINQ for removal of dead enemies.


- Constructors:
 Initialize objects with specific attributes like health, attack/defense dice, and display color.


- Game Loop & Input:
 GameLoop class handles input, game state updates, and redraws each turn.
