using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public Transform weaponHoldTurret;//Emty game object as placeholder when picked up gun
    public Gun startingGun;
    Gun equippedGun;//var to a other game script 

    private void Start()
    {
        if (startingGun != null)
        {
            EquipGun(startingGun);
        }
    }

    public void EquipGun(Gun gunToEquip)//statement when you have or dont have a weapon
    { 
        if (equippedGun != null) 
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip, weaponHoldTurret.position, weaponHoldTurret.rotation) as Gun;
        equippedGun.transform.parent = weaponHoldTurret;
    }

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
