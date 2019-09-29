using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AHScore : MonoBehaviour
{
    Text scoreText;
    int score;
    public Transform reference;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
        score = (int)Math.Round(reference.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)Math.Round(reference.position.x);
        scoreText.text = "Score: " + score.ToString();
    }
}
