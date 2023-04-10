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
    private float vrtRange;
    private float hrzRange;
    
    void Start()
    {
        // Obtain world boundaries
        Camera cam = Camera.main;
        vrtRange = 0.5f * 2f * cam.orthographicSize;
        hrzRange = vrtRange * cam.aspect;
        
        // Obtain component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Travel toward transform.up
        rb.velocity = speed * transform.up;
        
        // Expires when leaving the world bound
        Vector3 pos = transform.position;
        if (pos.x < -hrzRange || pos.x > hrzRange || pos.y < -vrtRange || pos.y > vrtRange)
        {
            Destroy(gameObject);
        }
    }

    // Expires when colliding with enemy
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}