﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShootToMove
{
    public class Target : MonoBehaviour
    {
        public int value;
        Text score;
        
        void Start()
        {
            score = GameObject.Find("Score").GetComponent<Text>();
        }
        
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Bullet(Clone)")
            {
                score.GetComponent<Score>().AddToScore(value);
                Destroy(gameObject);
            }
        }
    }
}
