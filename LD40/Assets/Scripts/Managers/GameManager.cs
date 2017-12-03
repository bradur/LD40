// Date   : 12.11.2017 22:58
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        UIManager.main.SetPower(fuel, maxFuel);
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
