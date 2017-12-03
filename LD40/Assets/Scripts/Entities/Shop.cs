// Date   : 03.12.2017 14:10
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private UIShop uiShop;

    [SerializeField]
    [Range(0.5f, 10f)]
    private float distanceToOpenShop = 4f;


    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance <= distanceToOpenShop)
        {
            
            if (!uiShop.isActiveAndEnabled)
            {
                uiShop.gameObject.SetActive(true);
            }
            if (!uiShop.IsOpen)
            {
                uiShop.Open();
            }
        }
        else if (distance >= distanceToOpenShop)
        {
            if (uiShop.IsOpen)
            {
                uiShop.Close();
            }
        }
    }

}
