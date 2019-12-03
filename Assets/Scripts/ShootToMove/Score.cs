using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace ShootToMove
{
    public class Score : MonoBehaviour
    {
        public int score;
        public Transform reference;

        Text scoreText;
        int bonusScore;

        void Start()
        {
            scoreText = gameObject.GetComponent<Text>();
            score = (int)Math.Round(reference.position.x);
        }

        void Update()
        {
            score = (int)Math.Round(reference.position.x) + bonusScore;
            scoreText.text = "Score: " + score.ToString();
        }

        public void AddToScore(int addition)
        {
            bonusScore += addition;
        }
    }
}
