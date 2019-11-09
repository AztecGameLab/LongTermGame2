using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Defend
{

    public class MonsterActions : MonoBehaviour
    {
        private float monsterStopwatch = 2f;

        public GameObject monsterplayer;
        public int monsterColumnPosition = 2;
        
        // Start is called before the first frame update
        void Start()
        {
            //For this line here, I need to make a spawner script that just controls the actual column of the spawn position of the Monster.
            monsterplayer = GameObject.Find("Player");
            monsterColumnPosition = monsterplayer.GetComponent<PlayerActions>().playerPosition;
                       
        }

        // Update is called once per frame
        void Update()
        {
            monsterStopwatch -= Time.deltaTime;
            MonsterMove();
            
        }

        //This is an automated movement of the monster and at the end checks if properly defended
        void MonsterMove()
        {
            if(monsterStopwatch < 0)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                monsterStopwatch = 2f;
            }
            if (transform.position.y <= -2)
            {
                DefenseCheck();
                return;
            }
        }
        //Checks if there's a defense to stop the monster from killing you
        void DefenseCheck()
        {
            
            if (monsterColumnPosition == 1)
            {
                if (monsterplayer.GetComponent<PlayerActions>().defensePresent1 == true)
                {
                    Destroy(gameObject);
                }
                else
                {
                    print("You've Been Killed!!!");
                    this.enabled = false;
                }
            }
            if (monsterColumnPosition == 2)
            {
                if (monsterplayer.GetComponent<PlayerActions>().defensePresent2 == true)
                {
                    Destroy(gameObject);
                }
                else
                {
                    print("You've Been Killed!!!");
                    this.enabled = false;
                }
            }
            if (monsterColumnPosition == 3)
            {
                if (monsterplayer.GetComponent<PlayerActions>().defensePresent3 == true)
                {
                    Destroy(gameObject);
                }
                else
                {
                    print("You've Been Killed!!!");
                    this.enabled = false;

                }
            }
            
        }
    }
}
