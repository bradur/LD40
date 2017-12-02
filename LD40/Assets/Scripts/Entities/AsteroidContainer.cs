// Date   : 03.12.2017 00:35
// Project: Being Plundered
// Author : bradur

using UnityEngine;
using System.Collections;

public class AsteroidContainer : MonoBehaviour {

    [SerializeField]
    private Asteroid asteroid;


    public void Kill()
    {
        asteroid.Die();
    }

}
