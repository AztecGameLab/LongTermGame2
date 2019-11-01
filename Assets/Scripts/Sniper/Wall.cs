using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sniper
{

    public class Wall : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.GetComponent<RectTransform>().rect.width,
                                                                        gameObject.GetComponent<RectTransform>().rect.height);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
