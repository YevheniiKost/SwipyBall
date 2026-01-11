# SwipyBall

Educational hypercasual Unity project.

## Project structure
Root content lives in `Assets\YevheniiKostenko\SwipyBall`:

- `Animations\` \- animation sets and animation steps assets.
- `Audio\` \- sound effects (e.g., jump).
- `Materials\` \- physics/material assets and VFX materials.
- `Prefabs\` \- gameplay and UI prefabs:
    - `Prefabs\Player.prefab` \- player prefab.
    - `Prefabs\Level_Base.prefab` \- base level prefab.
    - `Prefabs\Decor\`, `Prefabs\Enrvironment\`, `Prefabs\Interactables\`, `Prefabs\UI\`, `Prefabs\VFX\` \- categorized prefab groups.
- `Resources\` \- resource\-loaded content:
    - `Resources\Configs\` \- configuration assets.
    - `Resources\Levels\` \- level data/assets.
    - `Resources\UI\` \- UI resources.
- `Scenes\` \- Unity scenes:
    - `Scenes\GameScene.unity` \- main scene.
- `Scripts\` \- source code (C\#), organized by layers:
    - `Scripts\Application\`, `Scripts\Core\`, `Scripts\Data\`, `Scripts\Domain\`, `Scripts\Presentation\`.

## How to run
1. Open the project in Unity.
2. Open `Assets\YevheniiKostenko\SwipyBall\Scenes\GameScene.unity`.
3. Press Play.

## Requirements
- Unity: \[fill in version\]
- Platforms: \[Windows/Android/iOS\]
- Render pipeline: \[Built\-in/URP\]
- Input: \[Legacy/New Input System\]

## Gameplay
- Goal: \[describe objective\]
- Controls: \[swipe/drag/tap\]
- Fail conditions: \[describe\]

## Troubleshooting
- If UI/levels do not load, verify assets exist under `Resources\...` and paths match code.
- If nothing runs, ensure `GameScene.unity` is opened and required bootstrap objects exist in the scene.


## Notes
- Assets and code are organized under `Assets\YevheniiKostenko\SwipyBall` to keep project content isolated.
- `Resources\` content is intended to be loaded at runtime via Unity `Resources` API.

