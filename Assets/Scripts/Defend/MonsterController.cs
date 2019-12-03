using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defend
{

    public class MonsterController : MonoBehaviour
    {

        private float monsterDecider;
        private float moveSpeed;
        private float moveDistance;

        private float monsterStopwatch;

        private bool[] monsterPresent;
       

        private GameObject monsterPlayer;
        private GameObject evilSummoner;
        public int monsterColumnPosition = 2;

        // Start is called before the first frame update
        void Start()
        {        
            //For this line here, I need to make a spawner script that just controls the actual column of the spawn position of the Monster.
            evilSummoner = GameObject.Find("Evil Dude");
            monsterColumnPosition = evilSummoner.GetComponent<EvilSummonerActions>().spawnColumn;

            monsterPlayer = GameObject.Find("Player");

            DecideMonster();
            monsterStopwatch = moveSpeed;
                       
        }

        // Update is called once per frame
        void Update()
        {
            monsterStopwatch -= Time.deltaTime;
            MonsterMove();
            
        }

        //Determines the type of monster it is. Also need to make it so that it sets the sprite to something
        void DecideMonster()
        {
            /*Will need to make two variables. Move Speed and Move Distance
                 * Name, moveSpeed, moveDistance
                 * Basic Monster, .75, 1
                 * Slow Monster, 1, 2
                 * Fast Monster, .075, .5 
                 * 
                 */

            monsterDecider = (int)Random.Range(1f, 4f);
            if (monsterDecider == 1)
            {
                //This is the Basic monster
                moveSpeed = .75f;
                moveDistance = 1;
            }
            else if (monsterDecider == 2)
            {
                //Slow Monster
                moveSpeed = 1;
                moveDistance = 2;
            }
            else if (monsterDecider == 3)
            {
                //Fast Monster
                moveSpeed = .075f;
                moveDistance = .5f;
            }
        }

        //This is an automated movement of the monster and at the end checks if properly defended
        void MonsterMove()
        {
            if(monsterStopwatch < 0)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - moveDistance);
                monsterStopwatch = moveSpeed;
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
                if (monsterPlayer.GetComponent<PlayerActions>().defensePresent1 == true)
                {
                    //defenses.GetComponent<DefenderActions>().defenseMonsterPresent1 = true;
                    Destroy(GameObject.Find("Defender1(Clone)"));
                    Destroy(gameObject);
                    evilSummoner.GetComponent<EvilSummonerActions>().monsterPresent[0] = false;
                    monsterPlayer.GetComponent<PlayerActions>().defensePresent1 = false;
                }
                else
                {
                    print("You've Been Killed!!!");
                    this.enabled = false;
                }
            }
            if (monsterColumnPosition == 2)
            {
                if (monsterPlayer.GetComponent<PlayerActions>().defensePresent2 == true)
                {
                    //defenses.GetComponent<DefenderActions>().defenseMonsterPresent2 = true;
                    Destroy(GameObject.Find("Defender2(Clone)"));
                    Destroy(gameObject);
                    evilSummoner.GetComponent<EvilSummonerActions>().monsterPresent[1] = false;
                    monsterPlayer.GetComponent<PlayerActions>().defensePresent2 = false;
                }
                else
                {
                    print("You've Been Killed!!!");
                    this.enabled = false;
                }
            }
            if (monsterColumnPosition == 3)
            {
                if (monsterPlayer.GetComponent<PlayerActions>().defensePresent3 == true)
                {
                    //defenses.GetComponent<DefenderActions>().defenseMonsterPresent3 = true;
                    Destroy(GameObject.Find("Defender3(Clone)"));
                    Destroy(gameObject);
                    evilSummoner.GetComponent<EvilSummonerActions>().monsterPresent[2] = false;
                    monsterPlayer.GetComponent<PlayerActions>().defensePresent3 = false;
                }
                else
                {
                    print("You've Been Killed!!!");
                    this.enabled = false;

                }
            }
            if (monsterColumnPosition == 4)
            {
                if (monsterPlayer.GetComponent<PlayerActions>().defensePresent4 == true)
                {
                    //defenses.GetComponent<DefenderActions>().defenseMonsterPresent4 = true;
                    Destroy(GameObject.Find("Defender4(Clone)"));
                    Destroy(gameObject);
                    evilSummoner.GetComponent<EvilSummonerActions>().monsterPresent[3] = false;
                    monsterPlayer.GetComponent<PlayerActions>().defensePresent4 = false;
                }
                else
                {
                    print("You've Been Killed!!!");
                    this.enabled = false;

                }
            }
            if (monsterColumnPosition == 5)
            {
                if (monsterPlayer.GetComponent<PlayerActions>().defensePresent5 == true)
                {
                    //defenses.GetComponent<DefenderActions>().defenseMonsterPresent5 = true;
                    Destroy(GameObject.Find("Defender5(Clone)"));
                    Destroy(gameObject);
                    evilSummoner.GetComponent<EvilSummonerActions>().monsterPresent[4] = false;
                    monsterPlayer.GetComponent<PlayerActions>().defensePresent5 = false;
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
