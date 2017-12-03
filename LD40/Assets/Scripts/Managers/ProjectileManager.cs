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
    private ProjectilePool enemyProjectilePool;

    [SerializeField]
    [Range(0.5f, 100f)]
    private float speed = 2f;

    [SerializeField]
    [Range(0.5f, 100f)]
    private float enemySpeed = 2f;


    [SerializeField]
    [Range(1f, 10f)]
    private float lifeTime = 1f;

    [SerializeField]
    [Range(0.01f, 5f)]
    private float spawnInterval = 0.5f;
    private float spawnTimer = 1f;

    private bool spawningProjectiles = false;


    [SerializeField]
    private Transform leftCannon;

    [SerializeField]
    private Transform rightCannon;

    [SerializeField]
    private GameObject leftCannonLight;

    [SerializeField]
    private GameObject rightCannonLight;

    [SerializeField]
    [Range(0.05f, 1f)]
    private float cannonLightDuration;

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
                leftCannonLight.SetActive(true);
                SpawnProjectile(rightCannon.position, rightCannon.up, rightCannon.rotation);
                rightCannonLight.SetActive(true);
                spawnTimer = 0f;
            }
        }
        else if (spawnTimer > cannonLightDuration)
        {
            leftCannonLight.SetActive(false);
            rightCannonLight.SetActive(false);
        }
    }

    public void StopSpawningProjectiles()
    {
        leftCannonLight.SetActive(false);
        rightCannonLight.SetActive(false);
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

    public void SpawnProjectileNow()
    {
        SpawnProjectile(leftCannon.position, leftCannon.up, leftCannon.rotation);
        leftCannonLight.SetActive(true);
        SpawnProjectile(rightCannon.position, rightCannon.up, rightCannon.rotation);
        rightCannonLight.SetActive(true);
        spawnTimer = 0f;
    }

    [SerializeField]
    private Rigidbody2D playerRb2d;

    [SerializeField]
    Projectile projectilePrefab;
    public void SpawnProjectile(Vector3 position, Vector2 direction, Quaternion rotation)
    {
        Projectile newProjectile = pool.GetProjectile();
        newProjectile.transform.position = position;
        newProjectile.transform.rotation = rotation;
        //Projectile newProjectile = Instantiate(projectilePrefab, position, rotation);
        //newProjectile.gameObject.SetActive(true);
        newProjectile.Init(lifeTime, speed, direction, playerRb2d.velocity);
    }

    public void SpawnEnemyProjectile(Vector3 position, Vector2 direction, Quaternion rotation, Vector3 enemyShipVelocity)
    {
        Projectile newProjectile = enemyProjectilePool.GetProjectile();
        newProjectile.transform.position = position;
        newProjectile.transform.rotation = rotation;
        //Projectile newProjectile = Instantiate(projectilePrefab, position, rotation);
        //newProjectile.gameObject.SetActive(true);
        newProjectile.Init(lifeTime, enemySpeed, direction, enemyShipVelocity);
    }
    public void EnemyProjectileSleep(Projectile projectile)
    {
        enemyProjectilePool.Sleep(projectile);
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

