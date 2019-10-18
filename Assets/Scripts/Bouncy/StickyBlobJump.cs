using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBlobJump : MonoBehaviour
{
    public float jumpForce;
    public StickyBlobWalk walker;
    // Start is called before the first frame update
    void Start()
    {
        walker = GetComponent<StickyBlobWalk>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)&&walker.grounded)
        {           
            GetComponent<Rigidbody2D>().AddForce(walker.up*jumpForce);
        }
    }
}
