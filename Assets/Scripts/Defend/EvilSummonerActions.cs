using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Defend
{
    public class EvilSummonerActions : MonoBehaviour
    {
        private GameObject generalMonster;
        private GameObject fastMonster;
        private GameObject slowMonster;

        private float spawnTimer = 4f;

        private int monsterDecider;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            spawnTimer -= Time.deltaTime;
            SpawnMonster();print((int) Random.Range(0,3f));
        }

        //The method that constantly and randomly spawns diff types of monsters
        private void SpawnMonster()
        {
            if (spawnTimer < 0)
            {
                monsterDecider = (int) Random.Range(1, 4f);
            }
        }
    }
}
