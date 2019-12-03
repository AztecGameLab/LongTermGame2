using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Memorize {
    public class PlayerController : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] GameObject buttonPrefab, placeholderPrefab;
        [SerializeField] float absoluteMaxTime, waitTime, deltaSpeed;
        [SerializeField] AudioClip correct, incorrect, music;
        [SerializeField] ushort maxButtons, minButtons, loops;
        #pragma warning restore 0649

        GameObject[] buttons, placeholders;
        KeyCode[] keys;
        SpriteRenderer[] placeholderSprites;
        Slider slider;
        Text memorizeText, repeatText;
        float maxTime, deltaButtons;
        float speed = 1f;
        bool inputAllowed, isWin;
        ushort c;

        void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            Text[] texts = GetComponentsInChildren<Text>();
            memorizeText = texts[0];
            repeatText = texts[1];

            deltaButtons = maxButtons - minButtons;
            maxTime = absoluteMaxTime;
            isWin = true;
        }

        void Start()
        {
            StartCoroutine(GameLoop());
        }

        void Update()
        {
            if (inputAllowed)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    KeyCheck(KeyCode.UpArrow);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    KeyCheck(KeyCode.DownArrow);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    KeyCheck(KeyCode.LeftArrow);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    KeyCheck(KeyCode.RightArrow);
                }
                inputAllowed = c < buttons.Length;
            }
            AudioManager.instance.SetMusicPitch(speed += Time.deltaTime * deltaSpeed);
        }

        void KeyCheck(KeyCode code)
        {
            if (keys[c] == code)
            {
                placeholderSprites[c].color = Color.green;
                AudioManager.instance.PlaySFX(correct, 1f);
            }
            else
            {
                isWin = false;
                AudioManager.instance.PlaySFX(incorrect, 1f);
            }
            placeholders[c++].SetActive(true);
        }

        float RandomDirection(ushort i)
        {
            float r = Random.value;
            // TODO better way to sort this shared logic
            keys[i] = r < 0.5f ? (r < 0.25f ? KeyCode.UpArrow : KeyCode.LeftArrow) : (r < 0.75f ? KeyCode.DownArrow : KeyCode.RightArrow);
            return r < 0.5f ? (r < 0.25f ? 0f : 90f) : (r < 0.75f ? 180f : 270f);
        }

        IEnumerator GameLoop()
        {
            slider.gameObject.SetActive(memorizeText.enabled = repeatText.enabled = false);
            AudioManager.instance.PlayMusic(music, 1f, speed, true);
            yield return new WaitForSeconds(waitTime);

            for (int h = 0; h < loops; h++)
            {
                c = 0;
                slider.maxValue = maxTime;
                slider.value = slider.minValue;
                memorizeText.enabled = true;

                // generate new set of buttons and placeholders
                int size = Mathf.RoundToInt(Random.value * deltaButtons) + minButtons;
                buttons = new GameObject[size];
                placeholders = new GameObject[size];
                placeholderSprites = new SpriteRenderer[size];
                keys = new KeyCode[size];
                // TODO change UI and controls
                float j = -buttons.Length - 1;
                for (ushort i = 0; i < buttons.Length; i++)
                {
                    j += 2;
                    placeholders[i] = Instantiate(placeholderPrefab, new Vector3(j, -1.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
                    placeholders[i].SetActive(false);
                    placeholderSprites[i] = placeholders[i].GetComponent<SpriteRenderer>();
                    buttons[i] = Instantiate(buttonPrefab, new Vector3(j, 0f, 0f), Quaternion.Euler(0f, 0f, RandomDirection(i)));
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
                yield return new WaitForSeconds(waitTime);
                repeatText.enabled = true;
                slider.gameObject.SetActive(true);
                // repeat
                inputAllowed = true;
                while (slider.value > slider.minValue)
                {
                    slider.value -= Time.deltaTime;
                    yield return null; // yields for Update()
                }
                inputAllowed = false;
                slider.gameObject.SetActive(false);
                repeatText.enabled = false;

                // compare results
                foreach (GameObject button in buttons)
                {
                    button.SetActive(true);
                }
                yield return new WaitForSeconds(absoluteMaxTime);

                // clean up
                foreach (GameObject button in buttons)
                {
                    Destroy(button);
                }
                foreach (GameObject placeholder in placeholders)
                {
                    Destroy(placeholder);
                }

                if (!isWin)
                {
                    AudioManager.instance.StopMusic();
                    MinigameManager.FinishMinigame(isWin);
                    yield break;
                }
                
                // up the ante
                maxButtons++;
                minButtons++;
                maxTime--;
                yield return null;
            }
            
            AudioManager.instance.StopMusic();
            MinigameManager.FinishMinigame(isWin);
        }
    }
}
