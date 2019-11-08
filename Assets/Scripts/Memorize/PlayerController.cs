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
        // TODO globalize or pass in difficulty
        [SerializeField]
        ushort maxButtons, minButtons, difficulty;
        #pragma warning restore 0649

        GameObject[] buttons, placeholders;
        Slider slider;
        Text memorizeText, repeatText;
        float maxTime, deltaButtons;
        bool inputAllowed;

        void Start()
        {
            slider = GetComponentInChildren<Slider>();
            Text[] texts = GetComponentsInChildren<Text>();
            memorizeText = texts[0];
            repeatText = texts[1];
            slider.gameObject.SetActive(memorizeText.enabled = repeatText.enabled = false);
            inputAllowed = false;
            deltaButtons = maxButtons - minButtons;
            // TODO change how difficulty affects these vars
            maxButtons += difficulty;
            minButtons += difficulty;
            maxTime = absoluteMaxTime - difficulty;
            StartCoroutine(GameLoop());
        }

        void FixedUpdate()
        {
            if (inputAllowed)
            {
                // TODO receive input, show dots for number entered, change to green = correct
            }
        }

        float RandomDirection()
        {
            float r = Random.value;
            return r < 0.5f ? (r < 0.25f ? 0f : 90f) : (r < 0.75f ? 180f : 270f);
        }

        IEnumerator GameLoop()
        {
            slider.maxValue = maxTime;
            slider.value = slider.minValue;
            memorizeText.enabled = true;
            yield return new WaitForSeconds(waitTime);

            // generate new set of buttons and placeholders
            placeholders = buttons = new GameObject[Mathf.RoundToInt(Random.value * deltaButtons) + minButtons];
            // TODO fix size/placement of buttons on screen with vars
            float j = -buttons.Length - 1;
            for (ushort i = 0; i < buttons.Length; i++)
            {
                j += 2;
                float dir =  RandomDirection();
                placeholders[i] = Instantiate(placeholderPrefab, new Vector3(j, -1.5f, 0f), Quaternion.Euler(0f, 0f, 0f));
                placeholders[i].SetActive(false);
                buttons[i] = Instantiate(buttonPrefab, new Vector3(j, 0f, 0f), Quaternion.Euler(0f, 0f, dir));
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
                yield return null; // allows to receive input from FixedUpdate()
            }
            inputAllowed = false;
            slider.gameObject.SetActive(false);
            repeatText.enabled = false;

            // show buttons
            foreach (GameObject button in buttons)
            {
                button.SetActive(true);
            }
            // TODO create score
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
            yield return null;
        }
    }
}
