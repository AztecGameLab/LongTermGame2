using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTest : MonoBehaviour
{
    public GameObject Fight;
    public GameObject RoundOne;
    public GameObject RoundTwo;
    public GameObject Fatality;

    public Image healthBar;
    public Image enemyHealthBar;
    public GameObject displayBox;
    public GameObject enemyBox;
    public Animator anim;
    public Animator enemyAnim;
    public static string[] buttonOptions = new string[] { "Right", "Left", "Down" , "Up"};
    public float period = 0.1f;
    public float startHealth;
    public float enemyHealth;
    public float enemyStartHealth;
    public float playerHealth;
    public int damageAmount;

    private KeyCombo c = new KeyCombo(buttonOptions);
    private int winState;
    private int loseState;
    private int enemyReturnValue;
    private int randomAnim;
    // private int defended;
    private float nextActionTime = 0.0f;
    private bool gameStart;
    private int level;
    [SerializeField] AudioClip PlayerAttack, OnionEnemyDamage;
    private void Start()
    {
        gameStart = false;
        level = 1;
        StartCoroutine(roundCounter());
        

        playerHealth = 100;
        enemyHealth = enemyStartHealth;

        for (int t = 0; t < buttonOptions.Length; t++)
        {
            string tmp = buttonOptions[t];
            int r = Random.Range(t, buttonOptions.Length);
            buttonOptions[t] = buttonOptions[r];
            buttonOptions[r] = tmp;

        }

        for (int i = 0; i < buttonOptions.Length; i++)
        {
            displayBox.GetComponent<Text>().text += buttonOptions[i] + " ";
        }
        winState = 0;
    }

    void Update()
    {

        if (gameStart == true)
        {
            if (c.Check())
            {

                Debug.Log("PUNCH");
                StartCoroutine(succesfullAttack());

                for (int t = 0; t < buttonOptions.Length; t++)
                {
                    string tmp = buttonOptions[t];
                    int r = Random.Range(t, buttonOptions.Length);
                    buttonOptions[t] = buttonOptions[r];
                    buttonOptions[r] = tmp;
                }

                displayBox.GetComponent<Text>().text = string.Empty;

                for (int i = 0; i < buttonOptions.Length; i++)
                {
                    displayBox.GetComponent<Text>().text += buttonOptions[i] + " ";
                }
            }

            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                //defended = 1;
                //generateEnemyButton();
                StartCoroutine(enemyAttack());
            }

            if (playerHealth <= 0 && loseState == 0)
            {
                //some death state
                MinigameManager.FinishMinigame(false);
                loseState = 1;
                Debug.Log("lost");
                //restart
            }
            if (enemyHealth <= 0 && winState == 0)
            {
                //win state
                winState = 1;
                level = 2;
                StartCoroutine(roundCounter());
                if(enemyHealth <= 0 && level == 2)
                {
                    MinigameManager.FinishMinigame(true);
                }
                Debug.Log("win");
            }
        }
    }
    IEnumerator succesfullAttack()
    {
        
        randomAnim = Random.Range(0, 3);
        dealDamage(10);

        AudioManager.instance.PlaySFX(PlayerAttack, 0.65f);

        if (randomAnim == 0)
        {
            anim.SetBool("attack1", true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("attack1", false);
        }
        if (randomAnim == 1)
        {
            anim.SetBool("attack2", true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("attack2", false);
        }
        if (randomAnim == 2)
        {
            anim.SetBool("attack3", true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("attack3", false);
        }
    }

    IEnumerator enemyAttack()
    {
        if (gameStart == true)
        {

            int qteGen = Random.Range(0, 2);

            if (qteGen == 0)
            {
                enemyAnim.SetBool("attack1", true);
                yield return new WaitForSeconds(1);
                enemyAnim.SetBool("attack1", false);
                takeDamage(10);
            }
            if (qteGen == 1)
            {
                enemyAnim.SetBool("attack2", true);
                yield return new WaitForSeconds(1);
                enemyAnim.SetBool("attack2", false);
                takeDamage(10);
            }

            yield return new WaitForSeconds(1);

        }
    }
    IEnumerator enemyHurt()
    {
        enemyAnim.SetBool("Hurt", true);
        yield return new WaitForSeconds(.5f);
        enemyAnim.SetBool("Hurt", false);

        AudioManager.instance.PlaySFX(OnionEnemyDamage, 1.0f);

    }

    public int generateEnemyButton()
    {
        string enemyCharacter = "";
        int qteGen = Random.Range(0, 3);


        return -999; //temporary to stop error feel free to change it but it needs to return something. -Kain

    }

   
    public void dealDamage(float amount)
    {
        StartCoroutine(enemyHurt());
        enemyHealth -= amount;
        enemyHealthBar.fillAmount = enemyHealth / startHealth;
    }

    public void takeDamage(float amount)
    {
        playerHealth -= amount;
        healthBar.fillAmount = playerHealth / startHealth;
    }

    IEnumerator roundCounter()
    {
        if(level == 1)
        {
            RoundOne.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            RoundOne.gameObject.SetActive(false);
            Fight.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            gameStart = true;
            Fight.gameObject.SetActive(false);
        }
        else if(level == 2)
        {
            gameStart = false;
            RoundTwo.gameObject.SetActive(true);
            enemyHealth = 140;
            playerHealth = 100;
            healthBar.fillAmount = playerHealth / startHealth;
            enemyHealthBar.fillAmount = enemyHealth / startHealth;     
            yield return new WaitForSeconds(3f);
            RoundTwo.gameObject.SetActive(false);
            Fight.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            gameStart = true;
            Fight.gameObject.SetActive(false);
        }

    }
    IEnumerator randomDelay()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
