using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sniper
{
    public class Cursor : MonoBehaviour
    {
        Vector2 mouse;
        Rigidbody2D rb;
        
        void Start()
        {
            // Cursor.visible = false;
        }
        
        void Update()
        {
            mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            gameObject.transform.position = mouse;
        }
    }
}
