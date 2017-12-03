// Date   : 03.12.2017 19:07
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomePointer : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private Transform target;


    void Start ()
    {
    }

    void Update()
    {
        transform.up = Camera.main.WorldToScreenPoint(target.position) - transform.position;
    }

    public void Hide()
    {
        imgComponent.enabled = false;
    }

    public void Show()
    {
        imgComponent.enabled = true;
    }
}
