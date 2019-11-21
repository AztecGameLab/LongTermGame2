using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    public GameObject spell;
    public float speed = 1.0f;

    #pragma warning disable 0649
    [SerializeField] AudioClip WizardSpaceInvaderClip;
    #pragma warning restore 0649

    private void Start()
    {
        AudioManager.instance.PlayMusic(WizardSpaceInvaderClip, 0.6f, 1.0f, true);
    }

    // Update is called once per frame
    void Update()
    {
        //player movement with arrow keys
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0) * Time.deltaTime * speed;

        //initiates a moving spell when user hits space
        if (Input.GetKeyDown("space"))
        {
            castSpell();
        }
    }
    
    //user fires projectiles when spacebar is pressed
    void castSpell()
    {
        float x = transform.position.x;
        float y = transform.position.y + 1.5f;
        Instantiate(spell, new Vector2(x, y), Quaternion.identity);
    }

}
