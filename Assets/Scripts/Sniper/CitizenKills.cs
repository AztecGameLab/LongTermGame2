using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sniper
{
    public class CitizenKills : MonoBehaviour
    {
        public Text count;

        GameController gc;

        void Start()
        {
            gc = FindObjectOfType<GameController>();
        }
        
        void Update()
        {
            if(gc.goodiesKilled > 0)
            {
                count.text = "Citizens murdered: " + gc.goodiesKilled.ToString();
            }
        }
    }
}
