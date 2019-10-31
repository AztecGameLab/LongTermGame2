using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootToMove
{
    public class PlayerController : MonoBehaviour
    {
        public float shootForce;
        public GameObject bullet;
        public float bulletSpeed;

        Vector2 mouse;
        Rigidbody2D rb;
        Vector2 faceDirection;
        
        void Start()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {
            mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            faceDirection = (mouse - rb.position).normalized;
            float faceAngle = (Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg) - 90;
            rb.SetRotation(faceAngle);

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            rb.velocity = (rb.velocity + (faceDirection * shootForce));
            GameObject newBullet = Instantiate(bullet, this.gameObject.transform.GetChild(0).transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = -1 * faceDirection * bulletSpeed;
        }
    }
}
