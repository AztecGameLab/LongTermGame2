using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverController : MonoBehaviour
{
    private float lastMovement = 0.0f;
    private Rigidbody2D rigidBody;
    private int direction = 1;
    [SerializeField]
    [Range(0, 1)]private float difficultySpeed = 0.0f;
    private float changeSpeed;
    [SerializeField]
    private float moveDistance = 2.0f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        changeSpeed = Mathf.LerpUnclamped(1, 6, difficultySpeed);
        rigidBody.velocity = new Vector2((moveDistance/2) * direction * changeSpeed, rigidBody.velocity.y);
        direction *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveBlock(changeSpeed);
    }
    private void MoveBlock(float changeSpeed)
    {
 
        if (rigidBody.transform.position.x >= moveDistance || rigidBody.transform.position.x <= (moveDistance*-1))
        {
            rigidBody.velocity = new Vector2(moveDistance * direction * changeSpeed, rigidBody.velocity.y);
            direction *= -1;
            ;
            
        }
    }
}
