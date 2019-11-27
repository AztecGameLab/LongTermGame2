using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunAwayThief
{
    public class PlayerController : MonoBehaviour
    {
        public Vector2 moveForward;
        public float increaseSpeed;
        private float distanceToGround;
        public GameController gameController;
        public Collider2D playerCollider;
        private bool isGrounded;
        public GameObject dirtParticle;
        Rigidbody2D body;

        #pragma warning disable 0649

        [SerializeField] AudioClip FootSteps;
#pragma warning restore 0649

        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            playerCollider = GetComponent<Collider2D>();
        }
        
        void Update()
        {
            
        }

        public void PlayerRun()
        {
            if (Input.GetButtonDown("Primary"))
            {
                Vector3 dirtPosition = new Vector3(body.transform.position.x, body.transform.position.y - 1.4f, body.transform.position.z);
                Instantiate(dirtParticle, dirtPosition, body.transform.rotation);
                if (isGrounded)
                {
                    body.velocity = moveForward;
                }
                else if(!isGrounded)
                {
                    body.velocity = new Vector2(moveForward.x, moveForward.y*-1);
                }
            }
        }

        public void SetMoveForward(float speedChange)
        {
            if (speedChange > 1)
            {
                moveForward.x *= (speedChange - 0.3f);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                Debug.Log("is grounded");
                isGrounded = true;
                AudioManager.instance.PlaySFX(FootSteps, 0.7f);
                AudioManager.instance.SetSFXPitch(0.8f, 1.0f);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                Debug.Log("is not grounded");
                isGrounded = false;
            }
        }
    }
}
