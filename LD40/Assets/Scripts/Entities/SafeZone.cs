// Date   : 03.12.2017 11:11
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class SafeZone : MonoBehaviour
{


    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float distanceToAllowMovement = 5f;

    [SerializeField]
    private PlayerShoot playerShoot;
    private bool playerIsHere = false;
    public void PlayerEnter()
    {
        SoundManager.main.SwitchToSafeZone();
        playerIsHere = true;
        playerShoot.DisableWeapons();
        playerMovement.EnterSafeZone();
    }

    void Start()
    {

    }

    void Update()
    {
        if (playerIsHere)
        {
            float distance = Vector2.Distance(transform.position, playerMovement.transform.position);
            if (distance > distanceToAllowMovement)
            {
                playerIsHere = false;
                playerMovement.LeaveSafeZone();
                SoundManager.main.SwitchToNormal();
                playerShoot.EnableWeapons();
            }
            else if (!SoundManager.main.SafeZone)
            {
                SoundManager.main.SwitchToSafeZone();
            }
        }
        else
        {
            float distance = Vector2.Distance(transform.position, playerMovement.transform.position);
            if (distance < distanceToAllowMovement)
            {
                PlayerEnter();
            }
            else if (SoundManager.main.SafeZone)
            {
                SoundManager.main.SwitchToNormal();
            }
        }
    }
}
