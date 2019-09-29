using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShot : TurretShootBase
{
    public Transform muzzleLeft;
    public Transform muzzleRight;
    public GameObject missilePrefab;
    private bool shootLeft = true;

    public virtual void Shoot(GameObject go)
    {
        if (shootLeft)
        {
            GameObject gpmisselgo = Instantiate(missilePrefab, muzzleLeft.transform.position, muzzleLeft.rotation);
        }

        else
        {
            GameObject gpmisselgo = Instantiate(missilePrefab, muzzleRight.transform.position, muzzleRight.rotation);
        }

        shootLeft = !shootLeft;
    }
}
