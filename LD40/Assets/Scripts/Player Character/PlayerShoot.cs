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
    Right
}

[System.Serializable]
public class CannonPosition : System.Object
{
    public Cannon cannon;
    public Transform transform;
}

public class PlayerShoot : MonoBehaviour {

    [SerializeField]
    private List<CannonPosition> cannonPositions = new List<CannonPosition>();

    void Start () {
    
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

    void Update () {
        if (KeyManager.main.GetKeyDown(Action.ShootCannon))
        {
            ProjectileManager.main.StartSpawningProjectiles(GetCannonPosition(Cannon.Left), GetCannonPosition(Cannon.Right));
        }
        if (KeyManager.main.GetKeyUp(Action.ShootCannon))
        {
            ProjectileManager.main.StopSpawningProjectiles();
        }
    }
}
