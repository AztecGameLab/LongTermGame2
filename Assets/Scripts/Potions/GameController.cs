using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Potions
{

    public class GameController : MonoBehaviour
    {
        public GameObject fillLine;
        public GameObject bottleOrigin;
        [Range(0, 1)]
        [SerializeField]
        public float filled;

        public float fillRate;
        public float filledSize;

        [Range(0, 1)]
        [SerializeField]
        public float r, g, b, opaqueness;

        public float redAmount, yellowAmount, blueAmount;
        private float overflowAmount;



        Vector3[] cube = new Vector3[8]
        {
                new Vector3(1,1,1),
                new Vector3(0.163f,0.373f,0.6f),
                new Vector3(0.5f,0.5f,0),
                new Vector3(1,0,0),
                new Vector3(1,1,0),
                new Vector3(0,0.66f,0.2f),
                new Vector3(0.2f,0.094f,0),
                new Vector3(1,0.5f,0),
        };

        // Start is called before the first frame update
        void Start()
        {
            filled = 0;
            r = 0;
            g = 0;
            b = 0;
            opaqueness = 1;

            Instantiate(fillLine, bottleOrigin.transform.position + new Vector3(0, filledSize, 0), Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Reset();
            }

            if (Input.GetKey(KeyCode.R))
            {
                redAmount += fillRate * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                yellowAmount += fillRate * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.B))
            {
                blueAmount += fillRate * Time.deltaTime;
            }

            if (redAmount + yellowAmount + blueAmount > 1)
            {
                overflowAmount = redAmount + yellowAmount + blueAmount - 1;
                redAmount -= overflowAmount * (redAmount / (redAmount + yellowAmount + blueAmount));
                yellowAmount -= overflowAmount * (yellowAmount / (redAmount + yellowAmount + blueAmount));
                blueAmount -= overflowAmount * (blueAmount / (redAmount + yellowAmount + blueAmount));
            }
            RYBtoRGB(redAmount, yellowAmount, blueAmount,out r,out g,out b);
            filled = redAmount + yellowAmount + blueAmount;
        }
        private void Reset()
        {
            filled = 0;
            r = 0;
            g = 0;
            b = 0;
            opaqueness = 1;
            redAmount = 0;
            blueAmount = 0;
            yellowAmount = 0;
            overflowAmount = 0;
        }
        private void RYBtoRGB(float r_RYB, float y_RYB, float b_RYB, out float r, out float g, out float b)
        {
            Vector3 interp1 = Vector3.Lerp(cube[0], cube[3], r_RYB);
            Vector3 interp2 = Vector3.Lerp(cube[1], cube[2], r_RYB);
            Vector3 interp3 = Vector3.Lerp(cube[4], cube[7], r_RYB);
            Vector3 interp4 = Vector3.Lerp(cube[5], cube[6], r_RYB);

            Vector3 interp5 = Vector3.Lerp(interp1, interp2, b_RYB);
            Vector3 interp6 = Vector3.Lerp(interp3, interp4, b_RYB);

            Vector3 interp7 = Vector3.Lerp(interp5, interp6, y_RYB);

            r = interp7.x;
            g = interp7.y;
            b = interp7.z;

        }
    }
}

