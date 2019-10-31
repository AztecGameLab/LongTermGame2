using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sniper
{
    public class Citizen : MonoBehaviour
    {
        GameController gc;

        void Start()
        {
            gc = FindObjectOfType<GameController>();
        }
        
        void Update()
        {
            if(transform.position.x > 20 || transform.position.x < -20 || transform.position.y > 20 || transform.position.y < -20)
            {
                Destroy(gameObject);
            }
        }

        private void OnMouseDown()
        {
            Die();
        }
        public void Die()
        {
            gc.goodiesKilled++;
            Destroy(gameObject);
        }
    }
}
