using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sniper
{
    public class CitizenKills : MonoBehaviour
    {
        GameController gc;
        public Text count;

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
