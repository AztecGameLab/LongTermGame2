using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown : MonoBehaviour
{
    bool donezo;
    float timeLeft;
    public TextMeshProUGUI timer;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 30;
        timer.text = "00:" + timeLeft;
        donezo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft >= 10)
            {
                timer.text = "00:" + ((int)timeLeft);
            }
            else
            {
                timer.text = "00:0" + ((int)timeLeft);
            }

        } else {
            if (!donezo){
            // Time.timeScale = 0;
            MinigameManager.FinishMinigame(true);

            donezo = true;
            }
            
        }
    }
}
