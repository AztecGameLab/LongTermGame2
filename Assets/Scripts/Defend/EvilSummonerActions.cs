using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Defend
{
    public class EvilSummonerActions : MonoBehaviour
    {
        public GameObject generalMonster;
        public GameObject fastMonster;
        public GameObject slowMonster;
        private GameObject monsterToAdd;

        private float spawnTimer = 2f;

        public int spawnColumn;

        private int monsterDecider;

        public GameObject playerCharacter;

        public bool monsterPresent1;
        public bool monsterPresent2;
        public bool monsterPresent3;
        public bool monsterPresent4;
        public bool monsterPresent5;


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

        //The method that constantly and randomly spawns diff types of monsters
        private void SpawnMonster()
        {
            if (spawnTimer < 0)
            {
                spawnTimer = .6f; 

                monsterDecider = (int)Random.Range(1f, 4f);
                if (monsterDecider == 1)
                    monsterToAdd = generalMonster;
                if (monsterDecider == 2)
                    monsterToAdd = fastMonster;
                if (monsterDecider == 3)
                    monsterToAdd = slowMonster;

                spawnColumn = (int)Random.Range(1f, 6f);
                if (spawnColumn == 1 && monsterPresent1 == false)
                {
                    Instantiate(monsterToAdd, playerCharacter.GetComponent<PlayerActions>().position1 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent1 = true;
                }
                if (spawnColumn == 2 && monsterPresent2 == false)
                {
                    Instantiate(monsterToAdd, playerCharacter.GetComponent<PlayerActions>().position2 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent2 = true;
                }
                if (spawnColumn == 3 && monsterPresent3 == false)
                {
                    Instantiate(monsterToAdd, playerCharacter.GetComponent<PlayerActions>().position3 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent3 = true;
                }
                if (spawnColumn == 4 && monsterPresent4 == false)
                {
                    Instantiate(monsterToAdd, playerCharacter.GetComponent<PlayerActions>().position4 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent4 = true;
                }
                if (spawnColumn == 5 && monsterPresent5 == false)
                {
                    Instantiate(monsterToAdd, playerCharacter.GetComponent<PlayerActions>().position5 + new Vector2(0, 7.5f), Quaternion.identity);
                    monsterPresent5 = true;
                }

            }

        }
    }
}
