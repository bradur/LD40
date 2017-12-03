// Date   : 12.11.2017 22:58
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum DamageType
{
    None,
    EnemyProjectile
}

public class GameManager : MonoBehaviour
{

    public static GameManager main;



    [SerializeField]
    [Range(0, 1000)]
    int ore = 0;
    [SerializeField]
    [Range(0, 10000)]
    int money = 0;

    float fuel;

    [SerializeField]
    [Range(100, 20000)]
    float maxFuel = 100;


    [SerializeField]
    [Range(10, 1000)]
    private int hitpoints = 100;
    private int currentHealth;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        // Cursor.visible = false;
        UIManager.main.SetMoney(money);
        UIManager.main.SetOre(ore);
        fuel = maxFuel;
        currentHealth = hitpoints;
        UIManager.main.SetPower(fuel, maxFuel);
        UIManager.main.SetInitialHealth(hitpoints);
    }

    public void GameOver()
    {
        Debug.Log("You died!");
    }

    public void PlayerTakeDamage(DamageType damageType)
    {
        if (damageType == DamageType.EnemyProjectile)
        {
            currentHealth -= 10;
            if (currentHealth <= 0)
            {
                GameOver();
            }
        }
        UIManager.main.PlayerTakeDamage(10);
    }

    public int HitpointsMissing ()
    {
        return hitpoints - currentHealth;
    }

    public void RepairShip(int amount)
    {
        currentHealth += amount;
        UIManager.main.RepairHealth(amount);
    }

    public bool WithDrawFuel(float amount)
    {
        if (fuel >= amount)
        {
            fuel -= amount;
            UIManager.main.DrainPower(amount);
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool WithdrawResource(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Money)
        {
            if (money >= amount)
            {
                money -= amount;
                UIManager.main.Withdraw(amount);
                return true;
            }
        }
        else if (resourceType == ResourceType.Ore)
        {
            if (ore >= amount)
            {
                ore -= amount;
                UIManager.main.WithdrawOre(amount);
                return true;
            }
        }
        return false;
    }


    public bool GainResource(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Money)
        {
            PlayerGetMoney(amount);
        }
        else if (resourceType == ResourceType.Ore)
        {
            PlayerGetOre(amount);
        } else if (resourceType == ResourceType.Power)
        {
            PlayerGetPower(amount);
        }
        return false;
    }

    public void PlayerGetOreEffect(int tonsOfOre, Vector3 position)
    {
        PlayerGetOre(tonsOfOre);
        UIManager.main.AddOreEffect(tonsOfOre, position);
    }

    public void PlayerGetPower(int amount)
    {
        fuel += amount;
        UIManager.main.AddPower(amount);
    }

    public void PlayerGetOre(int tonsOfOre)
    {
        ore += tonsOfOre;
    }

    public void PlayerGetMoney(int newMoney)
    {
        money += newMoney;
        UIManager.main.Topup(newMoney);
        SoundManager.main.PlaySound(SoundType.GetMoney);
    }

    void Update()
    {

    }
}
