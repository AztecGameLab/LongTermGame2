using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            MinigameManage.FinishMinigame(false);
        }
    }
}
