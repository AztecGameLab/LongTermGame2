using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sniper
{
    public class Terrorist : MonoBehaviour
    {
        public float speed;
        Rigidbody2D rb;
        GameController gc;
        
        void Start()
        {
            gc = FindObjectOfType<GameController>();
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;
        }
        
        void Update()
        {
            
        }

        private void OnMouseDown()
        {
            Die();
        }
        public void Die()
        {
            gc.KillBaddie();
            Destroy(gameObject);
        }
    }
}
