using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;

    // doesn't work
    // void Start()
    // {
    //     Debug.Log("Start()");
    //     if (Input.GetKeyDown("left"))
    //     {
    //         Debug.Log("left");
    //         this.transform.position = new Vector3 (-7, 0, 0);
    //     }
    //     else if (Input.GetKeyDown("right"))
    //     {
    //         Debug.Log("right");
    //         this.transform.position = new Vector3 (7, 0, 0);
    //     }
    // }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
