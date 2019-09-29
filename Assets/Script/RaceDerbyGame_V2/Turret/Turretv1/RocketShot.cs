using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShot : TurretShootBase
{

    public virtual void Shoot(GameObject go)
    {
        Debug.Log("shot from Single Shot");
    }

}