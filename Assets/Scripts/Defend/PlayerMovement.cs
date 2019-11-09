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

        public int playerPosition;

        //Booleans for movement of one unit
        private bool isLeft = false;
        private bool isRight = false;

        //Booleans for checking if Defense is present
        public bool defensePresent1 = false;
        public bool defensePresent2 = false;
        public bool defensePresent3 = false;

        //Defense GameObject
        public GameObject defenderSprite;


        // Start is called before the first frame update
        void Start()
        {
            position2 = transform.position;
            position1 = new Vector2(transform.position.x - 4f, transform.position.y);
            position3 = new Vector2(transform.position.x + 4f, transform.position.y);

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

            //Calling the Place Defense when the button is pressed
            if (Input.GetButtonDown("Primary"))
            {
                PlaceDefense(transform.position, playerPosition);
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
        }

        //Now to create a Method that Instantiates the new Defending Object thingy
        private void PlaceDefense(Vector2 playerPositonSpot, int playerPositionChecker)
        {
            if(playerPositionChecker == 1 && defensePresent1 == false)
            {
                Instantiate(defenderSprite, playerPositonSpot + new Vector2(0, 1.5f), Quaternion.identity);
                defensePresent1 = true;
            }
            if (playerPositionChecker == 2 && defensePresent2 == false)
            {
                Instantiate(defenderSprite, playerPositonSpot + new Vector2(0, 1.5f), Quaternion.identity);
                defensePresent2 = true;
            }
            if (playerPositionChecker == 3 && defensePresent3 == false)
            {
                Instantiate(defenderSprite, playerPositonSpot + new Vector2(0, 1.5f), Quaternion.identity);
                defensePresent3 = true;
            }
        }
    }
}
