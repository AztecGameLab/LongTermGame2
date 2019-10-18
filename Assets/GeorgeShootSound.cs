using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeShootSound : MonoBehaviour
{
    void OnMouseClick()
    {
        AkSoundEngine.PostEvent("FireParticle", gameObject);
        Debug.Log("Testing");
    }
}