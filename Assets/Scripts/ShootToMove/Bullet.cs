using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootToMove
{
    public class Bullet : MonoBehaviour
    {
        void Start()
        {

        }
        
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}
