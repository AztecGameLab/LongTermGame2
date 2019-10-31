using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootToMove
{
    public class Lava : MonoBehaviour
    {
        public float scrollSpeed;
        public float speedIncrease;

        Rigidbody2D rb;
        
        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {

        }

        private void FixedUpdate()
        {
            transform.position = transform.position + (new Vector3(scrollSpeed, 0, 0) * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Shooty")
            {
                Destroy(collision.gameObject);
                SceneManager.LoadScene("ShootToMove");
            }
        }

        public void IncreaseSpeed()
        {
            scrollSpeed += speedIncrease;
        }
    }
}
