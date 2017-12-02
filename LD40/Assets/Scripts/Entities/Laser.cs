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
        laserPosition = cannonPosition.transform;
        isCasting = true;
    }

    private Asteroid previousAsteroid;
    private void CastLaser()
    {
        Vector2 dir = transform.TransformDirection(Vector2.up);
        line.SetPosition(0, transform.position);
        RaycastHit2D[] results = new RaycastHit2D[] { };
        rayHit = Physics2D.Raycast(laserPosition.position, dir, 10f, LayerMask.GetMask("Default"));
        if (rayHit.collider != null && rayHit.collider.tag == "Asteroid")
        {
            previousAsteroid = rayHit.collider.gameObject.GetComponent<Asteroid>();
            previousAsteroid.GetHit(this);
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
            line.SetPosition(1, (Vector2)transform.position + dir.normalized * 10f);
            ProjectileManager.main.HideLaserEffect();
        }
        if (!line.enabled)
        {
            line.enabled = true;
        }
    }

    public void StopLaser()
    {
        ProjectileManager.main.HideLaserEffect();
        isCasting = false;
        line.enabled = false;
    }


}
