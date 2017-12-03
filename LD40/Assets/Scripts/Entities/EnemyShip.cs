// Date   : 03.12.2017 16:53
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour
{

    [SerializeField]
    private EnemyShipMove enemyMove;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    [Range(1f, 10f)]
    private float warpSpeed;

    [SerializeField]
    [Range(0.2f, 2f)]
    private float warpDuration = 1f;

    private float warpTimer = 0f;

    [SerializeField]
    private bool isWarping = false;

    private Vector2 target;

    [SerializeField]
    [Range(10, 200)]
    private int health = 100;

    [SerializeField]
    private GameObject trailLeft;
    [SerializeField]
    private GameObject trailRight;

    public void Init(Vector2 position, Vector2 target)
    {
        transform.position = position;
        this.target = target;
        transform.up = (target - position).normalized;
        WarpIn();
    }

    public void WarpIn()
    {
        isWarping = true;
        animator.SetTrigger("Warp");
    }

    public void StartMoving()
    {
        trailLeft.SetActive(false);
        trailRight.SetActive(false);
        animator.enabled = false;
        enemyMove.StartMoving();
    }

    void Start()
    {

    }

    void Update()
    {
        if (isWarping)
        {
            warpTimer += Time.deltaTime;
            float distance = Vector2.Distance(transform.position, target);
            if (warpTimer > warpDuration)
            {
                isWarping = false;
                transform.position = target;
                StartMoving();
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, target, warpTimer / warpDuration);
            }

        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            collision.gameObject.GetComponent<Projectile>().Die();
            health -= GameManager.main.GetPlayerProjectileDamage();
            if (health <= 0)
            {
                Die();
            }
        }
    }
}
