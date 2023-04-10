using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gm;
    private GameObject hero;
    private BoxCollider2D bc;
    private Rigidbody2D rb;

    private struct EnemyData
    {
        public float rotateTimer;
        public float rotateDirection;
        public bool following;
    }
    EnemyData ed = new EnemyData();

    void Start()
    {
        //Obtain game manager and game object
        gm = GameObject.Find("Manager").GetComponent<GameManager>();
        hero = GameObject.FindGameObjectWithTag("Hero");

        // Obtain components and set as trigger
        bc = GetComponent<BoxCollider2D>();
        bc.isTrigger = true;
        
        // Spawned randomly within 90% of world boundaries
        Camera cam = Camera.main;
        float vrtRange = 0.45f * 2f * cam.orthographicSize;
        float hrzRange = vrtRange * cam.aspect;
        transform.position = new Vector3(Random.Range(-hrzRange, hrzRange), Random.Range(-vrtRange, vrtRange), 10);
        rb = GetComponent<Rigidbody2D>();
        rb.position = transform.position;
        rb.rotation = Random.Range(0, 360);

        // Initialize struct value
        ed.rotateTimer = 1f;
        ed.rotateDirection = 0f;
        ed.following = false;
    }

    void Update()
    {
        Vector3 direction = hero.transform.position - transform.position;
     
        if (Input.GetKeyDown("k"))
        {
            ed.rotateTimer = 1f;
            ed.rotateDirection = Random.Range(0, 2) * 2 - 1;
            ed.following = !ed.following;
            //gm.gmd.enemiesFollowing = !gm.gmd.enemiesFollowing;
        }

        if (ed.rotateTimer > 0)
        {
            transform.Rotate(0f, 0f, ed.rotateDirection * 90f * Time.deltaTime);
            ed.rotateTimer -= Time.deltaTime;
        }
        else if (ed.following)
        //else if (gm.gmd.enemiesFollowing)
        {
            Debug.Log("hello");
            rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.position = Vector3.MoveTowards(transform.position, hero.transform.position, 5*Time.deltaTime);
            rb.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Hero")
        {
            // Destroyed immediately
            gm.gmd.enemiesDestroyed++;
            gm.gmd.collisionsWithEnemies++;
            Destroy(gameObject);
        }
        else if (col.tag == "Egg")
        {
            // Loses 80% of current energy (alpha-channel) per hit
            Renderer r = GetComponent<Renderer>();
            Color c = r.material.color;
            c.a *= 0.8f;
            r.material.color = c;
            
            // Destroyed by 4th egg collision
            if (r.material.color.a <= Mathf.Pow(0.8f, 4))
            {
                gm.gmd.enemiesDestroyed++;
                Destroy(gameObject);
            }
        }
    }
}
