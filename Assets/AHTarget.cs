using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AHTarget : MonoBehaviour
{
    public int value;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ArcherH Bullet(Clone)")
        {
            Text score = GameObject.Find("Score").GetComponent<Text>();
            score.GetComponent<AHScore>().AddToScore(value);
            Destroy(gameObject);
        }
    }
}
