using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunAwayThief
{
    public class PlayerController : MonoBehaviour
    {
        public Vector2 moveForward;
        public float increaseSpeed;
        public GameController gameController;

        Rigidbody2D body;

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
            }
        }
    }
}
