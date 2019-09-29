using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherHSpawner : MonoBehaviour
{
    public List<GameObject> Formations;
    public float timeBetweenSpawns;
    float lastSpawn;
    int totalSpawns;
    // Start is called before the first frame update
    void Start()
    {
        lastSpawn = Time.time;
        totalSpawns = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>= lastSpawn + timeBetweenSpawns)
        {
            lastSpawn = Time.time;
            Instantiate(Formations[Random.Range(0, Formations.Count - 1)], gameObject.transform.position, Quaternion.identity);
            totalSpawns++;
            if (totalSpawns != 0 && totalSpawns % 10 == 0)
            {
                gameObject.transform.parent.GetComponent<ArcherHLava>().IncreaseSpeed();
            }
        }
        
    }
}
