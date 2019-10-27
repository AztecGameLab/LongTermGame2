﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sniper
{
    public class baddie : MonoBehaviour
    {
        public float speed;
        Rigidbody2D rb;
        GameController gc;
        // Start is called before the first frame update
        void Start()
        {
            gc = FindObjectOfType<GameController>();
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * speed;

        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnMouseDown()
        {
            gc.KillBaddie();
            Destroy(gameObject);
        }
    }
}