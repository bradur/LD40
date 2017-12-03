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
    private int maxHealth;

    [SerializeField]
    private GameObject trailLeft;
    [SerializeField]
    private GameObject trailRight;

    [SerializeField]
    private Color pointerColor;

    [SerializeField]
    private Sprite pointerIcon;

    public void Init(Vector2 position, Vector2 target)
    {
        maxHealth = health;
        transform.position = position;
        this.target = target;
        originalColor = sr.color;
        transform.up = (target - position).normalized;
        UIManager.main.CreatePointer(transform, pointerColor, sr, pointerIcon, 220);
        WarpIn();
    }

    public void WarpIn()
    {
        isWarping = true;
        //animator.SetTrigger("Warp");
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

    private bool laserIsHitting = false;
    private float isHittingTimer = 0f;
    private float isHittingInterval = 0.5f;

    private int laserDamage = 15;

    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Color laseredColor;
    private Color originalColor;

    void LateUpdate()
    {
        if (laserIsHitting)
        {
            isHittingTimer += Time.deltaTime;
            sr.color = Color.Lerp(originalColor, laseredColor, (maxHealth - health) / (maxHealth * 1.0f));
            if (isHittingTimer >= isHittingInterval)
            {
                health -= laserDamage;
                isHittingTimer = 0f;
            }
        }
        else
        {
            sr.color = Color.Lerp(originalColor, laseredColor, (maxHealth - health) / (maxHealth * 1.0f));
        }
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        SoundManager.main.PlaySound(SoundType.EnemyDies);
    }

    public void GetHit(Laser laser)
    {
        laserIsHitting = true;
    }

    public void NotHittingAnymore()
    {
        laserIsHitting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            collision.gameObject.GetComponent<Projectile>().Die();
            health -= GameManager.main.GetPlayerProjectileDamage();
            SoundManager.main.PlaySound(SoundType.EnemyGotHit);
            if (health <= 0)
            {
                Die();
            }
        }
    }
}
