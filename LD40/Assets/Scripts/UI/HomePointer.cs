// Date   : 03.12.2017 19:07
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomePointer : MonoBehaviour
{

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private SpriteRenderer targetRenderer;


    public void Init(Transform target, Color color, SpriteRenderer renderer, Sprite icon, float ypos)
    {
        this.target = target;
        targetRenderer = renderer;
        imgComponent.color = color;
        imgComponent.sprite = icon;
        RectTransform rt = imgComponent.GetComponent<RectTransform>();
        Vector2 localPos = rt.localPosition;
        localPos.y = ypos;
        rt.localPosition = localPos;
        //txtComponent.color = color;
    }

    void Update()
    {
        if (target == null || targetRenderer == null)
        {
            Destroy(gameObject);
        }
        else if (!targetRenderer.isVisible)
        {
            if (!imgComponent.enabled)
            {
                Show();
            }
            transform.up = (Vector2)Camera.main.WorldToScreenPoint((Vector2)target.position) - (Vector2)transform.position;
        }
        else
        {
            if (imgComponent.enabled)
            {
                Hide();
            }
        }

    }

    public void Hide()
    {
        imgComponent.enabled = false;
        //txtComponent.enabled = false;
    }

    public void Show()
    {
        imgComponent.enabled = true;
        //txtComponent.enabled = true;
    }
}
