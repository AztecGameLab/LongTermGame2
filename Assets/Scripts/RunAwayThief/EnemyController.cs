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

        #pragma warning disable 0649
        [SerializeField] AudioClip FootSteps;
        #pragma warning restore 0649

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
            AudioManager.instance.PlaySFX(FootSteps, 0.7f);
            AudioManager.instance.SetSFXPitch(0.8f, 1.0f);
        }
    }
}
