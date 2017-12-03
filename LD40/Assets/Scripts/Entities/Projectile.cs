// Date   : 02.12.2017 12:56
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{


    private bool isActive = false;
    public bool IsActive { get { return isActive; } }
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private bool enemyProjectile = false;

    private float lifeTimer = 0f;
    private float lifeTime = 5f;

    public void Deactivate()
    {
        isActive = false;
        rb2d.velocity = Vector3.zero;
        lifeTimer = 0f;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Die()
    {
        if (enemyProjectile)
        {
            ProjectileManager.main.EnemyProjectileSleep(this);
        }
        else
        {
            ProjectileManager.main.Sleep(this);
        }
    }

    private CannonPosition cannonPosition;
    public void Init(float lifeTime, float speed, Vector2 dir, Vector2 playerVelocity)
    {
        this.lifeTime = lifeTime;
        isActive = true;
        rb2d.velocity = playerVelocity;
        rb2d.AddForce(dir * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifeTime)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.gameObject.tag == "Asteroid")
        {
            collider2d.gameObject.GetComponent<Asteroid>().GetHit(this);
            Die();
        }
        else if (collider2d.gameObject.tag == "SafeZone")
        {
            Die();
        }
    }

}
