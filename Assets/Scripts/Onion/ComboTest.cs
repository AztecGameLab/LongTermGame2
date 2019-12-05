using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTest : MonoBehaviour
{
    public GameObject Fight;
    public GameObject RoundOne;
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
    [SerializeField] AudioClip PlayerAttack, OnionEnemyDamage, OnionPlayerHurt;
    private void Start()
    {
        float difficulty = MinigameManager.GetDifficulty();


        gameStart = false;
        StartCoroutine(roundCounter());

        AudioManager.instance.SetSFXPitch(0.8f, 1.0f);
        
        playerHealth = 100;

        if(difficulty < .25f)
        {
            enemyStartHealth = 60;
            enemyHealth = enemyStartHealth;

        }
        else if (difficulty >= .25f && difficulty < .50)
        {
            enemyStartHealth = 70;
            enemyHealth = enemyStartHealth;
        }
        else if (difficulty >= .50f &&  difficulty < .75)
        {
            enemyStartHealth = 80;
            enemyHealth = enemyStartHealth;
        }
        else if (difficulty >= .75f)
        {
            enemyStartHealth = 100;
            enemyHealth = enemyStartHealth;
        }

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
             
                StartCoroutine(enemyAttack());
            }

            if (playerHealth <= 0 && loseState == 0)
            {
                loseState = 1;
                //some death state


                StartCoroutine(endGame());
                
                //restart
            }
            if (enemyHealth <= 0 && winState == 0)
            {
                //win state
                winState = 1;

                StartCoroutine(endGame());
               
            }
        }
    }
    IEnumerator succesfullAttack()
    {
        
        randomAnim = Random.Range(0, 3);
        dealDamage(10);

        AudioManager.instance.PlaySFX(PlayerAttack, 0.65f);
        AudioManager.instance.PlaySFX(OnionEnemyDamage, 1.0f);

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
    }
    public void dealDamage(float amount)
    {
        StartCoroutine(enemyHurt());
        enemyHealth -= amount;
        enemyHealthBar.fillAmount = enemyHealth / enemyStartHealth;
    }

    public void takeDamage(float amount)
    {
        playerHealth -= amount;
        healthBar.fillAmount = playerHealth / startHealth;
        AudioManager.instance.PlaySFX(OnionPlayerHurt, 1.0f);
    }

    IEnumerator roundCounter()
    {
        
            RoundOne.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            RoundOne.gameObject.SetActive(false);
            Fight.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            gameStart = true;
            Fight.gameObject.SetActive(false);
        
    }
    IEnumerator endGame()
    {
        if (loseState == 1)
        {
            anim.enabled = false;  

            Fatality.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            MinigameManager.FinishMinigame(false);
        }
        else if (winState == 1)
        {
            //win state
            enemyAnim.enabled = false;

            Fatality.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            MinigameManager.FinishMinigame(true);
            
        }
    }
}
