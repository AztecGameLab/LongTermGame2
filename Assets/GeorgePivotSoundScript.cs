using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgePivotSoundScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D pivotcollision)
    {
        AkSoundEngine.PostEvent("BallHitsPaddle", gameObject);
        Debug.Log("Testing");
    }
}
