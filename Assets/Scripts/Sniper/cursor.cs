using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sniper
{
    public class cursor : MonoBehaviour
    {
        Vector2 mouse;
        Rigidbody2D rb;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            gameObject.transform.position = mouse;
        }
    }
}
