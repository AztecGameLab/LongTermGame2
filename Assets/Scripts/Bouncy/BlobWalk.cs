using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobWalk : MonoBehaviour
{
    Rigidbody2D rb;
    public float walkForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.right*Input.GetAxisRaw("Horizontal")*walkForce);
    }
}
