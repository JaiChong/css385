# Space-Bar to Spinning Double-Jump
( [Repo](https://github.com/JaiChong/css385/tree/main/01_unity_basics/space_spinning_double_jump/) / [Page](https://jaichong.github.io/css385/01_unity_basics/space_spinning_double_jump/) / [WebGL](https://jaichong.github.io/css385/01_unity_basics/space_spinning_double_jump/build_webgl) )

## Description
This mechanic is a variation of the [Space-Bar to Double-Jump](https://github.com/t4guw/100-Unity-Mechanics-for-Programmers/tree/master/programs/space_double_jump) project, which in turn built upon the [Space-Bar to Jump](https://github.com/t4guw/100-Unity-Mechanics-for-Programmers/tree/master/programs/space_to_jump_2d) project.  The change made distinguishes the ground jump from its midair double-jump counterpart by adding a spin, a common visual added to double-jumps in videogames.

## Implemented Changes
1. Added a `rotationSpeed`, which is set to 360 by default.
2. Added a `struct BufferData` to hold a `bool isRotating`, instantiated as `bd`.  It is set to `true` upon reading a double-jump input, and is set to `false` in `Start()` and `if(IsGrounded)`.
3. Rotation is implemented using `transform.Rotate(0, 0, rotationSpeed * Time.deltaTime)`, and is enabled `if (bd.isRotating)`.
4. Since `Player.cs` doesn't implement movement, and by extension a faced direction, I added a direction change via `rotationSpeed *= -1` during each ground jump.

#### Limitations
Originally, I wanted to implement several completely different mechanics, but ran into issues understanding collisions and rigid bodies in conjunction with velocity.  I tried implementing air dashes, then dives, then fast falls, but all seemed to have much higher velocity than expected, despite attempts to reset it to 0 before and afterward, and so ended up sending the player block flying off the stage or through it with a single input.  I'll have to experiment a bit more to get comfortable implementing movement options in 2D platformer settings, which I would like to pursue making.
