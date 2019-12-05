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
        private int displayTimer = 0;

        //Audio for Pin push and Unlocking
        public AudioClip pinPush;
        public AudioClip unlockLock;

        public AudioSource pushPin;
        public AudioSource unlock;


        private void WinState()
        {
            //uiText.GetComponent<Text>().text = "Nice!";
            print("You won");

            unlock.Play();
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
            float difficulty = 0;//MinigameManager.GetDifficulty();
            timeLimit = Mathf.LerpUnclamped(20f, 14f, difficulty);
            timer = timeLimit;

            startPosition = transform.position;
            successPosition = SuccessArea.transform.position;
            wire_Count++;
            secondWire.GetComponent<Wire>().SuccessArea = SuccessArea;
            secondWire.GetComponent<Wire>().uiText = uiText;

            unlock.clip = unlockLock;
            pushPin.clip = pinPush;

        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            displayTimer = Mathf.RoundToInt(timer);
            uiText.text = "Time Left: " + displayTimer;
            if(timer <= 0)
            {
                LoseState();
            }
            //Resets the position of pin if it touches top border
            if (GetComponent<Rigidbody2D>().position.y > -.16)
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
                if(GetComponent<Rigidbody2D>().position.y > pinHeight[wire_Count] && GetComponent<Rigidbody2D>().position.y < pinHeight[wire_Count] + .7f)
                {
                    pushPin.Play();
                    GetComponent<Rigidbody2D>().rotation = -3;

                    //Shift the SuccessArea Sprite to go along with the new Areas to stop the Wire
                    if (wire_Count == 1)
                    {
                        //uiText.GetComponent<Text>().text = "Nice! Time to Stick the nail in";
                        successPosition.y = 2.1f;
                        SuccessArea.transform.position = successPosition;
                        

                    }
                    if (wire_Count == 2)
                    {
                        //uiText.GetComponent<Text>().text = "Just the paperclip now...";
                        successPosition.y = .5f;
                        SuccessArea.transform.position = successPosition;
                        
                    }


                    if (wire_Count < 3)
                    {
                        Instantiate(secondWire, startPosition + new Vector2(.75f, 0), Quaternion.identity);

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
                print(GetComponent<Rigidbody2D>().position.y);
                
            }
        }
    }
}

