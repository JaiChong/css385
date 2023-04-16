using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
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

        // Randomizes spawn location using vrtCamRange and hrzCamRange as boundaries
        Spawn();
    }

    void Update()
    {
        // Updates target
        GameObject target;
        if (gm.enemyTargetRandom >= 0)
        {
            target = GameObject.FindGameObjectWithTag(gm.wds[gm.enemyTargetRandom].tag);
        }
        else if (gm.enemyTargetHero)
        {
            target = hero;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag(gm.enemyTargetSequential);
        }

        // Updates position
        Vector3 direction = target.transform.position - transform.position; 
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 5*Time.deltaTime);
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

    private void Spawn()
    {
        // Spawned randomly within 90% of world boundaries
        transform.position = new Vector3(UnityEngine.Random.Range(-hrzCamRange, hrzCamRange), UnityEngine.Random.Range(-vrtCamRange, vrtCamRange), 10);
        rb = GetComponent<Rigidbody2D>();
        rb.position = transform.position;
        rb.rotation = UnityEngine.Random.Range(0, 360);
    }
}
