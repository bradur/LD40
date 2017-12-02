// Date   : 02.12.2017 10:20
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class PullWhenFarEnough : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D targetRb2d;

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

    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, targetRb2d.position);
        float pullStrength;
        if (distance + bufferLength >= radius)
        {
            if (distance < radius) {
                pullStrength = strength * ( Mathf.Abs(radius - (bufferLength + distance)) / bufferLength );
            } else
            {
                pullStrength = strength;
            }
            Rigidbody2D rigidBody2D = targetRb2d.GetComponent<Rigidbody2D>();
            Vector2 force = transform.position - targetRb2d.transform.position;
            force.Normalize();

            rigidBody2D.AddForce(force * rigidBody2D.mass * pullStrength / force.magnitude);
        }

    }
}
