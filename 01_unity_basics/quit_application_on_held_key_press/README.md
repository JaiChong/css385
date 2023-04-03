# [Quit Application on Key Press](https://jaichong.github.io/css385/01_unity_basics/quit_application_on_held_key_press/web_gl)

## Description
This mechanic is a variation on the github project `t4guw/100-Unity-Mechanics-for-Programmers/projects/quit_application_on_key_press/`.  Changes enable the quitting of application using held key inputs, instead of just single key press inputs.

## Implemented Changes
1. A struct called `BufferData` stores the `float` down-counter `holdTime`, which is instantiated as `bd` and initialized in the newly implemented `Start()` to `3f` by default.
2. `Update()` now checks `if (bd.holdTime > 0)` each frame, decrementing via `bd.holdTime -= Time.deltaTime` or resetting by calling `Start()` depending on whether the input is held or released on that frame.  Upon `bd.holdTime` reaching 0, `Application.Quit()` is called.
