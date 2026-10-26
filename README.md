# 2D Unity Lab 6

Open this project in **Unity 6.6.7f1** and load `Assets/Scenes/SampleScene.unity`.

## Controls

- Left and Right arrow keys: move
- Space: jump
- R: restart after game over

## Lab 6 features

- Player health and lives start at 3 each. Enemy damage grants 1.5 seconds of immunity with a 0.1-second sprite flicker. Losing all health uses a life and returns the player to the latest checkpoint; losing the last life shows Game Over.
- A walking enemy patrols between two wall triggers on the first platform.
- Crossing the trigger left of the flower spawns the supplied ghost prefab at the flower. The ghost prefab has been repaired with a sprite and working script.
- A saw enemy follows the player on the second part of the level.

The Lab 6 scripts are in `Assets/`. Enemy prefabs are in `Assets/Prefabs/`, and their sprites are in `Assets/Lab6/Sprites/`.

The original Week 5 starter project was provided by [AbdallaFouad14](https://github.com/AbdallaFouad14/Week-5-).
