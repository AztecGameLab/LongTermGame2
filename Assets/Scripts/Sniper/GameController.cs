using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sniper
{
    public class GameController : MonoBehaviour
    {
        public Camera cam;
        public GameObject baddie;
        public int totalBaddies;
        public GameObject goodie;
        public float goodieBottomSpeed;
        public float goodieTopSpeed;
        public float spawnRate;
        public bool gameOn;
        public float timePenalty;
        public int goodiesKilled;

        int currentBaddies;
        float xLower;
        float xUpper;
        float yLower;
        float yUpper;
        float diameter;
        float timeLastSpawn;

        enum SpawnLocation
        {
            top = 1,
            right = 2,
            bottom = 3,
            left = 4
        }
        
        void Start()
        {
            gameOn = true;
            goodiesKilled = 0;

            float camHeight = cam.orthographicSize * 2;
            float camWidth = camHeight * Screen.width / Screen.height;
            diameter = 2 * baddie.GetComponent<CircleCollider2D>().radius;
            diameter *= 1.1f;

            xLower = (cam.transform.position.x - (camWidth / 2));
            xUpper = (cam.transform.position.x + (camWidth / 2));
            yLower = (cam.transform.position.y - (camHeight / 2));
            yUpper = (cam.transform.position.y + (camHeight / 2));

            //spawns baddies
            for (int i = 0; i < totalBaddies; i++)
            {
                Instantiate(baddie, new Vector2(Random.Range(xLower + diameter, xUpper - diameter), Random.Range(yLower + diameter, yUpper - diameter)), Quaternion.identity);
                currentBaddies++;
            }

            //spawns initial goodies (# based on bottom speed)
            for (int i = 0; i < goodieBottomSpeed * 5; i++)
            {
                GameObject newGoodie = Instantiate(goodie, new Vector2(Random.Range(xLower + diameter, xUpper - diameter), Random.Range(yLower + diameter, yUpper - diameter)), Quaternion.identity);
                int dir = Random.Range(1, 5);
                switch (dir)
                {
                    case 1:
                        newGoodie.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -Random.Range(goodieBottomSpeed, goodieTopSpeed));
                        break;
                    case 2:
                        newGoodie.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(goodieBottomSpeed, goodieTopSpeed), 0);
                        break;
                    case 3:
                        newGoodie.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(goodieBottomSpeed, goodieTopSpeed));
                        break;
                    case 4:
                        newGoodie.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(goodieBottomSpeed, goodieTopSpeed), 0);
                        break;
                }
            }

            //spawns first goodie outside bounds
            SpawnGoodie((SpawnLocation)Random.Range(1, 5));
            timeLastSpawn = Time.time;
        }
        
        void Update()
        {
            //spawns goodies outside at spawnrate
            if(Time.time > timeLastSpawn + (1 / spawnRate))
            {
                SpawnGoodie((SpawnLocation)Random.Range(1, 5));
                timeLastSpawn = Time.time;
            }
        }

        void SpawnGoodie(SpawnLocation loc)
        {
            if (loc == SpawnLocation.top)
            {
                GameObject newGoodie = Instantiate(goodie, new Vector2(Random.Range(xLower + diameter, xUpper - diameter), yUpper + diameter), Quaternion.identity);
                newGoodie.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -Random.Range(goodieBottomSpeed, goodieTopSpeed));
            }
            else if (loc == SpawnLocation.left)
            {
                GameObject newGoodie = Instantiate(goodie, new Vector2(xLower - diameter, Random.Range(yLower + diameter, yUpper - diameter)), Quaternion.identity);
                newGoodie.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(goodieBottomSpeed, goodieTopSpeed), 0);
            }
            else if (loc == SpawnLocation.bottom)
            {
                GameObject newGoodie = Instantiate(goodie, new Vector2(Random.Range(xLower + diameter, xUpper - diameter), yLower - diameter), Quaternion.identity);
                newGoodie.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Random.Range(goodieBottomSpeed, goodieTopSpeed));
            }
            else if (loc == SpawnLocation.right)
            {
                GameObject newGoodie = Instantiate(goodie, new Vector2(xUpper + diameter, Random.Range(yLower + diameter, yUpper - diameter)), Quaternion.identity);
                newGoodie.GetComponent<Rigidbody2D>().velocity = new Vector2(-Random.Range(goodieBottomSpeed, goodieTopSpeed), 0);
            }
        }

        public void KillBaddie()
        {
            currentBaddies--;
            if(currentBaddies == 0)
            {
                EndGame();
            }
        }

        void EndGame()
        {
            gameOn = false;
            Invoke("Restart", 3);
        }

        void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
