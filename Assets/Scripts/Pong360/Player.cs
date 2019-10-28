using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO add spin physics to ball
namespace Pong360
{
    public class Player : MonoBehaviour
    {
        public GameObject ball;
        public int bounces;

        bool invert;
        GameObject currentBall;
        float time;

        void Start()
        {
            currentBall = Instantiate(ball, Vector3.zero, Quaternion.identity);
            currentBall.GetComponent<Rigidbody2D>().velocity = 3 * Random.insideUnitCircle.normalized;
        }


        void Update()
        {

            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (Mathf.Abs(currentBall.transform.position.x) > 10)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Mathf.Abs(currentBall.transform.position.y) > 10)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            var rot = currentBall.transform.eulerAngles;

            if (invert)
            {
                rot.z += Time.deltaTime * ((bounces + 1) * 20);
            }
            else
            {
                rot.z -= Time.deltaTime * ((bounces + 1) * 20);
            }

            currentBall.transform.eulerAngles = rot;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            invert = !invert;

            bounces++;
            float multiplyer = 5 + (bounces * 0.3f);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized * multiplyer;
        }
    }
}

