using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sniper
{
    public class Wall : MonoBehaviour
    {
        void Start()
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.GetComponent<RectTransform>().rect.width, gameObject.GetComponent<RectTransform>().rect.height);
        }
        
        void Update()
        {

        }
    }
}
