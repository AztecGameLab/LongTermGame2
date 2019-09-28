using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimothyLWire : MonoBehaviour
{
    public Vector2 liftUp;

    
    
    // Start is called before the first frame update
    void Start()
    {
        print("Does this work?");
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
        if (GetComponent<Rigidbody2D>().position.y > 2.69)
        {

        }

        //They press this when they want to "turn" the wire and "pop" the lock open
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (GetComponent<Rigidbody2D>().position.y > 0)
            {
                print("Wow you made it work");
            }
            print (GetComponent<Rigidbody2D>().position);
            
        }


    }

    //Need to record WireLockPicker hitting Top or Bottom Border
    
}
