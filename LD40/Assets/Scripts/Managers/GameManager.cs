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

    private int playerDamage = 10;

    public int GetPlayerProjectileDamage()
    {
        return playerDamage;
    }

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

    [SerializeField]
    private Transform playerTransform;
    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    [SerializeField]
    private EnemyManager enemyManager;
    public void SpawnEnemies(int newOre)
    {
        int enemyCount = ((ore * 1000) + (newOre * 1000) + money) / 10000;
        if (enemyCount < 1)
        {
            enemyCount = 1;
        }
        enemyManager.SpawnEnemies(enemyCount);
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

    public int HitpointsMissing()
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


    public bool CanGainResource(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Power)
        {
            if (fuel >= maxFuel)
            {
                return false;
            }
            return true;
        } else if (resourceType == ResourceType.Health)
        {
            if (currentHealth >= hitpoints)
            {
                return false;
            }
        }
        return true;
    }

    public bool GainResource(ResourceType resourceType, int amount)
    {
        if (resourceType == ResourceType.Money)
        {
            PlayerGetMoney(amount);
            return true;
        }
        else if (resourceType == ResourceType.Ore)
        {
            PlayerGetOre(amount);
        }
        else if (resourceType == ResourceType.Power)
        {
            if ((fuel + amount) > maxFuel)
            {
                PlayerSetPower(maxFuel);
                return false;
            }
            PlayerGetPower(amount);
            return true;
        }
        else if (resourceType == ResourceType.Health)
        {
            if ((currentHealth + amount) > hitpoints)
            {
                PlayerSetHealth(hitpoints);
                return false;
            }
            RepairShip(amount);
            return true;
        }
        return false;
    }

    void PlayerSetHealth(int value)
    {
        currentHealth = value;
        UIManager.main.SetInitialHealth(value);
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

    public void PlayerSetPower(float amount)
    {
        fuel = amount;
        UIManager.main.SetPower(amount, maxFuel);
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
