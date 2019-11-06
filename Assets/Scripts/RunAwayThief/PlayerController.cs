using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Byron;

namespace RunAwayThief
{
    public class PlayerController : MonoBehaviour
    {
        public Vector2 moveForward;
        public float increaseSpeed;
        public GameController gameController;

        Rigidbody2D body;

        [SerializeField] AudioSource SFXSource;
        [SerializeField] AudioClip FootSteps;

        void Start()
        {
            body = GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {
            if(!gameController.end)
            {
                PlayerRun();
            }
        }

        public void PlayerRun()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                body.velocity = moveForward;
                body.AddForce(moveForward * increaseSpeed);
                AudioManager.instance.PlaySFX(FootSteps);
            }
        }
    }
}
