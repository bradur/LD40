// Date   : 02.12.2017 12:56
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {


    private bool isActive = false;
    public bool IsActive { get { return isActive; } }
    [SerializeField]
    private Rigidbody rb;

    private float lifeTimer = 0f;
    private float lifeTime = 5f;

    public void Deactivate()
    {
        isActive = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        lifeTimer = 0f;
    }

    public void Activate()
    {
        isActive = true;
        rb.isKinematic = false;
    }

    public void Die()
    {
        ProjectileManager.main.Sleep(this);
    }

    public void Init(CannonPosition cannonPosition, float lifeTime, float speed)
    {
        transform.position = cannonPosition.transform.position;
        transform.rotation = cannonPosition.transform.rotation;
        this.lifeTime = lifeTime;
        Shoot(speed);
        rb.isKinematic = false;
        isActive = true;
    }

    void Shoot(float speed)
    {
        rb.AddForce(transform.up * speed, ForceMode.Impulse);
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifeTime)
        {
            Die();
        }
    }

}
