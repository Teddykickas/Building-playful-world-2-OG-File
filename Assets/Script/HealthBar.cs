using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : LivingEntity
{
	public float Health;

    //public GameObject Endscherm;
	
	public void Update () {
		healthBar.fillAmount = Health / 100.0f;
		
	}

	public void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag ("Enemy")) {
			Health -= startingHealth;
		}
        //if (collision.gameObject.CompareTag("Enemy") && Health <= 0)
        //{
        //    Endscherm.SetActive(true);
        //}
   
    }


}
