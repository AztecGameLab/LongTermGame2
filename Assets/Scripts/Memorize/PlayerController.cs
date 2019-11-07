using UnityEngine;
using UnityEngine.UI;

namespace Memorize {
    public class PlayerController : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField]
        GameObject buttonPrefab;
        [SerializeField]
        float absoluteMaxTime, deltaMaxTime;
        #pragma warning restore 0649

        GameObject[] buttons;
        Slider slider;
        Text memorizeText, repeatText;
        float maxTime;

        void Start()
        {
            slider = GetComponentInChildren<Slider>();
            Text[] texts = GetComponentsInChildren<Text>();
            memorizeText = texts[0];
            repeatText = texts[1];
            slider.gameObject.SetActive(memorizeText.enabled = repeatText.enabled = false);
            maxTime = absoluteMaxTime; // TODO pass in or globalize somehow instead

            GameLoop(); // TODO coroutine
        }

        void FixedUpdate()
        {

        }

        float RandomDirection()
        {
            float r = Random.value;
            return r < 0.5f ? (r < 0.25f ? 0f : 90f) : (r < 0.75f ? 180f : 270f);
        }

        void NewSet()
        {
            buttons = new GameObject[Mathf.RoundToInt(Random.value * 3f) + 3];
            float j = -buttons.Length - 1;
            for (int i = 0; i < buttons.Length; i++)
            {
                j += 2;
                buttons[i] = Instantiate(buttonPrefab, new Vector3(j, 0f, 0f), Quaternion.Euler(0f, 0f, RandomDirection()));
            }
        }

        void HideSet()
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }
        }
        
        void ShowSet()
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(true);
            }
        }

        void EndSet()
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button);
            }
        }

        void GameLoop()
        {
            slider.maxValue = maxTime;
            slider.value = slider.minValue;
            NewSet();
            slider.gameObject.SetActive(true);
            memorizeText.enabled = true;
            // TODO memorize: start increasing slider time
            HideSet();
            memorizeText.enabled = false;
            slider.value = slider.maxValue = absoluteMaxTime;
            repeatText.enabled = true;
            // TODO repeat: start decreasing slider time and receive input
            slider.gameObject.SetActive(false);
            repeatText.enabled = false;
            ShowSet();
            // TODO score: show user input for comparison and use sum of remaining time as bonus and another score for completion, display for absoluteMaxTime secs
            EndSet();
            maxTime -= maxTime > deltaMaxTime ? deltaMaxTime : 0;
        }
    }
}
