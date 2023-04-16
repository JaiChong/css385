using UnityEngine;

public class Dash_Movement : MonoBehaviour
{
    // struct that stores data across frames
    private struct BufferData
    {
        public int bufferFrames;    // number of bufferFrames frames for reading directional inputs, enabling diagonal dashes
        public int dashFrames;      // number of frames spent dashing, giving a fixed length and time to dashes
        public bool up;             // stores whether the up    input was pressed
        public bool down;           // stores whether the down  input was pressed
        public bool right;          // stores whether the right input was pressed
        public bool left;           // stores whether the left  input was pressed
    }

    public int buffer = 3;  // the buffer constant, for access while BufferData.bufferFrames is decremented
    BufferData bd = new BufferData();

    void Start()
    {
        // initializes bd values
        bd.bufferFrames = buffer;
        bd.dashFrames   = 10;
        bd.up    = false;
        bd.down  = false;
        bd.right = false;
        bd.left  = false;
    }

    // "public float speed = 15f;" was originally here in Movement.cs, and used later to move the object via "pos.x += speed * Time.deltaTime" and the like.
    // Movement.cs needed Time.deltaTime to account for real-time updates on stops and goes, but Dash_Movement only needs to check once at the start during the
    // buffer window.  A set distance avoids the issue of differing FPS changing the dash length via real-time updates.
    public float distance = 0.25f;    // = speed * Time.deltaTime = 15f * (1/60)f = 30f * (1/30)f

    void Update()
    {
        Vector3 pos = transform.position;

        // currently reading or waiting to read inputs
        if (bd.bufferFrames > 0)
        {
            // decrements bd.bufferFrames for each frame any directional input is read
            if (bd.bufferFrames < buffer || ( Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("a") ))
            {
                bd.bufferFrames--;
            }

            // "w" can be replaced with any key
            // records that the up input was pressed
            if (Input.GetKey("w"))
            {
                bd.up = true;
            }

            // "s" can be replaced with any key
            // records that the down input was pressed
            if (Input.GetKey("s"))
            {
                bd.down = true;
            }

            // "d" can be replaced with any key
            // records that the right input was pressed
            if (Input.GetKey("d"))
            {
                bd.right = true;
            }

            // "a" can be replaced with any key
            // records that the left input was pressed
            if (Input.GetKey("a"))
            {
                bd.left = true;
            }
        }
        
        // currently dashing
        else // (bd.bufferFrames == 0)
        {
            if (bd.dashFrames > 0)
            {
                // moves the object up
                if (bd.up == true)
                {
                    pos.y += distance;
                }

                // moves the object down
                if (bd.down == true)
                {
                    pos.y -= distance;
                }

                // moves the object right
                if (bd.right == true)
                {
                    pos.x += distance;
                }

                // moves the object left
                if (bd.left == true)
                {
                    pos.x -= distance;
                }

                // decrements remaining dashFrames
                bd.dashFrames--;
            }

            else // (dashFrames == 0)
            {
                // resets bd values
                bd.bufferFrames = buffer;
                bd.dashFrames   = 10;
                bd.up    = false;
                bd.down  = false;
                bd.right = false;
                bd.left  = false;
            }
        }        

        transform.position = pos;
    }
}
