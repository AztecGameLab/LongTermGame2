using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMovement : MonoBehaviour
{
    public float speed = 20.0f;

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
