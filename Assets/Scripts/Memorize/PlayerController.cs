﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Memorize {
    public class PlayerController : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField]
        GameObject buttonPrefab;
        [SerializeField]
        float absoluteMaxTime, deltaMaxTime, waitTime;
        // TODO globalize or pass in difficulty
        // difficulty must be less than absoluteMaxTime
        [SerializeField]
        int maxButtons, minButtons, difficulty;
        #pragma warning restore 0649

        GameObject[] buttons;
        Slider slider;
        Text memorizeText, repeatText;
        float maxTime, deltaButtons;

        void Start()
        {
            deltaButtons = maxButtons - minButtons;
            maxButtons += difficulty;
            minButtons += difficulty;
            slider = GetComponentInChildren<Slider>();
            Text[] texts = GetComponentsInChildren<Text>();
            memorizeText = texts[0];
            repeatText = texts[1];
            slider.gameObject.SetActive(memorizeText.enabled = repeatText.enabled = false);
            maxTime = absoluteMaxTime - difficulty;

            StartCoroutine(GameLoop());
        }

        float RandomDirection()
        {
            float r = Random.value;
            return r < 0.5f ? (r < 0.25f ? 0f : 90f) : (r < 0.75f ? 180f : 270f);
        }

        void NewSet()
        {
            buttons = new GameObject[Mathf.RoundToInt(Random.value * deltaButtons) + minButtons];
            // TODO fix placement of buttons on screen
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

        IEnumerator GameLoop()
        {
            slider.maxValue = maxTime;
            slider.value = slider.minValue;
            memorizeText.enabled = true;
            yield return new WaitForSeconds(waitTime);
            NewSet();
            slider.gameObject.SetActive(true);
            // memorize
            while (slider.value < slider.maxValue)
            {
                slider.value += Time.deltaTime;
                yield return null;
            }
            slider.gameObject.SetActive(false);
            memorizeText.enabled = false;
            HideSet();
            slider.value = slider.maxValue = absoluteMaxTime;
            yield return new WaitForSeconds(waitTime);
            repeatText.enabled = true;
            slider.gameObject.SetActive(true);
            // repeat
            while (slider.value > slider.minValue)
            {
                // TODO receive input, show it
                slider.value -= Time.deltaTime;
                yield return null;
            }
            slider.gameObject.SetActive(false);
            repeatText.enabled = false;
            ShowSet();
            // TODO show score = (remaining input time + completion) * difficulty
            yield return new WaitForSeconds(absoluteMaxTime);
            // TODO hide user input and score
            EndSet();
            yield return null;
        }
    }
}
