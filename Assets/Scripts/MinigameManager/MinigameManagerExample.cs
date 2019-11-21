using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManagerExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //win state
            MinigameManager.FinishMinigame(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //lose state
            MinigameManager.FinishMinigame(false);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //draw state
            MinigameManager.FinishMinigame();
        }
    }
}
