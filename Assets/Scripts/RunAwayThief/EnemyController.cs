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

        [SerializeField] AudioClip FootSteps;

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
            AudioManager.instance.PlaySFX(FootSteps, 0.7f);
            AudioManager.instance.SetSFXPitch(0.8f, 1.0f);
        }
    }
}
