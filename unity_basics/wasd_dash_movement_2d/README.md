# WASD Dash Movement in 2D
( [Repo](https://github.com/JaiChong/css385/tree/main/unity_basics/wasd_dash_movement_2d) / [Page](https://jaichong.github.io/css385/unity_basics/wasd_dash_movement_2d/) / [WebGL](https://jaichong.github.io/css385/unity_basics/wasd_dash_movement_2d/build_webgl) )

## Description
This mechanic is a variation of the [WASD Movement in 2D](https://github.com/t4guw/100-Unity-Mechanics-for-Programmers/tree/master/programs/wasd_movement_2d) project.  Changes enable dash movement in 2D for a user's character using a set of input keys.

## Implemented Changes
1. A struct called `BufferData` stores the `int` down-counters `bufferFrames` and `dashFrames`, as well `bool`s that track whether `up`, `down`, `right`, and/or `left` inputs have been read within the buffer window implemented in `Update()`.
2. `public float speed` has been replaced by `public float distance`.  `speed` was only used to calculate the distance traveled on each frame in tandem with `Time.deltaTime` to enable real-time checks for stopping and going.  Since distance only checks once during a buffer window for input, and the distance traveled should be consistent, this was replaced.
3. `Start()` has been added to initialize the values of the BufferData instance `bd`.  By default, `bd.bufferFrames = 3` and `bd.dashFrames = 10`, and each `bd.<direction> = false`.
4. `Update()` now features 2 sections: a buffer window that reads for inputs by setting `bd.<direction> = true`, entered if `(bd.bufferFrames > 0)`, and a position updater, entered if `!(bd.bufferFrames > 0)` and `(bd.dashFrames > 0)`.  Both sections decrement their respective down-counters.  Once `!(bd.dashFrames > 0)`, `bd`'s values are reset to what they were from `Start()`.
