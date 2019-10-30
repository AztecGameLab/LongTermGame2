using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MinigameManager : MonoBehaviour
{
    private static MinigameManager _instance;
    public float difficulty = 0;
    public float cutsceneIndex = 0;
    public static MinigameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var instanceGm = new GameObject("Minigame Manager");
                _instance=instanceGm.AddComponent<MinigameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    public void FinishMinigame(bool win)
    {
        if (win)
        {
            difficulty++;
            SceneManager.LoadScene(1);
        }
        else if (difficulty > 0)
        {
            difficulty--;
            RandomlyLoadMinigame();
        }
        
    } 
    public void EndCutscene()
    {
        cutsceneIndex++;
        RandomlyLoadMinigame();
    }
    public void MenuStart()
    {
        RandomlyLoadMinigame();
    }
    private void RandomlyLoadMinigame()
    {
        SceneManager.LoadScene((int)Random.Range(2, SceneManager.sceneCount));
    }
}
