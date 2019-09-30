using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimothyLWire : MonoBehaviour
{
    public Vector2 liftUp;
    Vector2 startPosition;

    
    
    // Start is called before the first frame update
    void Start()
    {
        print("Does this work?");
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //This is how they're going to move the lockpicking wire
        if (Input.GetKey(KeyCode.Z))
        {
            GetComponent<Rigidbody2D>().velocity = liftUp;
        }

        //Creating a case where if it touches the top border, lock breaks and user has to start over
        if (GetComponent<Rigidbody2D>().position.y > 2.65)
        {
            print("You broke the lock!");
            transform.position = startPosition;
        }

        //They press this when they want to "turn" the wire and "pop" the lock open
        //Need to make a range for a successful "lockpick"
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (GetComponent<Rigidbody2D>().position.y > 2.1 && GetComponent<Rigidbody2D>().position.y < 2.4)
            {
                print("You succeeded!");
                /* This is just if I ever needed to get y position of places ever again by eye without calculations (typically if I were to change the range of the "lock")
                 * print(GetComponent<Rigidbody2D>().position.y);
                 */ 
            }
            else
            {
                print("Oof you failed.");
                transform.position = startPosition;
            }
            
        }


    }

    
    
}
