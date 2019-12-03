using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown : MonoBehaviour
{
    float timeLeft;
    public TextMeshProUGUI timer;
    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 30;
        timer.text = "00:" + timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft >= 10)
        {
            timer.text = "00:" + ((int)timeLeft);
        }
        else if (timeLeft > 0)
        {
            timer.text = "00:0" + ((int)timeLeft);
        }
        else
        {
            print("You Win! :)");
            Time.timeScale = 0;
        }
    }
}
