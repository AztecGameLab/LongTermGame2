using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    public Vector2 startDir;
    Vector2 dir;
    public float speed;
    public float speedIncRate;
    // Start is called before the first frame update
    void Start()
    {
        dir = startDir;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = dir * speed;
        speed += speedIncRate;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir=Vector2.Reflect(dir, collision.contacts[0].normal);

        if(collision.rigidbody)
        {
            Destroy(collision.rigidbody.gameObject.transform.parent.gameObject);
            print("destroyed");
        }
    }
   
}
