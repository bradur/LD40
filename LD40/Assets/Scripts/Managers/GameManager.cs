// Date   : 12.11.2017 22:58
// Project: Kajam 1
// Author : bradur

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager main;



    int ore = 0;
    int money = 0;

    private void Awake()
    {
        main = this;
    }

    public void PlayerGetOre(int tonsOfOre, Vector3 position)
    {
        ore += tonsOfOre;
        UIManager.main.AddOreEffect(tonsOfOre, position);

    }

    public void PlayerGetMoney(int newMoney)
    {
        money += newMoney;
        UIManager.main.Topup(newMoney);
    }

    void Start () {
       // Cursor.visible = false;
    }

    void Update () {

    }
}
