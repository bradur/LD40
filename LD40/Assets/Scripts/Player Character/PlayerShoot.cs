// Date   : 02.12.2017 13:25
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum Cannon
{
    None,
    Left,
    Right,
    Laser
}

[System.Serializable]
public class CannonPosition : System.Object
{
    public Cannon cannon;
    public Transform transform;
}


public class PlayerShoot : MonoBehaviour
{

    [SerializeField]
    private List<CannonPosition> cannonPositions = new List<CannonPosition>();

    void Start()
    {

    }

    bool weaponsDisabled = false;
    public void DisableWeapons()
    {
        weaponsDisabled = true;
        ProjectileManager.main.StopLaser();
        ProjectileManager.main.StopSpawningProjectiles();
    }


    public void EnableWeapons()
    {
        weaponsDisabled = false;
    }
    CannonPosition GetCannonPosition(Cannon cannon)
    {
        foreach (CannonPosition cannonPosition in cannonPositions)
        {
            if (cannon == cannonPosition.cannon)
            {
                return cannonPosition;
            }
        }
        return null;
    }

    void Update()
    {
        if (!weaponsDisabled)
        {
            /*if (KeyManager.main.GetKeyDown(Action.ShootCannon))
            {
                //ProjectileManager.main.StartSpawningProjectiles();
            }*/
            if (KeyManager.main.GetKeyUp(Action.ShootCannon))
            {
                //ProjectileManager.main.StopSpawningProjectiles();
                ProjectileManager.main.SpawnProjectileNow();
            }
            if (KeyManager.main.GetKeyDown(Action.FireLaser))
            {
                ProjectileManager.main.StartLaser(GetCannonPosition(Cannon.Laser));
            }
            if (KeyManager.main.GetKeyUp(Action.FireLaser))
            {
                ProjectileManager.main.StopLaser();
            }
        }
    }
}
