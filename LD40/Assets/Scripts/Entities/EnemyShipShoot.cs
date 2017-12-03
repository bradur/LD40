// Date   : 03.12.2017 15:51
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class EnemyShipShoot : MonoBehaviour {

    [SerializeField]
    [Range(0.5f, 100f)]
    private float speed = 2f;

    [SerializeField]
    [Range(1f, 10f)]
    private float lifetime = 1f;

    [SerializeField]
    [Range(0.01f, 5f)]
    private float spawnInterval = 0.5f;
    private float spawnTimer = 1f;

    private bool spawningProjectiles = false;

    [SerializeField]
    [Range(0.05f, 1f)]
    private float cannonLightDuration = 0.2f;
    [SerializeField]
    Transform leftCannon;
    [SerializeField]
    Transform rightCannon;

    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private GameObject leftCannonLight;

    [SerializeField]
    private GameObject rightCannonLight;

    void Start () {
    
    }

    public void StartFiring()
    {
        if (!spawningProjectiles)
        {
            spawningProjectiles = true;
        }
    }

    public void StopFiring()
    {
        leftCannonLight.SetActive(false);
        rightCannonLight.SetActive(false);
        spawnTimer = 0f;
        spawningProjectiles = false;
    }
    void Update () {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnInterval)
        {
            if (spawningProjectiles)
            {
                ProjectileManager.main.SpawnEnemyProjectile(leftCannon.position, leftCannon.up, leftCannon.rotation, rb2d.velocity);
                leftCannonLight.SetActive(true);
                ProjectileManager.main.SpawnEnemyProjectile(rightCannon.position, rightCannon.up, rightCannon.rotation, rb2d.velocity);
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
}
