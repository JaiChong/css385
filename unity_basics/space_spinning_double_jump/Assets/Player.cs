using UnityEngine;

public class Player : MonoBehaviour
{
    public bool grounded = true;
    private Rigidbody2D rb2d;
    public float jumpPower;
    public int rotationSpeed;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask platformsLayerMask;

    private int jumpCount;
    public int jumpCountMax;

    private struct BufferData
    {
        public bool isRotating;
    }
    
    BufferData bd = new BufferData();

    void Start()
    {
        rb2d = rb2d = GetComponent<Rigidbody2D> ();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        jumpCountMax = 2;
        rotationSpeed = 360;
        bd.isRotating = false;
    }

    void Update()
    {
        if (IsGrounded()) {
            jumpCount = 1;
            bd.isRotating = false;
        }

        if(Input.GetKey(KeyCode.Space)) {
            if (IsGrounded()) {
                rb2d.velocity = Vector2.up * jumpPower;
                rotationSpeed *= -1;
            } else {
                if(Input.GetKeyDown(KeyCode.Space)) {
                    if (jumpCount < jumpCountMax) {
                        rb2d.velocity = Vector2.up * jumpPower;
                        bd.isRotating = true;
                        jumpCount++;
                    }
                }
            }
        }

        if(bd.isRotating) {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }
}
