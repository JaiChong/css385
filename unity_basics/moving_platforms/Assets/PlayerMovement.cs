using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private float moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Input();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // UnityEngine class function called every frame this Collider2D object remains in Collision2D with another Collider2D object
    void OnCollisionStay2D(Collision2D collision) {
        Friction(collision);
    }

    private void Input()
    {
        moveDirection = UnityEngine.Input.GetAxis("Horizontal");
        if (UnityEngine.Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }
        
        isJumping = false;
    }

    private void Friction(Collision2D collision)
    {        
        Vector3 pos = transform.position;
        if (collision.collider.name.Contains("Moving Platform Block"))
        {    
            // moves player along with moving plat, mimicking friction
            PlatformMovement pm = collision.gameObject.GetComponent<PlatformMovement>();
            if (pm.bd.movingLeft)
            {
                pos.x -= pm.speed * Time.deltaTime;
            }
            else // (!movingPlat.bd.movingLeft)
            {
                pos.x += pm.speed * Time.deltaTime;
            }
        }
        transform.position = pos;
    }
}
