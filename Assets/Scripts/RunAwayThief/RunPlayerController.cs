using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RunAwayThief
{
    public class RunPlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        public Vector2 moveForward;
        public float increaseSpeed;
        public RunGameController gameController;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(!gameController.end)
            {
                PlayerRun();
            }
        }

        public void PlayerRun()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Rigidbody2D>().velocity = moveForward;
                GetComponent<Rigidbody2D>().AddForce(moveForward * increaseSpeed);
            }
        }
    }
}
