using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LockPick{
    public class Wire : MonoBehaviour
    {
        private Vector2 liftUp = new Vector2(0, 2f);
        private Vector2 startPosition;
        private Vector2 successPosition;

        public GameObject secondWire;

        public static int wire_Count = 0;
        public float[] pinHeight;

        public GameObject SuccessArea;

        public Text uiText;

        //Create a timer and timeLimit
        private float timer = 0f;
        private float timeLimit = 16f;


        private void WinState()
        {
            //uiText.GetComponent<Text>().text = "Nice!";
            //print("We're in!");

            wire_Count = 0;

            MinigameManager.FinishMinigame(true);
            /*
             * Probably will add more things.
             * Lock Background image should be changed as popped open instead of still closed
             * audio plays of a very satisfying, audible "pop"
             */
        }

        private void LoseState()
        {
            wire_Count = 0;

            //uiText.GetComponent<Text>().text = "Oof. Maybe next time";
            MinigameManager.FinishMinigame(false);
        }
        
        // Start is called before the first frame update
        void Start()
        {
            float difficulty = MinigameManager.GetDifficulty();
            timeLimit = Mathf.LerpUnclamped(8f, 16f, difficulty);

            startPosition = transform.position;
            successPosition = SuccessArea.transform.position;
            wire_Count++;
            secondWire.GetComponent<Wire>().SuccessArea = SuccessArea;
            secondWire.GetComponent<Wire>().uiText = uiText;


        }

        // Update is called once per frame
        void Update()
        {

            timer += Time.deltaTime;
            if(timer >= timeLimit)
            {
                LoseState();
            }
            //Resets the position of pin if it touches top border
            if (GetComponent<Rigidbody2D>().position.y > 2.65)
            {
                transform.position = startPosition;
                print("You broke the lock!");
            }
            
            //This is how to move the "wire" up.
            if (Input.GetButton("Primary"))
            {
                GetComponent<Rigidbody2D>().velocity = liftUp;            
            }

            //Presses button at a certain point to see if the lock pick actually works
            if (Input.GetButtonDown("Secondary"))
            {
                //Success if it's in range
                if(GetComponent<Rigidbody2D>().position.y > pinHeight[wire_Count] && GetComponent<Rigidbody2D>().position.y < pinHeight[wire_Count] + 0.5f)
                {
                    

                    //Shift the SuccessArea Sprite to go along with the new Areas to stop the Wire
                    if (wire_Count == 1)
                    {
                        //uiText.GetComponent<Text>().text = "Nice! Time to Stick the nail in";
                        successPosition.y += .7f;
                        SuccessArea.transform.position = successPosition;
                        

                    }
                    if (wire_Count == 2)
                    {
                        //uiText.GetComponent<Text>().text = "Just the paperclip now...";
                        successPosition.y = 1.4f;
                        SuccessArea.transform.position = successPosition;
                        
                    }


                    if (wire_Count < 3)
                    {
                        Instantiate(secondWire, startPosition + new Vector2(0.5f, 0), Quaternion.identity);

                        GetComponent<Rigidbody2D>().gravityScale = 0;
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                        this.enabled = false;
                    }
                    else
                    {
                                               
                        GetComponent<Rigidbody2D>().gravityScale = 0;
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        this.enabled = false;
                        //Create a "You won!" function
                        WinState();

                    }
                }

                //Failure if it's not in range
                else
                {
                    //print("Oof, you failed.");
                    transform.position = startPosition;
                }

                //This is to get a position to set parameters for the areas
                //print(GetComponent<Rigidbody2D>().position.y);
            }
        }
    }
}

