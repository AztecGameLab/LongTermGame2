using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defend
{
    public class PlayerMovement : MonoBehaviour
    {
        //The three different spots that the player could be
        public Vector2 position1;
        public Vector2 position2;
        public Vector2 position3;

        public int playerPosition = 2;

        public bool isLeft = false;
        public bool isRight = false;

        
        // Start is called before the first frame update
        void Start()
        {
            position2 = transform.position;
            position1 = new Vector2(transform.position.x - 5f, transform.position.y);
            position3 = new Vector2(transform.position.x + 5f, transform.position.y);
            transform.position = position1;

        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            
            //This little section is chedcking for movement of the Player sprite
            if (horizontal == 1 && playerPosition < 3)
            {
                if(isRight == false)
                {
                    playerPosition += 1;
                    MovePlayer();
                    isRight = true;
                }
            }
            if(horizontal == -1 && playerPosition > 1)
            {
                if(isLeft == false)
                {
                    playerPosition -= 1;
                    MovePlayer();
                    isLeft = true;
                }
            }
            if(horizontal == 0)
            {
                isLeft = false;
                isRight = false;
            }

        }

        //Creating the method to move the player
        private void MovePlayer()
        {
            if(playerPosition == 1)
            {
                transform.position = position1;
            }
            if (playerPosition == 2)
            {
                transform.position = position2;
            }
            if (playerPosition == 3)
            {
                transform.position = position3;
            }
            print("Hello");
        }
    }
}
