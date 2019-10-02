using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    Rigidbody2D rb;
    public float scrollSpeed;
    public float speedIncrease;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

        transform.position = transform.position + (new Vector3(scrollSpeed, 0,0) * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "ArcherH Shooty")
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("Archer");
        }
    }
    public void IncreaseSpeed()
    {
        scrollSpeed += speedIncrease;
    }
}
