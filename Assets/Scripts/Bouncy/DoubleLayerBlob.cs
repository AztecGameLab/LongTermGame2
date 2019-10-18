using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleLayerBlob: MonoBehaviour
{
    public GameObject HardCircle;
    public int segments;
    public float innerRadius;
    public float outerRadius;
    public float dampening;
    public float frequency;
    public bool intercollision;
    List<GameObject> InnerBodies;
    List<GameObject> OuterBodies;
    public float innerSize;
    public float outerSize;
    // Start is called before the first frame update
    void Start()
    {
        InnerBodies = new List<GameObject>();
        OuterBodies = new List<GameObject>();
        for (int i = 0; i < segments; i++)
        {
            var innerVertex = Instantiate(HardCircle, transform);
            var outerVertex = Instantiate(HardCircle, transform);
            innerVertex.transform.localScale = Vector3.one * innerSize;
            outerVertex.transform.localScale = Vector3.one * outerSize;
            Vector3 innerPos;
            Vector3 outerPos;
            innerPos.x = transform.position.x + innerRadius * Mathf.Sin(((float)i / (float)segments) * 360f * Mathf.Deg2Rad);
            print(i / segments);
            innerPos.y = transform.position.y + innerRadius * Mathf.Cos(((float)i / (float)segments) * 360f * Mathf.Deg2Rad);
            innerPos.z = 0;
            innerVertex.transform.position = innerPos;
            outerPos.x = transform.position.x + outerRadius * Mathf.Sin(((float)i / (float)segments) * 360f * Mathf.Deg2Rad);
            print(i / segments);
            outerPos.y = transform.position.y + outerRadius * Mathf.Cos(((float)i / (float)segments) * 360f * Mathf.Deg2Rad);
            outerPos.z = 0;
            outerVertex.transform.position = outerPos;
            SpringJoint2D spoke = gameObject.AddComponent<SpringJoint2D>();
            SpringJoint2D outerSpoke = innerVertex.AddComponent<SpringJoint2D>();
            spoke.connectedBody = innerVertex.GetComponent<Rigidbody2D>();
            spoke.frequency = frequency;
            spoke.dampingRatio = dampening;
            outerSpoke.connectedBody = outerVertex.GetComponent<Rigidbody2D>();
            outerSpoke.frequency = frequency;
            outerSpoke.dampingRatio = dampening;
            InnerBodies.Add(innerVertex);
            OuterBodies.Add(outerVertex);
        }
        //inner ring
        GameObject lastSegment = InnerBodies[0];
        GameObject lastCrossSegment = OuterBodies[0];
        SpringJoint2D spring = lastSegment.AddComponent<SpringJoint2D>();
        SpringJoint2D crossSpring = lastSegment.AddComponent<SpringJoint2D>();
        spring.connectedBody = InnerBodies[InnerBodies.Count - 1].GetComponent<Rigidbody2D>();
        spring.frequency = frequency;
        spring.dampingRatio = dampening;
        crossSpring.connectedBody = OuterBodies[OuterBodies.Count - 1].GetComponent<Rigidbody2D>();
        crossSpring.frequency = frequency;
        crossSpring.dampingRatio = dampening;
        for (int i = 1; i < InnerBodies.Count; i++)
        {
            SpringJoint2D circleSpring= InnerBodies[i].AddComponent<SpringJoint2D>();
            SpringJoint2D crossCircleSpring = InnerBodies[i].AddComponent<SpringJoint2D>();
            circleSpring.connectedBody = lastSegment.GetComponent<Rigidbody2D>();
            circleSpring.frequency = frequency;
            circleSpring.dampingRatio = dampening;
            crossCircleSpring.connectedBody = lastCrossSegment.GetComponent<Rigidbody2D>();
            crossCircleSpring.frequency = frequency;
            crossCircleSpring.dampingRatio = dampening;
            lastSegment = InnerBodies[i];
            lastCrossSegment = OuterBodies[i];
        }
        //outer ring
        lastSegment = OuterBodies[0];
        spring = lastSegment.AddComponent<SpringJoint2D>();
        spring.connectedBody = OuterBodies[InnerBodies.Count - 1].GetComponent<Rigidbody2D>();
        spring.frequency = frequency;
        spring.dampingRatio = dampening;
        for (int i = 1; i < OuterBodies.Count; i++)
        {
            SpringJoint2D circleSpring = OuterBodies[i].AddComponent<SpringJoint2D>();
            circleSpring.connectedBody = lastSegment.GetComponent<Rigidbody2D>();
            circleSpring.frequency = frequency;
            circleSpring.dampingRatio = dampening;
            lastSegment = OuterBodies[i];
        }



    }

}
