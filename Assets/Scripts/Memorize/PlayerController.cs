﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Memorize {
    public class PlayerController : MonoBehaviour
    {
        struct ControlStick
        {
            public float x, y;

            public ControlStick(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public static bool operator ==(ControlStick cs1, ControlStick cs2)
            {
                return cs1.x == cs2.x && cs1.y == cs2.y;
            }

            public static bool operator !=(ControlStick cs1, ControlStick cs2)
            {
                return !(cs1 == cs2);
            }

            public override bool Equals(object obj)
            {
                return obj is ControlStick && this == (ControlStick)obj;
            }

            public override int GetHashCode() 
            {
                return x.GetHashCode() ^ y.GetHashCode();
            }
        };

        const float absoluteMaxTime = 5f;
        const float initialWaitTime = 0.5f;
        const float waitTime = 3f;
        const float deltaSpeed = 0.005f;
        const float threshold = 0.333f;
        const float magnitude = 0.25f;
        const float duration = 0.1f;
        const float foodDuration = 2f;
        const float dampening = 0.5f;

        #pragma warning disable 0649
        [SerializeField] GameObject buttonPrefab, food;
        [SerializeField] AudioClip correct, incorrect, music;
        [SerializeField] Transform bgTransform, panTransform;
        #pragma warning restore 0649

        GameObject[] buttons;
        SpriteRenderer[] buttonSprites;
        ControlStick[] inputBuffer, keys, identityStick;
        Text memorizeText, repeatText;
        Slider slider;
        Vector3 initialBGPosition, initialPanPosition;
        float maxTime, deltaButtons, speed, timer, foodTimer, panDisplacement;
        bool isInputAllowed, isWin;
        ushort c, minButtons;

        void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            Text[] texts = GetComponentsInChildren<Text>();
            memorizeText = texts[0];
            repeatText = texts[1];
            inputBuffer = new ControlStick[2];
            identityStick = new ControlStick[4];
            identityStick[0] = new ControlStick(0f, 1f); // up
            identityStick[1] = new ControlStick(-1f, 0f); // left
            identityStick[2] = new ControlStick(0f, -1f); // down
            identityStick[3] = new ControlStick(1f, 0f); // right
            minButtons = 3;
            deltaButtons = 1f;
            isWin = true;
            speed = 1f;
            panDisplacement = 2f;
        }

        void Start()
        {
            food.SetActive(false);
            initialBGPosition = bgTransform.position;
            initialPanPosition = panTransform.position;
            maxTime = Mathf.Lerp(5f, 1f, MinigameManager.GetDifficulty());
            StartCoroutine(GameLoop(3));
        }

        void Update()
        {
            inputBuffer[1] = inputBuffer[0]; // shift
            inputBuffer[0] = new ControlStick(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            // pan movement
            panTransform.position = new Vector3(initialPanPosition.x + inputBuffer[0].x * panDisplacement, initialPanPosition.y + inputBuffer[0].y * panDisplacement, initialPanPosition.z);
            // clamp
            inputBuffer[0] = new ControlStick(inputBuffer[0].x > threshold ? 1f : (inputBuffer[0].x < -threshold ? -1f : 0f), inputBuffer[0].y > threshold ? 1f : (inputBuffer[0].y < -threshold ? -1f : 0f));
            
            if (isInputAllowed)
            {
                // not diagonal, centered, or the same input
                if (Mathf.Abs(inputBuffer[0].x) != Mathf.Abs(inputBuffer[0].y) && inputBuffer[0] != inputBuffer[1])
                {
                    if (keys[c] == inputBuffer[0])
                    {
                        buttonSprites[c].color = Color.green;
                        AudioManager.instance.PlaySFX(correct, 1f);
                        food.SetActive(true);
                        foodTimer = foodDuration;
                    }
                    else
                    {
                        isWin = false;
                        buttonSprites[c].color = Color.red;
                        AudioManager.instance.PlaySFX(incorrect, 1f);
                        timer = duration;
                        food.SetActive(false);
                        foodTimer = 0f;
                    }
                    buttons[c++].SetActive(true);
                }
                bool isNotDone = c < buttons.Length;
                isInputAllowed = isNotDone;
                if (!isNotDone)
                {
                    slider.value = slider.minValue;
                }
            }

            AudioManager.instance.SetMusicPitch(speed += Time.deltaTime * deltaSpeed);
            if (timer > 0f)
            {
                bgTransform.position = initialBGPosition + Random.insideUnitSphere * magnitude;
                timer -= Time.deltaTime * dampening;
            }
            else
            {
                bgTransform.position = initialBGPosition;
            }
            if (foodTimer > 0f)
            {
                foodTimer -= Time.deltaTime;
            }
            else
            {
                food.SetActive(false);
            }
        }

        float RandomDirection(ushort i)
        {
            int r = Random.Range(0, 4);
            keys[i] = identityStick[r];
            return r * 90f;
        }

        IEnumerator GameLoop(ushort loops)
        {
            slider.gameObject.SetActive(memorizeText.enabled = repeatText.enabled = false);
            AudioManager.instance.PlayMusic(music, 1f, speed, true);
            yield return new WaitForSeconds(initialWaitTime);

            for (ushort h = 0; h < loops; h++)
            {
                c = 0;
                slider.maxValue = maxTime;
                slider.value = slider.minValue;
                memorizeText.enabled = true;

                // generate new set of buttons and placeholders
                int size = Mathf.RoundToInt(Random.value * deltaButtons) + minButtons;
                buttons = new GameObject[size];
                buttonSprites = new SpriteRenderer[size];
                keys = new ControlStick[size];

                float j = -buttons.Length - 1;
                for (ushort i = 0; i < buttons.Length; i++)
                {
                    j += 2;
                    buttons[i] = Instantiate(buttonPrefab, new Vector3(j, 0f, 0f), Quaternion.Euler(0f, 0f, RandomDirection(i)));
                    buttonSprites[i] = buttons[i].GetComponent<SpriteRenderer>();
                }

                slider.gameObject.SetActive(true);
                // memorize
                while (slider.value < slider.maxValue)
                {
                    slider.value += Time.deltaTime;
                    yield return null;
                }
                slider.gameObject.SetActive(false);
                memorizeText.enabled = false;

                // hide buttons
                foreach (GameObject button in buttons)
                {
                    button.SetActive(false);
                }

                slider.value = slider.maxValue = absoluteMaxTime;
                repeatText.enabled = true;
                slider.gameObject.SetActive(true);
                // repeat
                isInputAllowed = true;
                while (slider.value > slider.minValue)
                {
                    slider.value -= Time.deltaTime;
                    yield return null; // yields for Update()
                }
                isInputAllowed = false;
                slider.gameObject.SetActive(false);
                repeatText.enabled = false;
                if (c < buttons.Length)
                {
                    isWin = false;
                }

                // compare results
                foreach (GameObject button in buttons)
                {
                    button.SetActive(true);
                }
                yield return new WaitForSeconds(waitTime);

                // clean up
                foreach (GameObject button in buttons)
                {
                    Destroy(button);
                }

                if (!isWin)
                {
                    AudioManager.instance.StopMusic();
                    MinigameManager.FinishMinigame(isWin);
                    yield break;
                }
                
                // up the ante
                minButtons++;
                yield return null;
            }
            
            AudioManager.instance.StopMusic();
            MinigameManager.FinishMinigame(isWin);
        }
    }
}
