using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sniper
{
    public class Score : MonoBehaviour
    {
        public Text score;
        public GameController gc;
        
        void Start()
        {

        }
        
        void Update()
        {
            if (gc.gameOn)
            {
                score.text = (Time.timeSinceLevelLoad + (gc.timePenalty * gc.goodiesKilled)).ToString();
            }
        }
    }
}
