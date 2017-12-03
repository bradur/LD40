// Date   : 02.12.2017 09:09
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb2d;
    private Rigidbody rb;

    [SerializeField]
    [Range(0.1f, 300f)]
    private float rotationSpeed = 100;

    [SerializeField]
    [Range(0.2f, 50f)]
    private float moveSpeed = 1f;

    [SerializeField]
    [Range(0.2f, 30f)]
    private float moveSpeedBack = 1f;

    [SerializeField]
    [Range(0.4f, 50f)]
    private float maxVelocityMagnitude = 5f;

    [SerializeField]
    [Range(0.2f, 10f)]
    private float maxVelocityMagnitudeSafeZone = 1f;

    private float velocityMagnitudeMax = 0f;

    private bool is3D = false;


    private void Start()
    {
        velocityMagnitudeMax = maxVelocityMagnitude;
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb = GetComponent<Rigidbody>();
            is3D = true;
        }
    }

    [SerializeField]
    private GameObject backThruster;

    [SerializeField]
    private GameObject leftThruster;

    [SerializeField]
    private GameObject rightThruster;

    [SerializeField]
    private GameObject frontThrusterLeft;

    [SerializeField]
    private GameObject frontThrusterRight;

    [SerializeField]
    [Range(0.5f, 20f)]
    private float sideThrusterFuelCost = 0.5f;

    [SerializeField]
    [Range(0.5f, 20f)]
    private float frontThrusterFuelCost = 2f;

    [SerializeField]
    [Range(0.5f, 20f)]
    private float backThrusterFuelCost = 1f;

    private float axisTreshold = 0.05f;

    private void Update()
    {
        /*float horizontalRotation = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        transform.eulerAngles -= new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, horizontalRotation);*/
        if (is3D)
        {
            rb.AddTorque(transform.forward * -Input.GetAxis("Horizontal") * rotationSpeed, ForceMode.Force);
        }
        else
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            if (Mathf.Abs(horizontalAxis) <= axisTreshold)
            {
                leftThruster.SetActive(false);
                rightThruster.SetActive(false);
            }
            else
            {
                if (GameManager.main.WithDrawFuel(sideThrusterFuelCost))
                {
                    if (horizontalAxis > 0f)
                    {
                        leftThruster.SetActive(true);
                    }
                    else
                    {
                        rightThruster.SetActive(true);
                    }
                    rb2d.AddTorque(-Input.GetAxis("Horizontal") * rotationSpeed, ForceMode2D.Force);
                } else
                {
                    leftThruster.SetActive(false);
                    rightThruster.SetActive(false);
                }
            }
        }

        float verticalAxis = Input.GetAxis("Vertical");
        if (Mathf.Abs(verticalAxis) <= axisTreshold)
        {
            backThruster.SetActive(false);
            frontThrusterLeft.SetActive(false);
            frontThrusterRight.SetActive(false);
        }
        else if (verticalAxis > 0)
        {
            if (GameManager.main.WithDrawFuel(backThrusterFuelCost))
            {
                backThruster.SetActive(true);
                frontThrusterLeft.SetActive(false);
                frontThrusterRight.SetActive(false);
                if (is3D)
                {
                    if (rb.velocity.magnitude < velocityMagnitudeMax)
                    {
                        rb.AddRelativeForce(Vector2.up * moveSpeed, ForceMode.Force);
                    }

                }
                else
                {
                    if (rb2d.velocity.magnitude < velocityMagnitudeMax)
                    {
                        rb2d.AddRelativeForce(Vector2.up * moveSpeed, ForceMode2D.Force);
                    }
                }
            } else
            {
                backThruster.SetActive(false);
            }
        }
        else
        {
            if (GameManager.main.WithDrawFuel(frontThrusterFuelCost))
            {
                frontThrusterLeft.SetActive(true);
                frontThrusterRight.SetActive(true);
                backThruster.SetActive(false);
                if (rb2d.velocity.magnitude < velocityMagnitudeMax)
                {
                    rb2d.AddRelativeForce(-Vector2.up * moveSpeedBack, ForceMode2D.Force);
                }
            } else
            {
                frontThrusterLeft.SetActive(false);
                frontThrusterRight.SetActive(false);
            }
        }
    }


    public void EnterSafeZone()
    {
        /*SafeZone safeZone = collision.gameObject.GetComponent<SafeZone>();
        safeZone.PlayerEnter();*/
        rb2d.velocity = rb2d.velocity * 0.2f;
        velocityMagnitudeMax = maxVelocityMagnitudeSafeZone;

    }

    public void LeaveSafeZone()
    {
        velocityMagnitudeMax = maxVelocityMagnitude;
    }
}
