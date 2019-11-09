using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Defend
{

    public class MonsterActions : MonoBehaviour
    {
        private float monsterStopwatch = 3f;
        
        // Start is called before the first frame update
        void Start()
        {
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
                monsterStopwatch = 3f;
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
            transform.position = new Vector2(transform.position.x, 4f);
            print("Does this work right now?");
            Destroy(gameObject);
        }
    }
}
