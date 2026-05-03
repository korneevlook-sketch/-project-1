using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private float moveInput;
    private bool jumpPressed;
    private bool isGrounded;
    private bool canUseDoubleJump;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            canUseDoubleJump = true;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (!jumpPressed)
            return;

        if (isGrounded)
        {
            Jump();
        }
        else if (canUseDoubleJump && AbilityManager.Instance != null && AbilityManager.Instance.DoubleJumpUnlocked)
        {
            Jump();
            canUseDoubleJump = false;
        }

        jumpPressed = false;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
