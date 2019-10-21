using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sniper
{

    public class UpstandingCitizen : MonoBehaviour
    {
        GameController gc;
        // Start is called before the first frame update
        void Start()
        {
            gc = FindObjectOfType<GameController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(transform.position.x > 20 || transform.position.x < -20 || transform.position.y > 20 || transform.position.y < -20)
            {
                Destroy(gameObject);
            }

        }
        private void OnMouseDown()
        {

            gc.goodiesKilled++;
            Destroy(gameObject);
        }
    }
}
