// Date   : 18.10.2017 23:19
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FollowCamera : MonoBehaviour
{


    [SerializeField]
    private bool followX = false;
    [SerializeField]
    private bool followY = false;

    [SerializeField]
    private bool followZ = false;

    [SerializeField]
    private Transform topBorder;

    [SerializeField]
    private Transform leftBorder;

    [SerializeField]
    private Transform depthBorder;


    [SerializeField]
    [Range(0f, 5f)]
    private float positionFollowSmoothTime = 0.5f;

    [SerializeField]
    private Transform target;

    private Vector3 newPosition;

    private Vector3 currentVelocity;

    private void Start()
    {
        //aspectRatio = Screen.width / Screen.height;
    }

    private void Update()
    {
        newPosition = transform.position;
        if (followX)
        {
            newPosition.x = Mathf.Clamp(target.position.x, leftBorder.position.x, -leftBorder.position.x);
        }
        if (followY)
        {
            newPosition.y = Mathf.Clamp(target.position.y, -topBorder.position.y, topBorder.position.y);
        }
        if (followZ)
        {
            newPosition.z = Mathf.Clamp(target.position.z, -depthBorder.position.z, depthBorder.position.z);
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            newPosition,
            ref currentVelocity,
            positionFollowSmoothTime
        );
    }

}
