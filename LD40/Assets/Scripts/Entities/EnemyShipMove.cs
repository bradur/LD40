// Date   : 03.12.2017 14:58
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class EnemyShipMove : MonoBehaviour
{

    [SerializeField]
    private Transform playerTarget;

    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    [Range(0.05f, 10f)]
    private float moveInterval = 2f;
    private float moveTimer = 0f;

    [SerializeField]
    [Range(0.5f, 2f)]
    private float thrustDuration = 2f;
    private float thrustTimer = 0f;
    private bool isThrusting = false;

    [SerializeField]
    [Range(0.2f, 50f)]
    private float speed = 5f;

    [SerializeField]
    private bool onTheMove = false;

    [SerializeField]
    [Range(0.5f, 200f)]
    private float rotateSpeed = 0.2f;

    [SerializeField]
    [Range(0.4f, 50f)]
    private float maxVelocityMagnitude = 5f;
    [SerializeField]
    private GameObject backThrusterLeft;
    [SerializeField]
    private GameObject backThrusterRight;

    [SerializeField]
    private EnemyShipShoot enemyShipShoot;

    void Start()
    {

    }

    private void FixedUpdate()
    {
        Vector3 dir = playerTarget.position - transform.position;
        //get the angle between transform.forward and target delta
        float angleDiff = Vector3.Angle(transform.up, dir);

        // get its cross product, which is the axis of rotation to
        // get from one vector to the other
        Vector3 cross = Vector3.Cross(transform.up, dir);

        // apply torque along that axis according to the magnitude of the angle.
        rb2d.AddTorque(cross.z * angleDiff * rotateSpeed);

    }

    void Update()
    {
        if (isThrusting)
        {
            thrustTimer += Time.deltaTime;
            if (thrustTimer > thrustDuration)
            {
                thrustTimer = 0f;
                isThrusting = false;
                onTheMove = true;
                backThrusterLeft.SetActive(false);
                backThrusterRight.SetActive(false);
                enemyShipShoot.StopFiring();
            }
            else
            {
                /*Vector2 direction = playerTarget.position - transform.position;
                direction.Normalize();*/
                if (rb2d.velocity.magnitude < maxVelocityMagnitude)
                {
                    rb2d.AddRelativeForce(Vector2.up * speed, ForceMode2D.Force);
                }
            }
        }
        if (onTheMove)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer > moveInterval)
            {
                moveTimer = 0f;
                onTheMove = false;
                isThrusting = true;
                backThrusterLeft.SetActive(true);
                backThrusterRight.SetActive(true);
                enemyShipShoot.StartFiring();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("Hit enemy!");
        }
    }
}
