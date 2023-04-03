# Reset Scene with Alternate Positions
( [Repo](https://github.com/JaiChong/css385/tree/main/01_unity_basics/reset_scene_with_alt_positions) / [Page](https://jaichong.github.io/css385/01_unity_basics/reset_scene_with_alt_positions/) / [WebGL](https://jaichong.github.io/css385/01_unity_basics/reset_scene_with_alt_positions/build_webgl) )

## Description
This mechanic is a variation of the [Reset Level/Scene](https://github.com/t4guw/100-Unity-Mechanics-for-Programmers/tree/master/programs/reset_scene) project.  The change enables alternate positions while resetting a scene/level on keypress or falling out of a screen, in tandem with a held directional arrow button.

## Implemented Changes
1. Added two additional scenes in which the starting position is set 7 units to the left or right, triggered by holding left or right while pressing R or respawning.  This, of course, required implementation in both Reset scripts.

#### Alternate (Failed) Implementation
Since using additional scenes felt crude, I initially wanted to implement the alternate reset positions into a script (namely, Movement.cs) linked to the Player object.  I attempted this by implementing the following `Start()` function, but it failed since Unity doesn't seem to check for inputs held before loading the scene.

```
// doesn't work
void Start()
{
    Debug.Log("Start()");
    if (Input.GetKey("left"))
    {
        Debug.Log("left");
        this.transform.position = new Vector3 (-7, 0, 0);
    }
    else if (Input.GetKey("right"))
    {
        Debug.Log("right");
        this.transform.position = new Vector3 (7, 0, 0);
    }
}
```
