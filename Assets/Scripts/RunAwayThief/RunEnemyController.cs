using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunAwayThief
{
    public class RunEnemyController : MonoBehaviour
    {
        public Vector2 moveForward;
        public float increaseSpeed;
        public BoxCollider2D playerBox;
        void Start()
        {
            playerBox = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void EnemyRun()
        {
            this.GetComponent<Rigidbody2D>().velocity = moveForward;
            this.GetComponent<Rigidbody2D>().AddForce(moveForward * increaseSpeed);
        }
    }
}