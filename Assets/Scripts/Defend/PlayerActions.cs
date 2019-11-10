using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defend
{
    public class PlayerActions : MonoBehaviour
    {
        //The three different spots that the player could be
        public Vector2 position1;
        public Vector2 position2;
        public Vector2 position3;
        public Vector2 position4;
        public Vector2 position5;

        public int playerPosition;

        public static GameObject playerCharacter;

        //Booleans for movement of one unit
        private bool isLeft = false;
        private bool isRight = false;

        //Booleans for checking if Defense is present
        public bool defensePresent1 = false;
        public bool defensePresent2 = false;
        public bool defensePresent3 = false;
        public bool defensePresent4 = false;
        public bool defensePresent5 = false;

        //Defense GameObjects
        public GameObject defenderSprite1;
        public GameObject defenderSprite2;
        public GameObject defenderSprite3;
        public GameObject defenderSprite4;
        public GameObject defenderSprite5;



        // Start is called before the first frame update
        void Start()
        {
            playerCharacter = this.gameObject;
            position3 = transform.position;
            position1 = new Vector2(transform.position.x - 3f, transform.position.y);
            position2 = new Vector2(transform.position.x - 1.5f, transform.position.y);
            position4 = new Vector2(transform.position.x + 1.5f, transform.position.y);
            position5 = new Vector2(transform.position.x + 3f, transform.position.y);

        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            
            //This little section is chedcking for movement of the Player sprite
            if (horizontal == 1 && playerPosition < 5)
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
            if (Input.GetKeyDown(KeyCode.Z))    //(Input.GetButtonDown("Primary"))
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
            if (playerPosition == 4)
            {
                transform.position = position4;
            }
            if (playerPosition == 5)
            {
                transform.position = position5;
            }
        }

        //Now to create a Method that Instantiates the new Defending Object thingy
        private void PlaceDefense(Vector2 playerPositonSpot, int playerPositionChecker)
        {
            if(playerPositionChecker == 1 && defensePresent1 == false)
            {
                Instantiate(defenderSprite1, playerPositonSpot + new Vector2(0, 1.5f), Quaternion.identity);
                defensePresent1 = true;
            }
            if (playerPositionChecker == 2 && defensePresent2 == false)
            {
                Instantiate(defenderSprite2, playerPositonSpot + new Vector2(0, 1.5f), Quaternion.identity);
                defensePresent2 = true;
            }
            if (playerPositionChecker == 3 && defensePresent3 == false)
            {
                Instantiate(defenderSprite3, playerPositonSpot + new Vector2(0, 1.5f), Quaternion.identity);
                defensePresent3 = true;
            }
            if (playerPositionChecker == 4 && defensePresent4 == false)
            {
                Instantiate(defenderSprite4, playerPositonSpot + new Vector2(0, 1.5f), Quaternion.identity);
                defensePresent1 = true;
            }
            if (playerPositionChecker == 5 && defensePresent5 == false)
            {
                Instantiate(defenderSprite5, playerPositonSpot + new Vector2(0, 1.5f), Quaternion.identity);
                defensePresent1 = true;
            }
        }
    }
}
