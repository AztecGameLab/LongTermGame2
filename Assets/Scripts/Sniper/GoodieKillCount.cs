using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Sniper
{

    public class GoodieKillCount : MonoBehaviour
    {
        GameController gc;
        public Text count;
        // Start is called before the first frame update
        void Start()
        {
            gc = FindObjectOfType<GameController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(gc.goodiesKilled > 0)
            {
                count.text = "Good, upstanding citizens murdered: " + gc.goodiesKilled.ToString();
            }
        }
    }
}
