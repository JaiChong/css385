# Quit Application on Held Key Press
( [Repo](https://github.com/JaiChong/css385/tree/main/unity_basics/quit_application_on_held_key_press) / [Page](https://jaichong.github.io/css385/unity_basics/quit_application_on_held_key_press) / [WebGL](https://jaichong.github.io/css385/unity_basics/quit_application_on_held_key_press/build_webgl) )

## Description
This mechanic is a variation on the [Quit Application on Key Press](https://github.com/t4guw/100-Unity-Mechanics-for-Programmers/tree/master/programs/quit_application_on_key_press) project.  Changes enable the quitting of the application using held key inputs, instead of just single key press inputs.

## Implemented Changes
1. A struct called `BufferData` stores the `float` down-counter `holdTime`, which is instantiated as `bd` and initialized in the newly implemented `Start()` to `3f` by default.
2. `Update()` now checks `if (bd.holdTime > 0)` each frame, decrementing via `bd.holdTime -= Time.deltaTime` or resetting by calling `Start()` depending on whether the input is held or released on that frame.  Upon `bd.holdTime` reaching 0, `Application.Quit()` is called.
