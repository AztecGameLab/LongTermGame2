using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Memorize {
    public class PlayerController : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField]
        GameObject buttonPrefab, placeholderPrefab;
        [SerializeField]
        float absoluteMaxTime, waitTime;
        [SerializeField]
        AudioClip correct, incorrect, music;
        // TODO globalize or pass in difficulty
        [SerializeField]
        ushort maxButtons, minButtons, difficulty;
        #pragma warning restore 0649

        GameObject[] buttons, placeholders;
        KeyCode[] keys;
        SpriteRenderer[] placeholderSprites;
        Slider slider;
        Text memorizeText, repeatText;
        float maxTime, deltaButtons;
        bool inputAllowed;
        ushort c;

        void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            Text[] texts = GetComponentsInChildren<Text>();
            memorizeText = texts[0];
            repeatText = texts[1];

            inputAllowed = false;
            deltaButtons = maxButtons - minButtons;
            // TODO change how difficulty affects these vars
            maxButtons += difficulty;
            minButtons += difficulty;
            maxTime = absoluteMaxTime - difficulty;
        }

        void Start()
        {
            slider.gameObject.SetActive(memorizeText.enabled = repeatText.enabled = false);

            StartCoroutine(GameLoop()); // TODO Loop several times
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
            c = 0;
            slider.maxValue = maxTime;
            slider.value = slider.minValue;
            memorizeText.enabled = true;
            AudioManager.instance.PlayMusicLoop(music, 1f, difficulty > 0 ? (float)difficulty / absoluteMaxTime + 1f : 1f);
            yield return new WaitForSeconds(waitTime);

            // generate new set of buttons and placeholders
            int size = Mathf.RoundToInt(Random.value * deltaButtons) + minButtons;
            buttons = new GameObject[size];
            placeholders = new GameObject[size];
            placeholderSprites = new SpriteRenderer[size];
            keys = new KeyCode[size];
            // TODO fix size/placement of buttons on screen with vars
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

            // show buttons
            foreach (GameObject button in buttons)
            {
                button.SetActive(true);
            }
            // TODO make score
            // TODO show score, score = (remaining input time + completion%) * difficulty
            yield return new WaitForSeconds(absoluteMaxTime);
            // TODO hide score

            // clean up
            foreach (GameObject button in buttons)
            {
                Destroy(button);
            }
            foreach (GameObject placeholder in placeholders)
            {
                Destroy(placeholder);
            }
            AudioManager.instance.StopMusic();
            yield return null;
        }
    }
}
