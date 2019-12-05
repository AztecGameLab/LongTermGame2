using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject troll;
    float speedIncrement = 0.0f;
    int trollCount = 0;
    float delay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayedSpawn(0));
    }

    //spawns enemy trolls to the scene
    private void SpawnEnemy(float x)
    {

        if (trollCount > 2)
        {
            speedIncrement += 0.5f;
            trollCount = 0;
            if (delay > 0.25f)
            {
                delay -= 0.10f;
            }
        }

        GameObject currTroll = Instantiate(troll, new Vector2(x, 6), Quaternion.identity);
        currTroll.GetComponent<TrollMovement>().speed += speedIncrement;
    }

    IEnumerator delayedSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        float randX = Random.Range(-8f, 8f);
        trollCount++;
        SpawnEnemy(randX);
        StartCoroutine(delayedSpawn(delay));
    }
   
}
