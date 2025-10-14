using UnityEngine;

public class GravityPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform GroundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool gravityFlipped = false;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        // Check if on ground
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundRadius, groundLayer);

        // Move left/right
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip character sprite based on direction
        if (moveInput != 0)
            sprite.flipX = moveInput < 0;

        // Gravity flip on space press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipGravity();
        }

        // Optional jump (only if you want jumping too)
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }
    }

    void FlipGravity()
    {
        gravityFlipped = !gravityFlipped;
        rb.gravityScale *= -1;

        // Flip character visually
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
    }

    void Jump()
    {
        float jumpDirection = gravityFlipped ? -1 : 1;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * jumpDirection);
    }

    private void OnDrawGizmosSelected()
    {
        if (GroundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(GroundCheck.position, groundRadius);
        }
    }
}
