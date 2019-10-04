using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimothyLWireLockPicker : MonoBehaviour
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
        //Resets the position of pin if it touches top border
        if (GetComponent<Rigidbody2D>().position.y > 2.15)
        {
            transform.position = startPosition;
        }
        

        if (Input.GetKey(KeyCode.Z))
        {
            GetComponent<Rigidbody2D>().velocity = liftUp;
            
        }

        //Presses button at a certain point to see if the lock pick actually works
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Success if it's in range
            if(GetComponent<Rigidbody2D>().position.y > 1.05 && GetComponent<Rigidbody2D>().position.y < 1.55)
            {
                print("You succeeded!");
            }

            //Failure if it's not in range
            else
            {
                print("Oof, you failed.");
            }


            //This is to get a position to set parameters for the areas
            print(GetComponent<Rigidbody2D>().position);
        }




    }



}
