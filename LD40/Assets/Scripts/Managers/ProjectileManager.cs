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


    [SerializeField]
    private Transform leftCannon;

    [SerializeField]
    private Transform rightCannon;

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
                SpawnProjectile(leftCannon.position, leftCannon.up, leftCannon.rotation);
                SpawnProjectile(rightCannon.position, rightCannon.up, rightCannon.rotation);
                spawnTimer = 0f;
            }
        }
    }

    public void StopSpawningProjectiles()
    {
        spawningProjectiles = false;
        spawnTimer = 0f;
    }

    public void StartSpawningProjectiles()
    {
        if (!spawningProjectiles)
        {
            spawningProjectiles = true;
        }
    }

    [SerializeField]
    Projectile projectilePrefab;
    public void SpawnProjectile(Vector3 position, Vector2 direction, Quaternion rotation)
    {
        Projectile newProjectile = pool.GetProjectile();
        newProjectile.transform.position = position;
        newProjectile.transform.rotation = rotation;
        //Projectile newProjectile = Instantiate(projectilePrefab, position, rotation);
        //newProjectile.gameObject.SetActive(true);
        newProjectile.Init(lifeTime, speed, direction);
    }

    [SerializeField]
    private Laser laser;
    private bool laserIsOn = false;
    public void StartLaser(CannonPosition cannonPosition)
    {
        if (!laserIsOn)
        {
            laserIsOn = true;
            laser.StartLaser(cannonPosition);
        }
    }
    public void StopLaser()
    {
        laser.StopLaser();
        laserIsOn = false;
    }

    public void Sleep(Projectile projectile)
    {
        pool.Sleep(projectile);
        //Destroy(projectile);
    }

    [SerializeField]
    private GameObject laserEffect;
    public void ShowLaserEffect(Vector3 position)
    {
        position.z = laserEffect.transform.position.z;
        laserEffect.transform.position = position;
        laserEffect.SetActive(true);
    }

    public void HideLaserEffect()
    {
        laserEffect.SetActive(false);
    }
}

