using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletesItself : MonoBehaviour
{
    float startTime;
    float timeAlive;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        timeAlive = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > startTime + timeAlive)
        {
            Destroy(gameObject);
        }
    }
}
