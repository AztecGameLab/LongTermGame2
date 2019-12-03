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
        public GameObject dirtParticle;
#pragma warning disable 0649
        [SerializeField] AudioClip FootSteps;
        #pragma warning restore 0649

        void Start()
        {
            playerBox = GetComponent<BoxCollider2D>();
            dirtParticle = GameObject.Find("DirtParticle");
        }
        
        void Update()
        {
            
        }

        public void EnemyRun(float difficultyModifier)
        {
            Vector3 dirtPosition = new Vector3(playerBox.transform.position.x, playerBox.transform.position.y - 1.4f, playerBox.transform.position.z);
            Instantiate(dirtParticle, dirtPosition, playerBox.transform.rotation);
            this.GetComponent<Rigidbody2D>().velocity = moveForward * difficultyModifier;
            this.GetComponent<Rigidbody2D>().AddForce(moveForward * increaseSpeed);
            AudioManager.instance.PlaySFX(FootSteps, 0.7f);
            AudioManager.instance.SetSFXPitch(0.8f, 1.0f);
        }
    }
}
