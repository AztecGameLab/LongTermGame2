using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEsystem : MonoBehaviour
{
    public GameObject displayBox;
    public GameObject passBox;
    public GameObject playerHealth;
    public GameObject enemyHealth;
    public int playerHP;
    public int EnemyHp;
    public float timer;
    public float timeAfterEvent;

    private int qteGen;
    private int waitingForKey;
    private int correctKey;
    private int countingDown;

    private void Start()
    {
        playerHP = 100;
        EnemyHp = 100;
    }

    private void Update()
    {
        enemyHealth.GetComponent<Text>().text = "" + EnemyHp;
        playerHealth.GetComponent<Text>().text = "" + playerHP;

        if (waitingForKey == 0)
        {
            qteGen = Random.Range(1, 7);
            countingDown = 1;
            StartCoroutine (CountDown());

            if(qteGen == 1)
            {
                waitingForKey = 1;
                displayBox.GetComponent<Text>().text = "[Q]";
            }
            if (qteGen == 2)
            {
                waitingForKey = 1;
                displayBox.GetComponent<Text>().text = "[W]";
            }
            if (qteGen == 3)
            {
                waitingForKey = 1;
                displayBox.GetComponent<Text>().text = "[E]";
            }
            if (qteGen == 4)
            {
                waitingForKey = 1;
                displayBox.GetComponent<Text>().text = "[A]";
            }
            if (qteGen == 5)
            {
                waitingForKey = 1;
                displayBox.GetComponent<Text>().text = "[S]";
            }
            if (qteGen == 6)
            {
                waitingForKey = 1;
                displayBox.GetComponent<Text>().text = "[D]";
            }
        }

        if (qteGen == 1)
        {
            if (Input.anyKeyDown)
            {
                if(Input.GetButtonDown("qKey"))
                {
                    correctKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (qteGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("wKey"))
                {
                    correctKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (qteGen == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("eKey"))
                {
                    correctKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (qteGen == 4)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("aKey"))
                {
                    correctKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (qteGen == 5)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("sKey"))
                {
                    correctKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (qteGen == 6)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("dKey"))
                {
                    correctKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    correctKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
    }

    IEnumerator KeyPressing()
    {
        if(correctKey == 1)
        {
            countingDown = 2;
            passBox.GetComponent<Text>().text = "pass";
            yield return new WaitForSeconds (timeAfterEvent);
            correctKey = 0;
            passBox.GetComponent<Text>().text = "";
            displayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(timeAfterEvent);
            enemyHealth.GetComponent<Text>().text = "" + EnemyHp;
            EnemyHp -= 10;
            waitingForKey = 0;
            countingDown = 1;
        }
        if (correctKey == 2)
        {
            countingDown = 2;
            passBox.GetComponent<Text>().text = "fail";
            yield return new WaitForSeconds(timeAfterEvent);
            correctKey = 0;
            passBox.GetComponent<Text>().text = "";
            displayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(timeAfterEvent);
            playerHealth.GetComponent<Text>().text = "" + playerHP;
            playerHP -= 10;
            waitingForKey = 0;
            countingDown = 1;
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(timer);
        if(countingDown == 1)
        {
            qteGen = 4;
            countingDown = 2;
            passBox.GetComponent<Text>().text = "fail";
            yield return new WaitForSeconds(timeAfterEvent);
            correctKey = 0;
            passBox.GetComponent<Text>().text = "";
            displayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(timeAfterEvent);
            waitingForKey = 0;
            countingDown = 1;
        }
    }
}
