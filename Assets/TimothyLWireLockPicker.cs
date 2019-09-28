using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimothyLWireLockPicker : MonoBehaviour
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

        while (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().velocity = liftUp;
        }

        


    }

    //Need to record WireLockPicker hitting Top or Bottom Border
   
}
