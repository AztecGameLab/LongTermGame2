using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TimothyL
{

    public class Player : MonoBehaviour
    {

        public GameObject ball;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Spaceeeeee");

                GameObject tempBall = Instantiate(ball, transform.position, transform.rotation);

                tempBall.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 10);
            }
        }
    }

}
