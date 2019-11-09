using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Defend
{

    public class MonsterActions : MonoBehaviour
    {
        private float monsterStopwatch = 2f;

        public GameObject playerCharacter;
        public int monsterColumnPosition;
        
        // Start is called before the first frame update
        void Start()
        {
            monsterColumnPosition = playerCharacter.GetComponent<PlayerMovement>().playerPosition;
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
                print("Did you move successfully?");
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
                if (playerCharacter.GetComponent<PlayerMovement>().defensePresent1 == true)
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
                if (playerCharacter.GetComponent<PlayerMovement>().defensePresent2 == true)
                {
                    Destroy(gameObject);
                }
                else
                {
                    print("You've Been Killed!!!");
                    this.enabled = false;
                }
            }
            if (monsterColumnPosition == 1)
            {
                if (playerCharacter.GetComponent<PlayerMovement>().defensePresent2 == true)
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
