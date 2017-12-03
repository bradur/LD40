// Date   : 02.12.2017 15:07
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{

    private RaycastHit2D rayHit;
    private LineRenderer line;

    private bool isCasting = false;
    [SerializeField]
    [Range(0.2f, 10f)]
    private float rayInterval = 0.2f;
    private float rayTimer = 0.2f;

    [SerializeField]
    private bool userIntervals = false;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (userIntervals)
        {
            rayTimer += Time.deltaTime;
            if (isCasting)
            {
                if (rayTimer > rayInterval)
                {
                    rayTimer = 0f;
                    CastLaser();
                }
            }
        }
        else if (isCasting)
        {
            CastLaser();
        }
    }

    Transform laserPosition;
    public void StartLaser(CannonPosition cannonPosition)
    {
        SoundManager.main.PlaySound(SoundType.LaserIsOn);
        laserPosition = cannonPosition.transform;
        isCasting = true;
    }

    private EnemyShip previousEnemy;
    private Asteroid previousAsteroid;
    [SerializeField]
    [Range(0.01f, 20f)]
    private float laserFuelCost = 0.05f;

    private float laserLength = 6f;
    private void CastLaser()
    {
        if (!GameManager.main.WithDrawFuel(laserFuelCost))
        {
            return;
        }
        Vector2 dir = transform.TransformDirection(Vector2.up);
        line.SetPosition(0, transform.position);
        RaycastHit2D[] results = new RaycastHit2D[] { };
        rayHit = Physics2D.Raycast(laserPosition.position, dir, laserLength, LayerMask.GetMask("Default", "SafeZone", "Enemy"));
        if (rayHit.collider != null)
        {
            SoundManager.main.PlaySound(SoundType.LaserHits);
        }
        else
        {
            SoundManager.main.StopSound(SoundType.LaserHits);
        }
        if (rayHit.collider != null && rayHit.collider.tag == "Asteroid")
        {
            previousAsteroid = rayHit.collider.gameObject.GetComponent<Asteroid>();
            previousAsteroid.GetHit(this);
            ProjectileManager.main.ShowLaserEffect(rayHit.point);
            line.SetPosition(1, rayHit.point);
        }
        else if (rayHit.collider != null && rayHit.collider.tag == "Enemy")
        {
            previousEnemy = rayHit.collider.gameObject.GetComponent<EnemyShip>();
            previousEnemy.GetHit(this);
            ProjectileManager.main.ShowLaserEffect(rayHit.point);
            line.SetPosition(1, rayHit.point);
        }
        else
        {
            if (previousAsteroid != null)
            {
                previousAsteroid.NotHittingAnymore();
                previousAsteroid = null;
            }
            if (previousEnemy != null)
            {
                previousEnemy.NotHittingAnymore();
                previousEnemy = null;
            }
            if (rayHit.collider != null && rayHit.collider.tag == "SafeZone")
            {
                line.SetPosition(1, rayHit.point);
            }
            else
            {
                line.SetPosition(1, (Vector2)transform.position + dir.normalized * laserLength);
            }
            ProjectileManager.main.HideLaserEffect();
        }
        if (!line.enabled)
        {
            line.enabled = true;
        }
    }

    public void StopLaser()
    {
        SoundManager.main.StopSound(SoundType.LaserHits);
        SoundManager.main.StopSound(SoundType.LaserIsOn);
        if (previousAsteroid != null)
        {
            previousAsteroid.NotHittingAnymore();
            previousAsteroid = null;
        }
        if (previousEnemy != null)
        {
            previousEnemy.NotHittingAnymore();
            previousEnemy = null;
        }
        ProjectileManager.main.HideLaserEffect();
        isCasting = false;
        line.enabled = false;
    }


}
