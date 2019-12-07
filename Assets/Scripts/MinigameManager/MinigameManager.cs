using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
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
    public static bool difficultyUnclamped;
    public static int cutsceneIndex = 0;
    public static int minigameIndex = 0;
    public static int frequencyIndex = 0;
    public int[] frequency;
    public int[] cutsceneIndices;
    public int[] minigameIndices;

    public static AudioClip WinMusic, LoseMusic;
    public AudioClip winSound, loseSound;

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
        WinMusic = winSound;
        LoseMusic = loseSound;

        if (_instance && _instance != this)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
            RandomlyGenerateMinigameSequence();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightBracket) && Input.GetKeyDown(KeyCode.Z))
            FinishMinigame(true);
        if (Input.GetKeyDown(KeyCode.LeftBracket) && Input.GetKeyDown(KeyCode.Z))
            FinishMinigame(false);

        if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKeyDown(KeyCode.Z))
            SetDifficulty(1);
        if (Input.GetKeyDown(KeyCode.Alpha5) && Input.GetKeyDown(KeyCode.Z))
            SetDifficulty(0.5f);
        if (Input.GetKeyDown(KeyCode.Alpha0) && Input.GetKeyDown(KeyCode.Z))
            SetDifficulty(0);
        if (Input.GetKeyDown(KeyCode.Equals) && Input.GetKeyDown(KeyCode.Z))
        {
            print("GAME MANAGER difficulty Unclamped:" + (difficultyUnclamped = difficultyUnclamped ? false : true));
            if(difficultyUnclamped)
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].GetComponent<Image>().color = Color.black;
                }
            else
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].GetComponent<Image>().color = new Color(200, 189, 189, 255);
                }
        }
        if (Input.GetKeyDown(KeyCode.Backspace) && Input.GetKeyDown(KeyCode.Z))
            SetDifficulty(GetDifficulty() + 0.1f);
    }

    private void Start()
    {
        health = hearts.Length;
        score = 0;
        scoreText.SetText("Score: " + score);
    }

    public static float GetDifficulty()
    {
        if (!_instance)
        {
            return 0.246f;
        }
        return difficulty;
    }

    static void SetDifficulty(float diff)
    {
        difficulty = diff;
        if(!difficultyUnclamped)
            difficulty = Mathf.Clamp01(difficulty);
        print("GAME MANAGER DIFFICULTY: " + difficulty);
    }

    public static void FinishMinigame(bool win)
    {
        print(win ? "GAME MANAGER: WIN" : "GAME MANAGER: LOSE");

        if (hasFinishedGame)
            return;
        else
            hasFinishedGame = true;

        if (!_instance)
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
                hasFinishedGame = false;
            
            return;
        }
        if (win)
        {

            SetDifficulty(difficulty + 0.07f);
            if (gameManager != null)
                gameManager.GetComponent<MinigameManager>().resultScreen(true);
            AudioManager.instance.PlayMusic(WinMusic, 1.0f);
            //Instance.nextScene();
            //hasFinishedGame = false;
        }
        else
        {
            if (difficulty > 0)
            {
                SetDifficulty(difficulty - 0.3f);
            }

            if (gameManager != null)
                gameManager.GetComponent<MinigameManager>().resultScreen(false);
            AudioManager.instance.PlayMusic(LoseMusic, 1.0f);
            //Instance.nextScene();
            //hasFinishedGame = false;
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
        #if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying)
            SceneManager.LoadScene(minigameIndices[minigameIndex++]);
        else if (!UnityEditor.EditorApplication.isPlaying)
            EditorSceneManager.OpenScene(SceneManager.GetSceneAt(minigameIndices[minigameIndex++]).name);
            #endif
    }
    private void LoadNextCutscene()
    {
        if (cutsceneIndex == cutsceneIndices.Length)
            LoadNextMinigame();
        SceneManager.LoadScene(cutsceneIndices[cutsceneIndex++]);
    }
    public void nextScene()
    {
        frequencyIndex++;
        LoadNextMinigame();

        /*
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
        }*/
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
        SetDifficulty(0);

        loseScreen.SetActive(false);
        winScreen.SetActive(false);

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
                yield return new WaitForSeconds(3f);
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
