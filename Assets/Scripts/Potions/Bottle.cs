using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Potions
{
    public class Bottle : MonoBehaviour
    {
        public GameController gc;

        SpriteRenderer sr;

        // Start is called before the first frame update
        void Start()
        {
            sr = gameObject.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.parent.localScale = new Vector3(1, gc.filled * gc.filledSize, 1);
            sr.color = new Color(gc.r, gc.g, gc.b, gc.opaqueness);
        }
    }
}
