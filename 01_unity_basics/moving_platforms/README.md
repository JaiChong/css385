# Moving Platforms
## Description
This mechanic is a variation of the [Platforms](https://github.com/t4guw/100-Unity-Mechanics-for-Programmers/tree/master/programs/platforms) project. The original game featured rows of mostly blocks that could be ascended through via the single more transparent platform block on each row, which did allow one-way movement and so could be jump through.  This variation instead gives focus to ascending the platform blocks as they move horizontally back and forth, without rows of blocks in the way.

## Implemented Changes
1. Removed all blocks in all rows that were not platform blocks, so the platform blocks would have room to move.
2. Added `PlatformMovement.cs` to each platform block.  It features:
    - a `bool movingLeft` in a `struct BufferData` to track whether to move left or right across frames, instantiated as `bd`,
    - a `Start()` that initializes `movingLeft` to true if the block is in an odd row and false if in an even row, and
    - an `Update()` which both changes the value of `movingLeft` upon reaching the far left and right walls, and moves the platform position left or right using a `public float speed` and `Time.deltaTime`.
3. Added the following functions to `PlayerMovement.cs`:
    - `OnCollisionStay2D()`, a UnityEngine class function that is called every frame, and which itself calls `Friction()` in a similar manner to how the file's `Update()` and `FixedUpdate()` called other functions for readability.
    - `Friction()`, which accesses the `PlatformMovement` variables `bd.movingLeft` and `speed` via `Collision2D.collider.gameObjectGetComponent<PlatformMovement>` and uses them to mimick having friction on the surface of the platform by moving in parallel to its position.

#### Limitations
- `Friction()`
    - It is merely a mimicry of true friction.  I have a feeling it could have been implemented more simply through BoxCollider2D or RigidBody2D settings, but wasn't sure how to.
    - It seems that the parallel movement of the player to mimic friction lags a bit behind that of the platform, and so the player ends up "sliding" a short distance with each change of direction.
- `isJumping`
    - Infinite jumps weren't an issue for the original game, since the player was blocked vertically by the next row until you reached the eventual goal of the top row.  In this variation, without rows to block the way, infinite jumps bypasses the need to jump from platform block to platform block at all.  I tried disabling it by requiring `!isJumping` to the if function that enables it in `Input()` (called by `Update()`), and moving `isJumping = false` from just after the jump repositioning in `Move()` (called by `FixedUpdate()`) to after making contact with a surface in `Friction()` (called by `OnCollision2D()`), but somehow infinite jumps could still be performed, so I reverted the changes.
