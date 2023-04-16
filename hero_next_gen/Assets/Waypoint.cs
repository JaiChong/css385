using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private GameManager gm;
    private GameObject hero;
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private Renderer r;
    public Color color;
    private int wdsIndex;


    private struct Data
    {
        public bool hidden;
        public Vector3 originalPosition;
    }
    Data d = new Data();

    void Start()
    {
        wdsIndex = (byte)name[0] - 65;
        Debug.Log(wdsIndex);
        
        //Obtain game manager and game object
        gm = GameObject.Find("Manager").GetComponent<GameManager>();
        hero = GameObject.FindGameObjectWithTag("Hero");

        // Obtain components, set as trigger, and initialize stored color
        bc = GetComponent<BoxCollider2D>();
        bc.isTrigger = true;
        r = GetComponent<Renderer>();
        color = r.material.color;

        // Initializes struct data
        d.hidden = false;
        d.originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            if (!d.hidden)
            {
                r.material.color = new Color(0,0,0,0);
                d.hidden = true;
            }
            else
            {
                r.material.color = color;
                d.hidden = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!d.hidden && col.tag == "Egg")
        {
            // Loses 25% of total opacity (alpha-channel) per egg hit
            color.a -= 0.25f;
            r.material.color = color;
            
            // Destroyed by 4th egg collision
            if (color.a <= 0)
            {
                gm.wds[wdsIndex].destroyed = true;
                gm.waypointsDestroyed++;
                Spawn();

                // Replenish opacity to 100%
                color.a = 1;
                r.material.color = color;
            }
        }
    }

    private void Spawn()
    {
        // Spawned randomly within 90% of world boundaries
        transform.position = new Vector3(Random.Range(d.originalPosition.x - 15, d.originalPosition.x + 15), Random.Range(d.originalPosition.y - 15, d.originalPosition.y + 15), 10);
        rb = GetComponent<Rigidbody2D>();
        rb.position = transform.position;
        rb.rotation = Random.Range(0, 360);
    }
}
