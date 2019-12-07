using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LockPick{
    public class Wire : MonoBehaviour
    {
        bool minigameOver;

        public static bool firstStarted;

        private Vector2 liftUp = new Vector2(0, 2f);
        float liftUpSpeedMult = 1;
        private Vector2 startPosition;
        private Vector2 successPosition;

        public GameObject secondWire;

        //Management of new "wires"
        public static int wire_Count = -1;
        private float[] pinHeight = new float[] {-1.1f, -2.05f, -3.65f };
        private float[] spriteLocation = new float[] { 3f, 2.1f, .5f };
        public static int[] remaining = new int[] {0, 1, 2};

        private int pinSpriteIndex;

        public GameObject SuccessArea;

        public Text uiText;

        //Create a timer and timeLimit
        private float timer;
        private float timeLimit;
        public static float newTimer;

        //Audio for Pin push and Unlocking
        public AudioClip pinPush;
        public AudioClip unlockLock;

        public AudioSource pushPin;
        public AudioSource unlock;


        private int FindPinSpriteIndex()
        {


        int result = 0;
            int rand = Random.Range(0, 3);
            if(remaining[rand] == -1)
            {
                print("Index should be changing");
                return FindPinSpriteIndex();
            }
            else
            {
                result = rand;
            }


            return result;
        }

        private void WinState()
        {
            if (!minigameOver)
            {

                //uiText.GetComponent<Text>().text = "Nice!";
                print("You won");

                unlock.clip = unlockLock;
                unlock.Play();
                wire_Count = -1;

                remaining = new int[] { 0, 1, 2 };
                minigameOver = true;
                MinigameManager.FinishMinigame(true);
                /*
                 * Probably will add more things.
                 * Lock Background image should be changed as popped open instead of still closed
                 * audio plays of a very satisfying, audible "pop"
                 */
            }
        }

        private void LoseState()
        {
            if (!minigameOver)
            {

                wire_Count = -1;

                //uiText.GetComponent<Text>().text = "Oof. Maybe next time";
                remaining = new int[] { 0, 1, 2 };
                minigameOver = true;
                MinigameManager.FinishMinigame(false);
            }
        }


        // Start is called before the first frame update
        void Start()
        {

            float difficulty = MinigameManager.GetDifficulty();
            timeLimit = Mathf.LerpUnclamped(16f, 2f, difficulty);
            liftUpSpeedMult = Mathf.LerpUnclamped(1, 5, difficulty);
            GetComponent<Rigidbody2D>().gravityScale = Mathf.LerpUnclamped(1, 5, difficulty);

            startPosition = transform.position;
            successPosition = SuccessArea.transform.position;

            if (wire_Count == -1)
            {
                timer = timeLimit;
            }
            else
            {
                timer = newTimer;
            }
            wire_Count++;

            if(!minigameOver)
            pinSpriteIndex = FindPinSpriteIndex();
            if(remaining[pinSpriteIndex] == -1 && !minigameOver)
            {
                FindPinSpriteIndex();
                successPosition.y = spriteLocation[pinSpriteIndex];
                SuccessArea.transform.position = successPosition;
            }
            successPosition.y = spriteLocation[pinSpriteIndex];
            SuccessArea.transform.position = successPosition;

            secondWire.GetComponent<Wire>().SuccessArea = SuccessArea;
            secondWire.GetComponent<Wire>().uiText = uiText;

            unlock.clip = unlockLock;
            pushPin.clip = pinPush;

        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            newTimer = timer;
            uiText.text = "Time Left: " + Mathf.RoundToInt(timer);
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
            if (Input.GetButton("Primary") && !minigameOver)
            {
                GetComponent<Rigidbody2D>().velocity = liftUp * liftUpSpeedMult;            
            }

            if (remaining[pinSpriteIndex] == -1 && !minigameOver)
            {
                FindPinSpriteIndex();
                successPosition.y = spriteLocation[pinSpriteIndex];
                SuccessArea.transform.position = successPosition;
            }

            //Presses button at a certain point to see if the lock pick actually works
            if (Input.GetButtonDown("Secondary") && !minigameOver)
            {
                print("pin height is " + pinHeight[pinSpriteIndex]);
                print("remaining spriteIndex is " + remaining[pinSpriteIndex]);
                //Success if it's in range
                if(GetComponent<Rigidbody2D>().position.y > pinHeight[pinSpriteIndex/*wire_Count*/] && GetComponent<Rigidbody2D>().position.y < pinHeight[pinSpriteIndex/*wire_Count*/] + .7f)
                {
                    remaining[pinSpriteIndex] = -1;
                    pushPin.Play();
                    newTimer = timer + 2f;
                    GetComponent<Rigidbody2D>().rotation = -3;

                    //Shift the SuccessArea Sprite to go along with the new Areas to stop the Wire
                    successPosition.y = spriteLocation[pinSpriteIndex];
                    SuccessArea.transform.position = successPosition;
                    /*if (wire_Count == 0)
                    {
                        successPosition.y = 2.1f;
                        SuccessArea.transform.position = successPosition;
                        

                    }
                    if (wire_Count == 1)
                    {
                        successPosition.y = .5f;
                        SuccessArea.transform.position = successPosition;
                        
                    }
                    */

                    if (wire_Count < 2)
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
                //print(GetComponent<Rigidbody2D>().position.y);
                
            }
        }
    }
}

