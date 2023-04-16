// TODO:
// - Test whether transform.position.y is rotated by transform.rotation, and adjust either
//    "// Travel toward transform.up" or "// Expires when leaving the world bound" accordingly
// - OnTiggerEnter2D()

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private Rigidbody2D rb;

    private float speed = 40f;  // 40 units/sec, as per assignment
    private float vrtCamRange;
    private float hrzCamRange;
    
    void Start()
    {
        // Obtain world boundaries
        Camera cam = Camera.main;
        vrtCamRange = 0.5f * 2f * cam.orthographicSize;
        hrzCamRange = vrtCamRange * cam.aspect;
        
        // Obtain component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Travel toward transform.up
        rb.velocity = speed * transform.up;
        
        // Expires when leaving the world bound
        Vector3 pos = transform.position;
        if (pos.x < -hrzCamRange || pos.x > hrzCamRange || pos.y < -vrtCamRange || pos.y > vrtCamRange)
        {
            Destroy(gameObject);
        }
    }

    // Expires when colliding with enemy
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" || col.tag.Contains("Waypoint"))
        {
            Destroy(gameObject);
        }
    }
}