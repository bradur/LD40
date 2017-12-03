// Date   : 03.12.2017 12:11
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDOre : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;
    private int currentValue = 0;

    public void SetValue(int value)
    {
        currentValue = value;
        txtComponent.text = value + "";
    }

    public void AddToValue(int addition)
    {
        currentValue += addition;
        txtComponent.text = currentValue + "";
    }
    public void Withdraw(int removal)
    {
        currentValue -= removal;
        txtComponent.text = currentValue + "";
    }
}
