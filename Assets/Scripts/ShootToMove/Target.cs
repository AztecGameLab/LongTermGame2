using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ShootToMove
{

    public class Target : MonoBehaviour
    {
        public int value;
        Text score;

        // Start is called before the first frame update
        void Start()
        {
            score = GameObject.Find("Score").GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Bullet(Clone)")
            {
                score.GetComponent<Score>().AddToScore(value);
                Destroy(gameObject);
            }
        }
    }
}
