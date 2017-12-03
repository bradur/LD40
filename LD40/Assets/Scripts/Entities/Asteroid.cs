// Date   : 02.12.2017 15:02
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    void Start () {
        originalColor = sr.color;
    }

    [SerializeField]
    [Range(1, 60)]
    private int tonsOfOre = 5;

    private Color originalColor;
    private bool laserIsHitting = false;
    private bool isDying = false;

    private float isHittingTimer = 0f;

    void LateUpdate () {
        if (laserIsHitting && !isDying)
        {
            isHittingTimer += Time.deltaTime;
            sr.color = Color.Lerp(originalColor, laseredColor, (isHittingTimer / tonsOfOre));
            if (isHittingTimer >= tonsOfOre)
            {
                laserIsHitting = false;
                StartDying();
            }
        } else
        {
            sr.color = Color.Lerp(originalColor, laseredColor, (isHittingTimer / tonsOfOre));
        }
    }


    [SerializeField]
    private Color laseredColor;
    [SerializeField]
    private SpriteRenderer sr;

    [SerializeField]
    private Animator animator;
    private void StartDying ()
    {
        isDying = true;
        GameManager.main.SpawnEnemies(tonsOfOre);
        GameManager.main.PlayerGetOreEffect(tonsOfOre, transform.position);
        animator.SetTrigger("Die");
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void GetHit(Projectile projectile)
    {

    }

    public void GetHit(Laser laser)
    {
        laserIsHitting = true;
    }

    public void NotHittingAnymore()
    {
        laserIsHitting = false;
    }
}
