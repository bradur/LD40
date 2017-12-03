// Date   : 03.12.2017 16:18
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDHealth : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    private RectTransform imgRect;
    private int maxHealth;
    private int health;

    private void Start()
    {
        imgRect = imgComponent.GetComponent<RectTransform>();
    }

    public void SetInitialHealth(int value)
    {
        maxHealth = value;
        health = value;
        RenderHealth();
    }

    public void TakeDamage(int value)
    {
        health -= value;
        RenderHealth();
    }

    public void Repair(int value)
    {
        health += value;
        RenderHealth();
    }

    void RenderHealth()
    {
        Vector2 tempScale = imgRect.localScale;
        tempScale.x = 1.0f * health / maxHealth;
        imgRect.localScale = tempScale;
        txtComponent.text = (1.0f * health / maxHealth * 100.0f).ToString("F2");
    }

}
