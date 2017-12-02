// Date   : 02.12.2017 15:02
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    void Start () {
        originalColor = sr.color;
    }

    private Color originalColor;
    private bool laserIsHitting = false;
    [SerializeField]
    [Range(5, 50)]
    private int hitPoints = 20;
    private float isHittingTimer = 0f;

    void LateUpdate () {
        if (laserIsHitting)
        {
            isHittingTimer += Time.deltaTime;
            sr.color = Color.Lerp(originalColor, laseredColor, (isHittingTimer / hitPoints));
            if (isHittingTimer >= hitPoints)
            {
                laserIsHitting = false;
                StartDying();
            }
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
        animator.SetTrigger("Die");
    }

    public void Die()
    {
        Destroy(gameObject);
        // pay player
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
