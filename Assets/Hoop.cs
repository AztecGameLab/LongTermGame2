using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoop : MonoBehaviour
{

    public ParticleSystem ParticleSystem;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem tempPs =  Instantiate(ParticleSystem, transform.position, transform.rotation);
        tempPs.Play();

        //Destroy(tempPs, 5);
        
    }
}
