// Date   : 03.12.2017 10:56
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class PullWhenCloseBy : MonoBehaviour {

    [SerializeField]
    private GameObject target;

    private Rigidbody targetRb;
    private Rigidbody2D targetRb2d;
    private bool is3D = false;

    [SerializeField]
    [Range(0.1f, 100f)]
    private float strength = 1f;

    [SerializeField]
    [Range(0.5f, 10f)]
    private float distanceToStartPulling = 4f;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float distanceToStopPulling = 2f;

    [SerializeField]
    [Range(0.2f, 10f)]
    private float pushStrength = 0.2f;

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
        if (distance <= distanceToStopPulling)
        {
            Vector2 force = target.transform.position - transform.position;
            force.Normalize();
            targetRb2d.AddForce(force * targetRb2d.mass * pushStrength / force.magnitude);
        }
        else if (distanceToStartPulling >= distance)
        {
            Vector2 force = transform.position - target.transform.position;
            force.Normalize();

            if (is3D)
            {
                targetRb.AddForce(force * targetRb.mass * strength / force.magnitude);
            }
            else
            {
                targetRb2d.AddForce(force * targetRb2d.mass * strength / force.magnitude);
            }
        }
    }
}
