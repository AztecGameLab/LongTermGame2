using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Potions
{
    public class GameController : MonoBehaviour
    {
        public GameObject fillLine;
        public GameObject colorTarget;
        public GameObject bottleOrigin;
        public GameObject celebrationParticles;

        [Range(0, 1)]
        public float filled;

        public float fillRate;
        public float filledSize;

        [Range(0, 1)]
        public float r, g, b, opaqueness;

        float redAmount, yellowAmount, blueAmount;
        float redPotency, yellowPotency, bluePotency;
        private float overflowAmount;

        GameObject instantiatedColorTarget;
        GameObject instantiatedCelebrationParticles;
        Text scoreText;
        int score;

        float timeBuffer;
        float bufferStartTime;

        Text timeText;
        float countdownTime;

        Text PotionsNeededText;
        int potionsNeeded;

        Color targetColor;

        public float acceptableRange;

        bool canTakeInput;

        Vector3[] RYBtoRGBCube = new Vector3[8]
        {
            new Vector3(1f,1f,1f),
            new Vector3(0,0,1f),
            new Vector3(1f,0f,1f),
            new Vector3(1f,0,0),
            new Vector3(1f,1f,0),
            new Vector3(0,1f,0),
            new Vector3(0,0,0),
            new Vector3(1f,0.5f,0),
        };

        [SerializeField]
        AudioClip pourSound;

        // Start is called before the first frame update
        void Start()
        {
            canTakeInput = true;
            timeBuffer = 0.5f;
            
            score = 0;
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            scoreText.text = score.ToString();

            countdownTime = 20f;
            timeText = GameObject.Find("TimeText").GetComponent<Text>();
            timeText.text = countdownTime.ToString();

            potionsNeeded = 4;
            PotionsNeededText = GameObject.Find("PotionsNeededText").GetComponent<Text>();
            PotionsNeededText.text = potionsNeeded.ToString();

            filled = 0;
            r = 0;
            g = 0;
            b = 0;
            opaqueness = 1;

            Instantiate(fillLine, bottleOrigin.transform.position + new Vector3(0, filledSize, 0), Quaternion.identity);
            instantiatedColorTarget = Instantiate(colorTarget, bottleOrigin.transform.position + new Vector3(-5, filledSize,0),Quaternion.identity);
            targetColor = GetRandomColor();
            instantiatedColorTarget.GetComponent<SpriteRenderer>().color = targetColor;
            print(targetColor.r);
            print(targetColor.g);
            print(targetColor.b);
        }

        // Update is called once per frame
        void Update()
        {
            scoreText.text = score.ToString();
            countdownTime -= Time.deltaTime;
            timeText.text = countdownTime.ToString();

            if(countdownTime <= 0)
            {
                Lose();
            }

            PotionsNeededText.text = potionsNeeded.ToString();

            if (canTakeInput)
            {
                if (Input.GetAxisRaw("Vertical") == -1 || Input.GetKeyDown(KeyCode.Space))
                {
                    DumpPotion();
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Reset();
                }

                bool isThereInput = Input.GetAxisRaw("Horizontal") == -1 || Input.GetKey(KeyCode.R) || Input.GetAxisRaw("Vertical") == 1 || Input.GetKey(KeyCode.Y) || Input.GetAxisRaw("Horizontal") == 1 || Input.GetKey(KeyCode.B);
                if (isThereInput && !AudioManager.instance.GetIsSFXPlaying())
                {
                    AudioManager.instance.PlaySFX(pourSound, 1f, true);
                }
                else if (!isThereInput && AudioManager.instance.GetIsSFXPlaying())
                {
                    AudioManager.instance.StopSFX();
                }

                if (Input.GetAxisRaw("Horizontal") == -1 || Input.GetKey(KeyCode.R))
                {
                    redAmount += fillRate * Time.deltaTime;
                }
                else if (Input.GetAxisRaw("Vertical") == 1 || Input.GetKey(KeyCode.Y))
                {
                    yellowAmount += fillRate * Time.deltaTime;
                }
                else if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetKey(KeyCode.B))
                {
                    blueAmount += fillRate * Time.deltaTime;
                }
            }
            else
            {
                if(Time.time >= bufferStartTime + timeBuffer)
                {
                    CorrectPotion();
                    canTakeInput = true;
                }
            }

            if (redAmount + yellowAmount + blueAmount > 1)
            {
                overflowAmount = redAmount + yellowAmount + blueAmount - 1;
                redAmount -= overflowAmount * (redAmount / (redAmount + yellowAmount + blueAmount));
                yellowAmount -= overflowAmount * (yellowAmount / (redAmount + yellowAmount + blueAmount));
                blueAmount -= overflowAmount * (blueAmount / (redAmount + yellowAmount + blueAmount));
            }

            filled = redAmount + yellowAmount + blueAmount;

            redPotency = redAmount / (redAmount + yellowAmount + blueAmount);
            yellowPotency = yellowAmount / (redAmount + yellowAmount + blueAmount);
            bluePotency = blueAmount / (redAmount + yellowAmount + blueAmount);
            RYBtoRGB(redPotency, yellowPotency, bluePotency,out r,out g,out b);

            if (canTakeInput)
            {
                if (Mathf.Abs(r - targetColor.r) <= acceptableRange && Mathf.Abs(g - targetColor.g) <= acceptableRange && Mathf.Abs(b - targetColor.b) <= acceptableRange && filled >= 0.25)
                {
                    score++;
                    canTakeInput = false;
                    bufferStartTime = Time.time;
                    instantiatedCelebrationParticles = Instantiate(celebrationParticles, bottleOrigin.transform.position + new Vector3(0, filledSize * filled, 0), Quaternion.identity);
                    //ParticleSystem.MainModule main = instantiatedCelebrationParticles.GetComponent<ParticleSystem>().main;
                    //main.startColor = targetColor;
                }
            }

        }

        private void Reset()
        {
            DumpPotion();

            Destroy(instantiatedColorTarget);
            instantiatedColorTarget = Instantiate(colorTarget, bottleOrigin.transform.position + new Vector3(-5, filledSize, 0), Quaternion.identity);
            targetColor = GetRandomColor();
            instantiatedColorTarget.GetComponent<SpriteRenderer>().color = targetColor;
        }

        private void CorrectPotion()
        {
            Reset();
            potionsNeeded--;
            if (potionsNeeded <= 0)
            {
                Win();
            }
        }

        private void DumpPotion()
        {
            filled = 0;
            r = 0;
            g = 0;
            b = 0;
            opaqueness = 1;
            redAmount = 0;
            blueAmount = 0;
            yellowAmount = 0;
            redPotency = 0;
            bluePotency = 0;
            yellowPotency = 0;
            overflowAmount = 0;
        }

        private void RYBtoRGB(float r_RYB, float y_RYB, float b_RYB, out float r, out float g, out float b)
        {
            Vector3 interp1 = Vector3.Lerp(RYBtoRGBCube[0], RYBtoRGBCube[3], r_RYB);
            Vector3 interp2 = Vector3.Lerp(RYBtoRGBCube[1], RYBtoRGBCube[2], r_RYB);
            Vector3 interp3 = Vector3.Lerp(RYBtoRGBCube[4], RYBtoRGBCube[7], r_RYB);
            Vector3 interp4 = Vector3.Lerp(RYBtoRGBCube[5], RYBtoRGBCube[6], r_RYB);

            Vector3 interp5 = Vector3.Lerp(interp1, interp2, b_RYB);
            Vector3 interp6 = Vector3.Lerp(interp3, interp4, b_RYB);

            Vector3 interp7 = Vector3.Lerp(interp5, interp6, y_RYB);

            r = interp7.x;
            g = interp7.y;
            b = interp7.z;
        }

        private Color GetRandomColor()
        {
            float val1, val2, val3;
            float red, yellow, blue;
            val1 = Random.Range(0f, 1f);
            val2 = Random.Range(0f, 1f - val1);
            val3 = 1f - val1 - val2;
            int sort = Random.Range(0, 5);

            if(sort == 0)
            {
                red = val1;
                yellow = val2;
                blue = val3;
            }
            else if(sort == 1)
            {
                red = val2;
                yellow = val1;
                blue = val3;
            }
            else if(sort == 2)
            {
                red = val3;
                yellow = val2;
                blue = val1;
            }
            else if(sort == 3)
            {
                red = val1;
                yellow = val3;
                blue = val2;
            }
            else if(sort == 4)
            {
                red = val2;
                yellow = val3;
                blue = val1;
            }
            else
            {
                red = val3;
                yellow = val1;
                blue = val1;
            }
            float r, g, b;
            RYBtoRGB(red, yellow, blue, out r, out g, out b);
            return new Color(r, g, b);
        }
        private void Win()
        {
            MinigameManager.FinishMinigame(true);
        }
        private void Lose()
        {
            MinigameManager.FinishMinigame(false);
        }
    }
}
