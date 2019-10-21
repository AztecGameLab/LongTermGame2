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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (gc.gameOn)
            {
                score.text = (Time.timeSinceLevelLoad + (gc.timePenalty * gc.goodiesKilled)).ToString();
            }
        }
    }
}
