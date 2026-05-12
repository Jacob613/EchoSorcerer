using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 movementUpdate;
    [SerializeField] private float horizontal;


    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float groundDistance = 0.2f;

    
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool doubleJump;
    [SerializeField] public bool wallJump;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashTime = 0.15f;
    [SerializeField] private float lookDirection = 1f;

    [SerializeField] private Hands hands;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded)
        {
            doubleJump = true;
            canDash = true;
            wallJump = true;
        }


        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || doubleJump || (wallJump && hands.canGrab)))
        {
            if (hands.canGrab)
                wallJump = false;
            else if (!isGrounded)
                doubleJump = false;
            

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (!isDashing && !hands.grabbed)
        {
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }

        if (horizontal > 0)
            lookDirection = 1;
        else if (horizontal < 0)
            lookDirection = -1;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        rb.linearVelocity = new Vector2(lookDirection * dashSpeed, 0f);

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
    }


}
