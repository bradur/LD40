// Date   : 03.12.2017 16:52
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private EnemyShip enemyPrefab;

    [SerializeField]
    private Transform playerTransform;

    private float radiusMax = 8f;
    private float radiusMin = 6f;

    private float warpLength = 1.5f;

    private float enemySpawnInterval = 0.3f;
    private float enemySpawnTimer = 0f;

    private bool spawningEnemies = false;

    private int numberOfEnemiesToSpawn = 0;

    public void SpawnEnemies(int number)
    {
        enemySpawnTimer = 0f;
        numberOfEnemiesToSpawn = number;
        spawningEnemies = true;
    }

    public void SpawnAnEnemy()
    {
        Vector2 newPos = Random.insideUnitCircle * Random.Range(radiusMin, radiusMax);
        EnemyShip newEnemy = Instantiate(enemyPrefab);
        newEnemy.gameObject.SetActive(true);
        newEnemy.Init(
            (Vector2)playerTransform.position + (newPos * warpLength),
            (Vector2)playerTransform.position + newPos
        );
        SoundManager.main.PlaySound(SoundType.PirateWarning);
        //Vector2 direction = newPos - (Vector2)playerTransform.position;
    }

    void Start()
    {

    }

    void Update()
    {
        if (spawningEnemies)
        {
            if (numberOfEnemiesToSpawn > 0)
            {
                enemySpawnTimer += Time.deltaTime;
                if (enemySpawnTimer > enemySpawnInterval)
                {
                    enemySpawnTimer = 0f;
                    numberOfEnemiesToSpawn -= 1;
                    SpawnAnEnemy();
                }
            }
            else
            {
                spawningEnemies = false;
            }
        }
    }
}
