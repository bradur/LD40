// Date   : 03.12.2017 12:11
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDPower : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    private RectTransform imgRect;

    private float maxValue = 100f;

    private void Start()
    {
        currentValue = maxValue;
        imgRect = imgComponent.GetComponent<RectTransform>();
    }

    private float currentValue;

    public void SetValue(float value, float maxAmount)
    {
        maxValue = maxAmount;
        currentValue = value;
        SetScale();
    }

    public void AddToValue(float addition)
    {
        currentValue += addition;
        SetScale();
    }
    public void Withdraw(float removal)
    {
        currentValue -= removal;
        SetScale();
    }

    void SetScale()
    {
        Vector2 tempScale = imgRect.localScale;
        tempScale.x = currentValue / maxValue;
        imgRect.localScale = tempScale;

    }
}
