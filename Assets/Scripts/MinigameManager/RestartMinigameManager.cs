using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartMinigameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restart()
    {
        MinigameManager.Instance.nextScene();
    }
}
