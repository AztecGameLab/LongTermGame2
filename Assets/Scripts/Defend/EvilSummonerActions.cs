using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Defend
{
    public class EvilSummonerActions : MonoBehaviour
    {
        //Probably will need to serialize this later
        public GameObject monster;

        private float spawnTimer = 2f;

        //Need to learn how to serialize this too
        public int spawnColumn;

        public GameObject playerCharacter;

        public bool[] monsterPresent = new bool[5];
        /*public bool monsterPresent1;
        public bool monsterPresent2;
        public bool monsterPresent3;
        public bool monsterPresent4;
        public bool monsterPresent5;
        */


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            spawnTimer -= Time.deltaTime;
            SpawnMonster();
        }

        //The method that constantly and randomly spawns diff types of monsters. Will need to add to a serialized array that will disable evertthing on loss
        private void SpawnMonster()
        {
            if (spawnTimer < 0)
            {
                spawnTimer = .6f;


                spawnColumn = (int)Random.Range(1f, 6f);
                if (spawnColumn == 1 && monsterPresent[0] == false)
                {
                    Instantiate(monster, playerCharacter.GetComponent<PlayerActions>().position1 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent[0] = true;
                }
                if (spawnColumn == 2 && monsterPresent[1] == false)
                {
                    Instantiate(monster, playerCharacter.GetComponent<PlayerActions>().position2 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent[1] = true;
                }
                if (spawnColumn == 3 && monsterPresent[2] == false)
                {
                    Instantiate(monster, playerCharacter.GetComponent<PlayerActions>().position3 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent[2] = true;
                }
                if (spawnColumn == 4 && monsterPresent[3] == false)
                {
                    Instantiate(monster, playerCharacter.GetComponent<PlayerActions>().position4 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent[3] = true;
                }
                if (spawnColumn == 5 && monsterPresent[4] == false)
                {
                    Instantiate(monster, playerCharacter.GetComponent<PlayerActions>().position5 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent[4] = true;
                }

            }

        }
    }
}
