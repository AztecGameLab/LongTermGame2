using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MinigameManager : MonoBehaviour
{
    System.Random _random = new System.Random();
    private static MinigameManager _instance;
    public static float difficulty = 0;
    public static int cutsceneIndex = 0;
    public static int minigameIndex = 0;
    public static int frequencyIndex = 0;
    public int[] frequency;
    public int[] cutsceneIndices;
    public int[] minigameIndices;
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
        DontDestroyOnLoad(this);
    }
    public void FinishMinigame(bool win)
    {
        if (win)
        {
            difficulty+=0.5f;
            difficulty = Mathf.Clamp01(difficulty);
            nextScene();
        }
        else if (difficulty > 0)
        {
            difficulty-=0.05f;
            difficulty = Mathf.Clamp01(difficulty);
            nextScene();
        }
        
    }
    public void FinishCutscene()
    {
        nextScene();
    }
    public void MenuStart()
    {
        RandomlyGenerateMinigameSequence();
        nextScene();
    }
    private void LoadNextMinigame()
    {
        if (minigameIndex == minigameIndices.Length)
        {
            RandomlyGenerateMinigameSequence();
            minigameIndex = 0;
        }
        if (UnityEditor.EditorApplication.isPlaying)
            SceneManager.LoadScene(minigameIndices[minigameIndex++]);
        else if (!UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.OpenScene(SceneManager.GetSceneAt(minigameIndices[minigameIndex++]).name);
    }
    private void LoadNextCutscene()
    {
        if (cutsceneIndex == cutsceneIndices.Length)
            LoadNextMinigame();
        SceneManager.LoadScene(cutsceneIndices[cutsceneIndex]);
    }
    public void nextScene()
    {
        if (frequency.Length == frequencyIndex)
        {
            Debug.Log("GAME OVER");
        }
        else
        {
            if (frequency[frequencyIndex++] == 0)
                LoadNextMinigame();
            else
                LoadNextCutscene();
        }
    }
    private void RandomlyGenerateMinigameSequence()
    {
        cutsceneIndices = Shuffle(cutsceneIndices);
    }
    
    int[] Shuffle(int[] array)
    {

        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(1, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
        return array;
    }
}
