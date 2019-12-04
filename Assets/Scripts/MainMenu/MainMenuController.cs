using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] AudioClip MouseHover;

    [SerializeField] GameObject credits;

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
        if (credits.activeSelf)
            credits.SetActive(false);
        else
            credits.SetActive(true);
    }

    public void OnMouseHover()
    {
        AudioManager.instance.PlaySFX(MouseHover, 1.0f);
    }

}
