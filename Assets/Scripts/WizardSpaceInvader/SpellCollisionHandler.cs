using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
       if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
