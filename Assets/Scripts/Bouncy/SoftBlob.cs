using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBlob : MonoBehaviour
{
    public GameObject HardCircle;
    public int segments;
    public float radius;
    public float dampening;
    public float frequency;
    public bool intercollision;
    List<GameObject> bodies;
    public float sizes;
    // Start is called before the first frame update
    void Start()
    {
        bodies = new List<GameObject>();
        for (int i = 0; i < segments; i++)
        {
            var subject = Instantiate(HardCircle, transform);
            subject.transform.localScale = Vector3.one * sizes;
            Vector3 pos;
            pos.x = transform.position.x + radius * Mathf.Sin(((float)i / (float)segments) * 360f * Mathf.Deg2Rad);
            print(i / segments);
            pos.y = transform.position.y + radius * Mathf.Cos(((float)i / (float)segments) * 360f * Mathf.Deg2Rad);
            pos.z = 0;
            subject.transform.position = pos;
            SpringJoint2D spoke = gameObject.AddComponent<SpringJoint2D>();
            spoke.connectedBody = subject.GetComponent<Rigidbody2D>();
            spoke.frequency = frequency;
            spoke.dampingRatio = dampening;
            bodies.Add(subject);
        }
        GameObject lastSegment = bodies[0];
        SpringJoint2D spring = lastSegment.AddComponent<SpringJoint2D>();
        spring.connectedBody = bodies[bodies.Count - 1].GetComponent<Rigidbody2D>();
        spring.frequency = frequency;
        spring.dampingRatio = dampening;
        for (int i = 1; i < bodies.Count; i++)
        {
            SpringJoint2D circleSpring = bodies[i].AddComponent<SpringJoint2D>();
            circleSpring.connectedBody = lastSegment.GetComponent<Rigidbody2D>();
            circleSpring.frequency = frequency;
            circleSpring.dampingRatio = dampening;
            lastSegment = bodies[i];
        }





    }

    

}
