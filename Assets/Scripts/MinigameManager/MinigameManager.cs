﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class MinigameManager : MonoBehaviour
{
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
            if (_instance)
                return _instance;
            else
            {

                var ManagerGM = new GameObject("Minigame Manager");
                return _instance = ManagerGM.AddComponent<MinigameManager>();
            }
        }
    }

    void Awake()
    {
        if (_instance && _instance != this)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
            RandomlyGenerateMinigameSequence();
        }
    }

    public static void FinishMinigame(bool win)
    {
        if (!_instance)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            return;
        }
        if (win)
        {
      
            difficulty += 0.5f;
            difficulty = Mathf.Clamp01(difficulty);
            Instance.nextScene();
        }
        else if (difficulty > 0)
        {
            difficulty-=0.05f;
            difficulty = Mathf.Clamp01(difficulty);
            Instance.nextScene();
        }
        
    }
    public static void FinishMinigame()
    {
        if (!_instance)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            return;
        }
            Instance.nextScene();
        
    }
    public static void FinishCutscene()
    {
        if (!_instance)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            return;
        }
        Instance.nextScene();
    }
    public void MenuStart()
    {
        
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
            EditorSceneManager.OpenScene(SceneManager.GetSceneAt(minigameIndices[minigameIndex++]).name);
    }
    private void LoadNextCutscene()
    {
        if (cutsceneIndex == cutsceneIndices.Length)
            LoadNextMinigame();
        SceneManager.LoadScene(cutsceneIndices[cutsceneIndex++]);
    }
    public void nextScene()
    {
        if (frequency.Length == frequencyIndex)
        {
            RestartGame();
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
        minigameIndices = Shuffle(minigameIndices);
    }
    
    int[] Shuffle(int[] array)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int p = array.Length;
        var tmp = new List<int> (array);
        for (int n = 0; n < p; n++)
        {
            int r = Random.Range(0, tmp.Count);
            array[n] = tmp[r];
            tmp.RemoveAt(r);
        }
        return array;
    }
    public void RestartGame()
    {
        cutsceneIndex = 0;
        frequencyIndex = 0;
        minigameIndex = 0;
        RandomlyGenerateMinigameSequence();
        SceneManager.LoadScene(0);
    }
}
