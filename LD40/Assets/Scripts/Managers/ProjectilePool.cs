// Date   : 03.11.2017 19:47
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{

    private List<Projectile> currentProjectiles;
    private List<Projectile> backupProjectiles;

    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private int poolSize = 20;

    [SerializeField]
    private bool spawnMore = false;

    [SerializeField]
    private Transform poolContainer;

    private int projectileCount = 1;

    void Start()
    {
        currentProjectiles = new List<Projectile>();
        backupProjectiles = new List<Projectile>();
        for (int i = 0; i < poolSize; i += 1)
        {
            backupProjectiles.Add(Spawn());
        }
    }

    private Projectile Spawn()
    {
        Projectile newProjectile = Instantiate(projectilePrefab);
        newProjectile.name = "Projectile " + projectileCount;
        projectileCount += 1;
        newProjectile.transform.SetParent(poolContainer, true);
        return newProjectile;
    }

    public void Sleep(Projectile projectile)
    {
        currentProjectiles.Remove(projectile);
        projectile.Deactivate();
        projectile.transform.SetParent(poolContainer, true);
        projectile.gameObject.SetActive(false);
        backupProjectiles.Add(projectile);
    }

    public Projectile GetProjectile ()
    {
        return WakeUp();
    }

    private Projectile WakeUp()
    {
        if (backupProjectiles.Count <= 2)
        {
            if (spawnMore)
            {
                backupProjectiles.Add(Spawn());
            }
        }
        Projectile newProjectile = null;
        if (backupProjectiles.Count > 0)
        {
            newProjectile = backupProjectiles[0];
            backupProjectiles.RemoveAt(0);
            newProjectile.gameObject.SetActive(true);
            newProjectile.Activate();
            currentProjectiles.Add(newProjectile);
        }
        return newProjectile;
    }

    public List<Projectile> GetCurrentProjectiles()
    {
        return currentProjectiles;
    }

    void Update()
    {

    }
}
