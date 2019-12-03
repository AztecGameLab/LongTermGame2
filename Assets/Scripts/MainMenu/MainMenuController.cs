using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    void Start()
    {
       
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PlayTest()
    {
        print("Play Button Pressed");
        MinigameManager.FinishMinigame();
    }

    public void showCredits()
    {
        print("People made this");
    }
}
