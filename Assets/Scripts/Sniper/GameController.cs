using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject baddie;
    public int numBadGuys;
    public GameObject goodie;
    public int numGoodGuys;
    // Start is called before the first frame update
    void Start()
    {
        float xLower = -8f;
        float xUpper = 8f;
        float yLower = -3;
        float yUpper = 3;
        for (int i = 0; i <numBadGuys; i++)
        {
            Instantiate(baddie, new Vector2(Random.Range(xLower, xUpper), Random.Range(yLower, yUpper)), Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
