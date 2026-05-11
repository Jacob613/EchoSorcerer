using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.05f;
    public Rigidbody2D rb;
    Vector2 movementUpdate;
    public float horizontal;


    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck; 
    public float groundDistance = 0.2f;

    public bool isGrounded;

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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.LeftShift))
        {
            print("Crouch");
        }
        

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        
    }
}
