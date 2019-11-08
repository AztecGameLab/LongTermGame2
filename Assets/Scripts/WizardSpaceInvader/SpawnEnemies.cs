using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject troll;
    int count = 0;
    float timer = 0.0f;
    float speedIncrement = 0.0f;
    int trollCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //first troll is spawned in the middle of the screen
        SpawnEnemy(0);
    }

    private void Update()
    {
        float randX;
        float randDelay;

        //consequent trolls are spawned in intervals of 1-3 seconds
        randX = Random.Range(-8f, 8f);
        randDelay = Random.Range(1.0f, 4.5f);

        timer += Time.deltaTime;
        if (timer > randDelay)
        {
            SpawnEnemy(randX);
            trollCount++;
            timer = 0.0f;
        }
    }

    //spawns enemy trolls to the scene
    private void SpawnEnemy(float x)
    {
        GameObject currTroll = Instantiate(troll, new Vector2(x, 6), Quaternion.identity);

        if (trollCount >= 3)
        {
            currTroll.GetComponent<TrollMovement>().speed += speedIncrement;
            count = 0;
        }
    }
   
}
