using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunAwayThief
{
    public class GameController : MonoBehaviour
    {
        public PlayerController player;
        public EnemyController enemy;
        public GameObject Canvas, Panel_Win, Panel_Lose, ground;
        public Collider2D playerCollider, enemyCollider, groundCollider;
        public bool end = false;
        public float period = 0.1f;

        float nextActionTime = 0.0f;

        void Start()
        {
            Canvas.SetActive(false);
            Panel_Lose.SetActive(false);
            Panel_Win.SetActive(false);
            playerCollider = player.GetComponent<Collider2D>();
            enemyCollider = enemy.GetComponent<Collider2D>();
            groundCollider = ground.GetComponent<Collider2D>();
        }

        void Update()
        {
            if (enemyCollider.IsTouching(playerCollider))
            {
                EndGame("lose");
                end = true;
            }
            else if (player.transform.position.x >= 11.5)
            {
                EndGame("win");
                end = true;
            }
            if(end == false)
            { 
                if (Time.time*Time.deltaTime > nextActionTime)
                {
                    nextActionTime += period;
                    enemy.EnemyRun();
                }
            }
        }

        public void EndGame(string endCondition)
        {
            if (endCondition == "lose")
            {
                Canvas.SetActive(true);
                Panel_Lose.SetActive(true);
                Panel_Win.SetActive(false);
            }
            else if(endCondition == "win")
            {
                Canvas.SetActive(true);
                Panel_Lose.SetActive(false);
                Panel_Win.SetActive(true);
            }
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
