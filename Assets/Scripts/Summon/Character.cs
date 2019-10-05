using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimothyL {
    public class Character : MonoBehaviour
    {
        void Start()
        {
            
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Destroy(gameObject);
            }
        }
    }
}

