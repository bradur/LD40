// Date   : 02.12.2017 12:50
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ProjectileManager : MonoBehaviour
{



    public static ProjectileManager main;

    [SerializeField]
    private ProjectilePool pool;

    [SerializeField]
    [Range(0.5f, 100f)]
    private float speed = 2f;

    [SerializeField]
    [Range(1f, 10f)]
    private float lifeTime = 1f;

    [SerializeField]
    [Range(0.2f, 5f)]
    private float spawnInterval = 0.5f;
    private float spawnTimer = 1f;

    private bool spawningProjectiles = false;

    private CannonPosition leftCannonPosition;
    private CannonPosition rightCannonPosition;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {

    }

    void Update()
    {

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnInterval)
        {
            if (spawningProjectiles)
            {
                SpawnProjectile(leftCannonPosition);
                SpawnProjectile(rightCannonPosition);
                spawnTimer = 0f;
            }
        }
    }

    public void StopSpawningProjectiles()
    {
        spawningProjectiles = false;
        spawnTimer = 0f;
    }

    public void StartSpawningProjectiles(CannonPosition leftCannonPosition, CannonPosition rightCannonPosition)
    {
        if (!spawningProjectiles)
        {
            this.leftCannonPosition = leftCannonPosition;
            this.rightCannonPosition = rightCannonPosition;
            spawningProjectiles = true;
        }
    }

    public void SpawnProjectile(CannonPosition cannonPosition)
    {
        Projectile newProjectile = pool.GetProjectile();
        newProjectile.Init(cannonPosition, lifeTime, speed);
    }

    public void Sleep(Projectile projectile)
    {
        pool.Sleep(projectile);
    }
}

