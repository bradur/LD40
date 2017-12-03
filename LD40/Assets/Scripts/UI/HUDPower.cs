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

    private float currentValue;

    public void SetValue(float value)
    {
        currentValue = value;
        txtComponent.text = value + "";
    }

    public void AddToValue(float addition)
    {
        currentValue += addition;
        txtComponent.text = currentValue + "";
    }
    public void Withdraw(float removal)
    {
        currentValue -= removal;
        txtComponent.text = currentValue + "";
    }
}
