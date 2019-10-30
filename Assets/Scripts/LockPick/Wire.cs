using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LockPick{
    public class Wire : MonoBehaviour
    {
        public Vector2 liftUp;
        public Vector2 startPosition;

        public GameObject secondWire;

        public static int wireCount = 0;
        public float[] pinHeight;

        public SpriteRenderer SuccessArea;


        private void WinState()
        {
            print("You won!");
            /*
             * Probably will add more things.
             * Lock Background image should be changed as popped open instead of still closed
             * audio plays of a very satisfying, audible "pop"
             */
        }
        
        // Start is called before the first frame update
        void Start()
        {
            print("Does this work?");
            startPosition = transform.position;
            wireCount++;


        }

        // Update is called once per frame
        void Update()
        {
            //Resets the position of pin if it touches top border
            if (GetComponent<Rigidbody2D>().position.y > 2.65)
            {
                transform.position = startPosition;
                print("You broke the lock!");
            }
            
            //This is how to move the "wire" up.
            if (Input.GetKey(KeyCode.Z))
            {
                GetComponent<Rigidbody2D>().velocity = liftUp;            
            }

            //Presses button at a certain point to see if the lock pick actually works
            if (Input.GetKeyDown(KeyCode.X))
            {
                //Success if it's in range
                if(GetComponent<Rigidbody2D>().position.y > pinHeight[wireCount] && GetComponent<Rigidbody2D>().position.y < pinHeight[wireCount] + 0.5f)
                {
                    print("You succeeded!");

                    //Shift the SuccessArea Sprite to go along with the new Areas to stop the Wire

                    if (wireCount < 3)
                    {
                        Instantiate(secondWire, startPosition + new Vector2(0.5f, 0), Quaternion.identity);

                        GetComponent<Rigidbody2D>().gravityScale = 0;
                        this.enabled = false;
                    }
                    else
                    {
                        //Create a "You won!" function
                        WinState();
                        GetComponent<Rigidbody2D>().gravityScale = 0;
                        this.enabled = false;
                        
                    }
                }

                //Failure if it's not in range
                else
                {
                    print("Oof, you failed.");
                    transform.position = startPosition;
                }

                //This is to get a position to set parameters for the areas
                print(GetComponent<Rigidbody2D>().position.y);
            }
        }
    }
}

