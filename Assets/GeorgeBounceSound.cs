using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeBounceSound : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D pivotcollision)
    {
        AkSoundEngine.PostEvent("BounceOffWall", gameObject);
        Debug.Log("Testing");
    }
}
