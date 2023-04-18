using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gm;
    private GameObject hero;
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private Renderer r;

    private Color color;
    private float vrtCamRange;
    private float hrzCamRange;

    private int targetSequential;
    private int targetRandom;
    private bool targetHero;

    void Start()
    {
        //Obtain game manager and game object, and set first target
        gm = GameObject.Find("Manager").GetComponent<GameManager>();
        hero = GameObject.FindGameObjectWithTag("Hero");

        // Obtain components, set as trigger, and initialize stored color
        bc = GetComponent<BoxCollider2D>();
        bc.isTrigger = true;
        r = GetComponent<Renderer>();
        color = r.material.color;

        // Calculates and stores the inner 90% of the camera's boundaries
        Camera cam = Camera.main;
        vrtCamRange = 0.45f * 2f * cam.orthographicSize;
        hrzCamRange = vrtCamRange * cam.aspect;

        // Initializes target vlaues
        targetSequential = 0;
        targetRandom = -1;
        targetHero = false;

        // Randomizes spawn location using vrtCamRange and hrzCamRange as boundaries
        Spawn();
    }

    void Update()
    {
        // Check for target change input
        if (Input.GetKeyDown("j"))
        {
            if (targetRandom == -1)
            {
                targetRandom = 6;
                NextTargetWaypoint(-1);
            }
            else
            {
                targetRandom = -1;
            }
            targetHero = false;
        }
        else if (Input.GetKeyDown("k"))
        {
            targetHero = !targetHero;
            targetRandom = -1;
        }
        
        // Updates target based on input
        GameObject target;
        if (targetRandom >= 0)
        {
            target = GameObject.FindGameObjectWithTag(gm.wds[targetRandom].tag);
            if (transform.position == target.transform.position)
            {
                NextTargetWaypoint(-1);
                target = GameObject.FindGameObjectWithTag(gm.wds[targetRandom].tag);
            }
        }
        else if (targetHero)
        {
            target = hero;
        }
        else // targetting sequentially
        {
            target = GameObject.FindGameObjectWithTag(gm.wds[targetSequential].tag);
            if (transform.position == target.transform.position)
            {
                NextTargetWaypoint(targetSequential);
                target = GameObject.FindGameObjectWithTag(gm.wds[targetSequential].tag);
            }
        }

        // Updates position
        Vector3 direction = target.transform.position - transform.position; 
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 20*Time.deltaTime);
        rb.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Hero")
        {
            // Destroyed immediately by hero
            gm.enemiesDestroyed++;
            gm.collisionsWithEnemies++;
            Spawn();
        }
        else if (col.tag == "Egg")
        {
            // Loses 25% of total opacity (alpha-channel) per egg hit
            color.a -= 0.25f;
            r.material.color = color;
            
            // Destroyed by 4th egg collision
            if (color.a <= 0)
            {
                gm.enemiesDestroyed++;
                Spawn();
                
                // Replenish opacity to 100%
                color.a = 1;
                r.material.color = color;
            }
        }
    }

    // Passed -1 if obtaining a new targetRandom, or 0-5 if obtaining a new targetSequential
    void NextTargetWaypoint(int i)
    {
        // if targetting randomly, updates to next unique random
        if (targetRandom >= 0)
        {
            int res;
            do
            {
                res = Random.Range(0,5);
            } while (res == targetRandom || res == targetSequential);
            targetRandom = res;
        }

        // if targetting sequentially, updates to next in sequence
        else if (!targetHero)
        {
            targetSequential = (targetSequential + 1) % 6;
        }
    }
    
    void Spawn()
    {
        // Spawned randomly within 90% of world boundaries
        transform.position = new Vector3(UnityEngine.Random.Range(-hrzCamRange, hrzCamRange), UnityEngine.Random.Range(-vrtCamRange, vrtCamRange), 10);
        rb = GetComponent<Rigidbody2D>();
        rb.position = transform.position;
        rb.rotation = UnityEngine.Random.Range(0, 360);
    }
}
