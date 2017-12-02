// Date   : 02.12.2017 10:20
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class PullWhenFarEnough : MonoBehaviour
{

    [SerializeField]
    private GameObject target;

    private Rigidbody targetRb;
    private Rigidbody2D targetRb2d;
    private bool is3D = false;

    [SerializeField]
    [Range(0.1f, 100f)]
    private float strength;

    [SerializeField]
    [Range(0.1f, 100f)]
    private float radius;

    [SerializeField]
    [Range(0.5f, 10f)]
    private float bufferLength = 4f;

    void Start()
    {
        targetRb2d = target.GetComponent<Rigidbody2D>();
        if (targetRb2d == null)
        {
            is3D = true;
            targetRb = target.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        float pullStrength;
        if (distance + bufferLength >= radius)
        {
            if (distance < radius)
            {
                pullStrength = strength * (Mathf.Abs(radius - (bufferLength + distance)) / bufferLength);
            }
            else
            {
                pullStrength = strength;
            }
            Vector2 force = transform.position - target.transform.position;
            force.Normalize();

            if (is3D)
            {
                targetRb.AddForce(force * targetRb.mass * pullStrength / force.magnitude);
            }
            else
            {
                targetRb2d.AddForce(force * targetRb2d.mass * pullStrength / force.magnitude);
            }
        }

    }
}
