using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TimothyLieu
{
    public class SineWave : MonoBehaviour
    {
        //Creating a specific speed level
        public float speed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        }
    }
}
