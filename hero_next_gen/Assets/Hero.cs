// TODO:
// - PointAtPosition()
// - OnTrigger...2D()

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private GameObject egg;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Vector3 mousePosition;

    private float keyboardAcceleration = 20f;  // personal value; not required by assignment
    private float rotateSpeed = 45f;           // 45 degrees/sec, as per assignment
    private float eggShotRate = 5f;            // 1 egg / 0.2 sec = 5 egg / 1 sec, as per assignment
    private struct Data
    {
        public bool mouseControl;
        public bool mouseLeftClickToTravel;
        public Vector3 mouseClickPosition;
        public float keyboardSpeed;
        public float rotationArc;
        public float eggShotCooldown;
        public float respawnTimer;
    }
    Data d = new Data();

    void Start()
    {
        //Obtain game manager
        gm = GameObject.Find("Manager").GetComponent<GameManager>();
        
        // Obtain component
        rb = GetComponent<Rigidbody2D>();
        
        // Initialize struct data
        d.mouseControl = true;
        d.mouseLeftClickToTravel = false;
        d.mouseClickPosition = Vector3.zero;
        d.keyboardSpeed = 20f;                 // 20 units / sec, as per assignment
        d.rotationArc = 0f;
        d.eggShotCooldown = 1f;
        d.respawnTimer = 0f;
    }
    
    void Update()
    {
        if (d.respawnTimer > 0)
        {
            d.respawnTimer -= Time.deltaTime;
            Debug.Log(d.respawnTimer);
        }

        else
        {
            if (d.mouseControl)
            {
                // CHANGE INPUT TYPES
                // "m" input toggles keyboard control
                if (Input.GetKeyDown("m"))
                {
                    d.mouseControl = false;
                }
                // Mouse button 2 (right-click) toggles mouse click to follow
                if (Input.GetMouseButton(1))
                {
                    d.mouseLeftClickToTravel = !d.mouseLeftClickToTravel;
                }

                // MOVEMENT
                // Hero follows mouse movement with very slight delay
                if (!d.mouseLeftClickToTravel)
                {
                    gm.movementControl = "Mouse (Follow)";
                    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = Vector3.Lerp(transform.position, new Vector3(mousePosition.x, mousePosition.y, 10), 0.5f);
                }
                // Hero follows mouse button 1 (left-click) positions
                else
                {
                    gm.movementControl = "Mouse (Left-Click-to-Travel)";

                    // Mouse button 1 (left-click) updates bd.mouseClickPosition
                    if (Input.GetMouseButton(0))
                    {
                        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }
                    
                    // if there is a target coordinate
                    if (mousePosition != Vector3.zero)
                    {
                        // if we haven't reached that target coordinate
                        if (transform.position != mousePosition)
                        {
                            // Interpolate toward it
                            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePosition.x, mousePosition.y, 10), Time.deltaTime);
                        }
                        else
                        {
                            // else reset the saved target coordinate
                            d.mouseClickPosition = Vector3.zero;
                        }
                    }
                }
            }

            else // (keyboard control)
            {
                gm.movementControl = "Keyboard";
                
                
                // CHANGE INPUT TYPE
                // "m" input toggles Mouse control
                if (Input.GetKeyDown("m"))
                {
                    d.mouseControl = true;
                    d.keyboardSpeed = 20f;
                }

                // MOVEMENT: SPEED
                // Vertical axis inputs increment/decrement speed up to 20 or -20 units/sec
                d.keyboardSpeed += Input.GetAxis("Vertical") * keyboardAcceleration * Time.smoothDeltaTime;
                rb.velocity = d.keyboardSpeed * transform.up;
            }

            // EGG FIRE
            // Space-bar spawns an egg at 0.2 sec/egg
            if (Input.GetKey("space"))
            {
                d.eggShotCooldown -= eggShotRate * Time.deltaTime;
                if (d.eggShotCooldown <= 0)
                {
                    Instantiate(egg, transform.position, transform.rotation);
                    gm.eggsInWorld++;
                    d.eggShotCooldown = 1f;
                }
            }
        }

        // MOVEMENT: ROTATION
        // Horizontal axis inputs rotate angle at up to 45-degrees per second
        transform.Rotate(0f, 0f, -Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);
        //transform.up = Vector3.LerpUnclamped(transform.up, transform.position, 1);

    }

    // COLLISIONS:
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            // Loses 25% of current opacity (alpha-channel) per enemy collision
            Renderer r = GetComponent<Renderer>();
            Color c = r.material.color;
            c.a -= 0.25f;
            r.material.color = c;
            
            // Destroyed by 4th enemy collision
            if (r.material.color.a <= 0)
            {
                gm.heroDeaths++;
                transform.position = Vector3.zero;
                c.a = 1;
                r.material.color = c;
                d.respawnTimer = 1f;
            }
        }
    }
}
