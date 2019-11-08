using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sniper
{
    public class Crosshair : MonoBehaviour
    {
        public float cursorSpeed;
        public bool isMouseControlled;

        Vector2 mouse;

        void Start()
        {
            if (isMouseControlled)
            {
                Cursor.visible = false;
            }
        }
        
        void Update()
        {
            if (isMouseControlled)
            {
                mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                gameObject.transform.position = mouse;
            }
            else
            {
                transform.position = (Vector2)transform.position + new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * cursorSpeed * Time.deltaTime;
            }
            if (Input.GetButtonDown("Primary"))
            {
                Ray ray = new Ray(transform.position + new Vector3(0,0,-5), new Vector3(0, 0, 1));
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 40f, (1 << 10) | (1 << 11));
                if (hit)
                {
                    if (hit.transform.gameObject.GetComponent<Terrorist>() != null)
                    {
                        hit.transform.gameObject.GetComponent<Terrorist>().Die();
                    }
                    else if (hit.transform.gameObject.GetComponent<Citizen>() != null)
                    {
                        hit.transform.gameObject.GetComponent<Citizen>().Die();
                    }
                }
            }
        }
    }
}
