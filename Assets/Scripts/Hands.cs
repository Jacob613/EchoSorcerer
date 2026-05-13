using UnityEngine;
using System.Collections;

public class Hands : MonoBehaviour
{

    [SerializeField] private PhysicsMaterial2D slipperyMaterial;
    [SerializeField] private Collider2D grabCollider;
    public bool canGrab;
    public bool grabbed;
    [SerializeField] private bool wallJumpBuffer;
    [SerializeField] private float jumpTime;

    public Player body;
    [SerializeField] float dir;
    public float upwardFactor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canGrab = true;
        body.wallJump = true;
        Vector2 normal = collision.GetContact(0).normal;

        if (normal.x > 0.5f)
        {
            Debug.Log("Collided from right");
            dir = 1;
        }

        if (normal.x < -0.5f)
        {
            Debug.Log("Collided from left");
            dir = -1;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canGrab = false;
    }

    void Update()
    {
        if(Input.GetKey("g") && canGrab && !Input.GetKeyDown(KeyCode.Space) && !wallJumpBuffer)
        {
            grabbed = true;
            SetMaterial(null);
            body.rb.linearVelocity = new Vector2(body.rb.linearVelocity.x, 0f);
        }
        else if (grabbed && Input.GetKeyDown(KeyCode.Space))
        {
            grabbed = false;
            wallJumpBuffer = true;
            SetMaterial(slipperyMaterial);
            StartCoroutine(WallRelease());
        }
        else
        {
            grabbed = false;
            SetMaterial(slipperyMaterial);
        }
    }

    void SetMaterial(PhysicsMaterial2D newMaterial)
    {
        grabCollider.sharedMaterial = newMaterial;

        Physics2D.SyncTransforms();
    }

    private IEnumerator WallRelease()
    {

        yield return new WaitForSeconds(jumpTime);

        wallJumpBuffer = false;
    }
}
