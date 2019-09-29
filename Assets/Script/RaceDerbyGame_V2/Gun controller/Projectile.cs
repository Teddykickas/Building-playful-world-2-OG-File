using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : LivingEntity
{
    public LayerMask collisionMask;//determens wich layer the playersbullet colide with
    float damage = 1;//used for the IDamageable
    public float speed = 2;//speed projectile
    float lifeTime = 1;
    float bulletLengtHit = 1;// make sure the bullet raycast hit the target
    LivingEntity targetEntity;
    Transform target;//player pos location so the enemy know where to go

    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;

    protected override void Start()
    {//overide the livingEntity public voide start, this is needed otherwise it overide the other
        base.Start();//wont get called the livingEntity public voide start

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;//target is de player
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;
        }
    }

    void OnTargetDeath()//once the target dies we no longer has a target
    {
        hasTarget = false;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;//check collision
        CheckCollisions(moveDistance);

        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)//enemy coll
    {
        Ray ray = new Ray(transform.position, transform.position);
        RaycastHit hit;//return information

        if (Physics.Raycast(ray, out hit, moveDistance + bulletLengtHit, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider, hit.point);//if true pass in the hit variable
        }
    }

    void OnHitObject(Collider c, Vector3 hitPoint)//enemy coll
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();//so if the bullet hit something it might get the IDamageable
        if (damageableObject != null)//but not all wil have the variable
        {
            damageableObject.TakeHit(damage, hitPoint, transform.forward);
            Debug.Log("player hit");
        }
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")//if the enemy bullet hit target tag "player", bullet is destroyed and the entity takes health damage
        {
            targetEntity.TakeHit(damage, targetEntity.transform.position, targetEntity.transform.forward);
            //Debug.Log("Hit");
            GameObject.Destroy(gameObject);
        }
    }
}

