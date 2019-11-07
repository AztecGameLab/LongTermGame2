using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunAwayThief
{
    public class EnemyController : MonoBehaviour
    {
        public Vector2 moveForward;
        public float increaseSpeed;
        public BoxCollider2D playerBox;

        void Start()
        {
            playerBox = GetComponent<BoxCollider2D>();
        }
        
        void Update()
        {
            
        }

        public void EnemyRun(float difficultyModifier)
        {
            this.GetComponent<Rigidbody2D>().velocity = moveForward * difficultyModifier;
            this.GetComponent<Rigidbody2D>().AddForce(moveForward * increaseSpeed);
        }
    }
}
