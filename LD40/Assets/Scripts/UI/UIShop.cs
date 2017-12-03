// Date   : 03.12.2017 14:00
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum ResourceType
{
    None,
    Power,
    Ore,
    Money,
    Health
}
public class UIShop : MonoBehaviour
{

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private ResourceType selling;
    [SerializeField]
    private ResourceType buying;


    [SerializeField]
    [Range(0, 10000)]
    private int cost;

    [SerializeField]
    [Range(0, 10000)]
    private int amount;

    public bool IsOpen { get { return isOpen; } }

    private bool isOpen;

    public void Open()
    {
        animator.SetTrigger("Open");
        isOpen = true;
    }

    public void Buy()
    {
        if (GameManager.main.CanGainResource(selling, amount) && GameManager.main.WithdrawResource(buying, cost))
        {
            GameManager.main.GainResource(selling, amount);
        }
    }

    [SerializeField]
    private bool allowContinuousSelling = false;
    private void Update()
    {
        if (isOpen)
        {
            if (allowContinuousSelling)
            {
                if (KeyManager.main.GetKey(Action.Buy))
                {
                    Buy();
                }
            } else
            {
                if (KeyManager.main.GetKeyDown(Action.Buy))
                {
                    Buy();
                }
            }
        }
    }

    public void Close()
    {
        animator.SetTrigger("Close");
        isOpen = false;
    }

}
