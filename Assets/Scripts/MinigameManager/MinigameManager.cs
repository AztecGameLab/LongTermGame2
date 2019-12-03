using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    static GameObject gameManager;
    public GameObject canvas;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject[] hearts;
    int health;
    int score;
    public TextMeshProUGUI scoreText;

    private static MinigameManager _instance;
    public static float difficulty = 0;
    public static int cutsceneIndex = 0;
    public static int minigameIndex = 0;
    public static int frequencyIndex = 0;
    public int[] frequency;
    public int[] cutsceneIndices;
    public int[] minigameIndices;

    public static bool hasFinishedGame;
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
        gameManager = GameObject.FindGameObjectWithTag("Manager");

        if (_instance && _instance != this)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
            RandomlyGenerateMinigameSequence();
        }
    }

    private void Start()
    {
        health = hearts.Length;
        score = 0;
        scoreText.SetText("Score: " + score);
    }

    public static void FinishMinigame(bool win)
    {
        if (hasFinishedGame)
            return;
        else
            hasFinishedGame = true;

        if (!_instance)
        {
                UnityEditor.EditorApplication.isPlaying = false;
                hasFinishedGame = false;
            
            return;
        }
        if (win)
        {
      
            difficulty += 0.2f;
            difficulty = Mathf.Clamp01(difficulty);
            if (gameManager != null)
                gameManager.GetComponent<MinigameManager>().resultScreen(true);
            //Instance.nextScene();
            //hasFinishedGame = false;
        }
        else
        {
            if (difficulty > 0)
            {
                difficulty -= 0.05f;
                difficulty = Mathf.Clamp01(difficulty);
            }

            if (gameManager != null)
                gameManager.GetComponent<MinigameManager>().resultScreen(false);
            //Instance.nextScene();
            //hasFinishedGame = false;
        }

    }

    public static void FinishMinigame()
    {
        if (!_instance)
        {
                UnityEditor.EditorApplication.isPlaying = false;
            
            return;
        }
        if (gameManager != null)
        {
            gameManager.GetComponent<MinigameManager>().resultScreen();
        }
        //Instance.nextScene();

    }
    public static void FinishCutscene()
    {
        if (!_instance)
        {
                UnityEditor.EditorApplication.isPlaying = false;
            
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
        gameManager = GameObject.FindGameObjectWithTag("Manager");

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(true);
        }
        health = hearts.Length;
        score = 0;
        scoreText.SetText("Score: " + score);

        cutsceneIndex = 0;
        frequencyIndex = 0;
        minigameIndex = 0;
        RandomlyGenerateMinigameSequence();
        SceneManager.LoadScene(0);
    }

    public bool CRrunning;
    Coroutine lastRoutine;
    void resultScreen()
    {
        if (!CRrunning)
        {
            StartCoroutine(resultScreenCR());
        }
    }
    IEnumerator resultScreenCR()
    {
        CRrunning = true;

        yield return new WaitForSeconds(0.5f);

        Instance.nextScene();

        CRrunning = false;
    }

    void resultScreen(bool win)
    {
        if (!CRrunning)
        {
            lastRoutine = StartCoroutine(resultScreenCR(win));
        }
    }
    IEnumerator resultScreenCR(bool win)
    {

        CRrunning = true;
        yield return new WaitForSeconds(0.2f); //time for result screen to show

        canvas.SetActive(true);
        if (win)
        {
            score++;
            scoreText.SetText("Score: " + score);
            winScreen.SetActive(true);
        }
        else
        {
            scoreText.SetText("Score: " + score);
            loseScreen.SetActive(true);

            health--;
            yield return new WaitForSeconds(0.3f);
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.1f);
                hearts[health].SetActive(true);
                yield return new WaitForSeconds(0.1f);
                hearts[health].SetActive(false);
            }

            if (health <= 0)
            {
                yield return new WaitForSeconds(0.6f);
                gameOverScreen.SetActive(true);
                yield return new WaitForSeconds(5f);
                gameOverScreen.SetActive(false);
                RestartGame();
                canvas.SetActive(false);
                hasFinishedGame = false;
                CRrunning = false;
                StopCoroutine(lastRoutine);
            }
        }
        yield return new WaitForSeconds(3); //time to move to next level

        if (win)
            winScreen.SetActive(false);
        else
            loseScreen.SetActive(false);
        canvas.SetActive(false);

        Instance.nextScene();
        hasFinishedGame = false;

        CRrunning = false;
    }

}
