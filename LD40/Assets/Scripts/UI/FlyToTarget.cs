// Date   : 03.12.2017 12:36
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlyToTarget : MonoBehaviour {

    private RectTransform target;

    [SerializeField]
    [Range(1f, 200f)]
    private float speed = 20f;

    private bool isMoving = false;
    private Vector2 direction;
    private RectTransform rt;
    private int value;

    public void Init(RectTransform target, Vector2 position, int value)
    {
        this.value = value;
        rt = GetComponent<RectTransform>();
        rt.position = position;
        this.target = target;
        direction = target.position - rt.position;
        direction.Normalize();
        isMoving = true;
    }

    void Update () {
        if(isMoving)
        {
            float distance = Vector2.Distance(rt.position, target.position);
            if (distance <= (speed + 1f))
            {
                isMoving = false;
                Die();
            } else
            {
                rt.Translate(direction * speed);
            }
        }
    }

    public void Die()
    {
        UIManager.main.AddOre(value);
        Destroy(gameObject);
    }
}
