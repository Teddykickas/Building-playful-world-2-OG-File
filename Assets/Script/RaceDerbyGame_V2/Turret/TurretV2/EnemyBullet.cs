using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float destroyDelay = 5f;

    void Start()//destroy gameobject enemy bullet
    {
        Destroy(gameObject, destroyDelay);
    }
}