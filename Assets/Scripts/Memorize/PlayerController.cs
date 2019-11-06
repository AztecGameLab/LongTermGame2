using UnityEngine;

namespace Memorize {
    public class PlayerController : MonoBehaviour
    {
        GameObject[] buttons;
        #pragma warning disable 0649
        [SerializeField]
        GameObject buttonPrefab;
        [SerializeField]
        float maxTime, currentTime;
        #pragma warning restore 0649

        void FixedUpdate()
        {
            
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
