using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Defend
{

    public class MonsterActions : MonoBehaviour
    {
        private float monsterStopwatch = 1f;

        private GameObject monsterPlayer;
        private GameObject evilSummoner;
        public int monsterColumnPosition = 2;

        public GameObject defenses;

        // Start is called before the first frame update
        void Start()
        {        
            //For this line here, I need to make a spawner script that just controls the actual column of the spawn position of the Monster.
            evilSummoner = GameObject.Find("Evil Dude");
            monsterColumnPosition = evilSummoner.GetComponent<EvilSummonerActions>().spawnColumn;

            monsterPlayer = GameObject.Find("Player");
                       
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
                monsterStopwatch = 1f;
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
                    evilSummoner.GetComponent<EvilSummonerActions>().monsterPresent1 = false;
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
                    evilSummoner.GetComponent<EvilSummonerActions>().monsterPresent2 = false;
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
                    evilSummoner.GetComponent<EvilSummonerActions>().monsterPresent3 = false;
                    monsterPlayer.GetComponent<PlayerActions>().defensePresent3 = false;
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
