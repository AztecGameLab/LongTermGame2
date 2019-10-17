using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOn2DPivot : MonoBehaviour
{
    public string GeorgeVariableName; 

    void OnCollisionEnter2D(Collision2D collision)
    {
        AkSoundEngine.PostEvent(GeorgeVariableName, gameObject);
        Debug.Log("Testing");
    }
}
