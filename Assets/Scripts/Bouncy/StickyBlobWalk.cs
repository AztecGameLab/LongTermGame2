using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBlobWalk : MonoBehaviour
{
    Rigidbody2D rb;
    public float walkForce;
    public float fallingGravity;
    [HideInInspector]
    public Vector2 up;
    public float stickforce;
    public float stickDistance;
    public LayerMask terrain;
    public float checkRadius;
    [HideInInspector]
    public bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        up = Vector2.up;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(new Vector2(up.y, -up.x) * Input.GetAxisRaw("Horizontal") * walkForce);
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, checkRadius,-up, stickDistance, terrain);
        Debug.DrawRay(transform.position, -up, Color.red);
        if (hit.collider != null)
        {
            up = hit.normal;
            rb.AddForce(new Vector2(up.y, -up.x) * Input.GetAxisRaw("Horizontal") * walkForce);
            rb.AddForce(-up*stickforce);
            grounded = true;

        }
        else
        {
            //up = Vector2.up;
            grounded = false;
            rb.AddForce(-Vector2.up*fallingGravity);
        }
    }
    private void LateUpdate()
    {
        //up = Vector2.up;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("collided with: "+collision.contacts[0].normal);
    }
}
