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
        [SerializeField]
        private float changeSpeed;
        private float enemySpeed;
        [Range(0, 1)] public float difficultyModifier;
        float nextActionTime = 0.0f;

        #pragma warning disable 0649
        [SerializeField] AudioClip RunAwayThiefMusic;
        #pragma warning restore 0649

        void Start()
        {
            Canvas.SetActive(false);
            Panel_Lose.SetActive(false);
            Panel_Win.SetActive(false);
            playerCollider = player.GetComponent<Collider2D>();
            enemyCollider = enemy.GetComponent<Collider2D>();
            groundCollider = ground.GetComponent<Collider2D>();
            difficultyModifier = MinigameManager.GetDifficulty();
            difficultyModifier = 1;
            changeSpeed = 1.5f;
            enemySpeed = Mathf.LerpUnclamped(1, 3.3f, difficultyModifier);
            player.SetMoveForward(changeSpeed);
            nextActionTime = Time.time + period;
            AudioManager.instance.PlayMusic(RunAwayThiefMusic, 0.9f, 1.0f, true);
        }

        void Update()
        {
            if (enemyCollider.IsTouching(playerCollider))
            {
                EndGame("lose");
                end = true;
            }
            else if (player.transform.position.x >= 9.8f)
            {
                EndGame("win");
                end = true;
            }
            if(end == false)
            {
                player.PlayerRun();
                if (Time.time/**Time.deltaTime*/ > nextActionTime)
                {
                    nextActionTime += period;
                    enemy.EnemyRun(enemySpeed);
                    //enemy.EnemyRun(changeSpeed);
                }
            }
        }

        public void EndGame(string endCondition)
        {
            if (endCondition == "lose")
            {
                /*Canvas.SetActive(true);
                Panel_Lose.SetActive(true);
                Panel_Win.SetActive(false);*/
                MinigameManager.FinishMinigame(false);
            }
            else if(endCondition == "win")
            {
                /*Canvas.SetActive(true);
                Panel_Lose.SetActive(false);
                Panel_Win.SetActive(true);*/
                MinigameManager.FinishMinigame(true);
            }
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
