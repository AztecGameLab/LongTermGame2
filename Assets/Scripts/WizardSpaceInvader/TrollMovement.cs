using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollMovement : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

}
