using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootToMove
{
    public class BlockDestroyer : MonoBehaviour
    {
        void Start()
        {

        }
        
        void Update()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}
