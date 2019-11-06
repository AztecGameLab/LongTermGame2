using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Byron;

namespace RunAwayThief
{
    public class EnemyController : MonoBehaviour
    {
        public Vector2 moveForward;
        public float increaseSpeed;
        public BoxCollider2D playerBox;

        [SerializeField] AudioClip FootSteps;

        void Start()
        {
            playerBox = GetComponent<BoxCollider2D>();
        }
        
        void Update()
        {
            
        }

        public void EnemyRun()
        {
            this.GetComponent<Rigidbody2D>().velocity = moveForward;
            this.GetComponent<Rigidbody2D>().AddForce(moveForward * increaseSpeed);
            AudioManager.instance.PlaySFX(FootSteps);
        }
    }
}
