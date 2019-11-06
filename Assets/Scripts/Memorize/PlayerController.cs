#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memorize {
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        GameObject buttonPrefab;
        GameObject[] buttons;

        // Start is called before the first frame update
        void Start()
        {
            NewSet();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EndSet();
                NewSet();
            }
        }

        float RandomDirection()
        {
            float r = Random.value;
            return r < 0.5f ? (r < 0.25f ? 0f : 90f) : (r < 0.75f ? 180f : 270f);
        }

        void NewSet()
        {
            buttons = new GameObject[Mathf.RoundToInt(Random.value * 3f) + 3];
            float j = -buttons.Length - 1;
            for (int i = 0; i < buttons.Length; i++)
            {
                j += 2;
                buttons[i] = Instantiate(buttonPrefab, new Vector3(j, 0f, 0f), Quaternion.Euler(0f, 0f, RandomDirection()));
            }
        }

        void EndSet()
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button);
            }
        }
    }
}
#pragma warning restore 0649
