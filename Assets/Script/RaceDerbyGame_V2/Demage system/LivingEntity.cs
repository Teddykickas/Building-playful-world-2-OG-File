using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour, IDamageable {

    public Image healthBar;

    public float startingHealth;
    protected float health;//protected limits the the class in the inspecter
    protected bool dead;

    public virtual event System.Action OnDeath;//event for the enemy

    protected virtual void Start() //overide the livingEntity public voide start, this is needed otherwise it overide the other
    {
        health = startingHealth;
    }


    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        //do some stuf with the hit var
        TakeDamage(damage);
        if (healthBar != null)
        {
            healthBar.fillAmount = health / startingHealth;
        }


    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (healthBar != null)
        {
            healthBar.fillAmount = health / startingHealth;
        }
        if (health <= 0 && !dead)//if health is 0 player die
        {
            Die();
        }

    }

    public void Die()//if die thats is true
    {
        dead = true;
        if(OnDeath != null)// call it if it is not ondeath
        {
            OnDeath();
        }
        //GameObject.Destroy(gameObject);
        //gameObject.SetActive(false);
    }

}
