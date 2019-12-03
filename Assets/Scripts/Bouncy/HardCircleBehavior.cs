using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCircleBehavior : MonoBehaviour
{
    public bool sticky;
    public LayerMask terrain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (sticky)
        {
            //GetComponentInParent<Rigidbody2D>().AddForce(collision.contacts[0].normal * 20);
            //if(collision.otherCollider.gameObject.layer==terrain)
                GetComponentInParent<StickyBlobWalk>().up = collision.contacts[0].normal;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (sticky)
        {
           
            //GetComponentInParent<StickyBlobWalk>().up = Vector2.up;
        }
    }
}
