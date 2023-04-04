using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public struct BufferData
    {
        public bool movingLeft;
    }
    public BufferData bd = new BufferData();
    public float speed = 5f;

    void Start()
    {
        if (this.name.Contains("1") || this.name.Contains("3"))
        {
            bd.movingLeft = true;
        }
        else // (this.name.Contains("2") || this.name.Contains("4"))
        {
            bd.movingLeft = false;
        }
    }

    void Update()
    {
        Vector3 pos = transform.position;

        // changes direction upon reaching either wall
        if (bd.movingLeft && pos.x <= -7.5)
        {
            bd.movingLeft = false;
        }
        else if (!bd.movingLeft && pos.x >= 7.5)
        {
            bd.movingLeft = true;
        }
        
        // moves the platform left or right
        if (bd.movingLeft)
        {
            pos.x -= speed * Time.deltaTime;
        }
        else // (!bd.movingLeft)
        {
            pos.x += speed * Time.deltaTime;
        }

        transform.position = pos;
    }
}