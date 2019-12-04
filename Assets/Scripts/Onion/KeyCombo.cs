using UnityEngine;
using UnityEngine.UI;

public class KeyCombo
{
    public GameObject displayBox;
    public string[] buttons;
    public float allowedTimeBetweenButtons = 0.22f; //tweak as needed

    private float timeLastButtonPressed;
    private int currentIndex = 0; //moves along the array as buttons are pressed

    public KeyCombo(string[] b)
    {
        buttons = b;
    }

    //usage: call this once a frame. when the combo has been completed, it will return true
    public bool Check()
    {
        if (Time.time > timeLastButtonPressed + allowedTimeBetweenButtons) currentIndex = 0;
        {
            if (currentIndex < buttons.Length)
            {
                if ((buttons[currentIndex] == "Down" && Input.GetAxisRaw("Vertical") == -1) ||
                (buttons[currentIndex] == "Up" && Input.GetAxisRaw("Vertical") == 1) ||
                (buttons[currentIndex] == "Left" && Input.GetAxisRaw("Horizontal") == -1) ||
                (buttons[currentIndex] == "Right" && Input.GetAxisRaw("Horizontal") == 1) ||
                (buttons[currentIndex] != "Down" && buttons[currentIndex] != "Up" && buttons[currentIndex] != "Left" && buttons[currentIndex] != "Right" && Input.GetButtonDown(buttons[currentIndex])))
                {
                    timeLastButtonPressed = Time.time;
                    currentIndex++;
                }

                if (currentIndex >= buttons.Length)
                {
                    currentIndex = 0;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }
}
