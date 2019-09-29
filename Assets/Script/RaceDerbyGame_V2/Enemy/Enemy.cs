using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;//In the newer unity version you need to add this line in order to work
using UnityEngine;


[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : LivingEntity{ //this class is set in code livingEntity 

    public enum State { Idle, Chasing, Attacking};
    State currentState;
    public GameObject deathEffect;

    NavMeshAgent pathfinder;//variable for the navmesh pathfinder
    Transform target;//player pos location so the enemy know where to go
    LivingEntity targetEntity;

    float attackDistanceThreshold = 1.5f;//set range of the nemy attack
    float timeBetweenAttacks = 5f;
    float nextAttackTime;
    float damage = 1f;

    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;

    protected override void Start () {//overide the livingEntity public voide start, this is needed otherwise it overide the other
        base.Start();//wont get called the livingEntity public voide start
        pathfinder = GetComponent<NavMeshAgent>();

        if (GameObject.FindGameObjectWithTag ("Player") != null)
        {
            currentState = State.Chasing;
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;//target is de player
            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            StartCoroutine(UpdatePath());
        }
       
    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if(damage <= health)
        {
            Destroy(Instantiate(deathEffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject,2);//code is about before somethin dies, this will hapen
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }

    void OnTargetDeath()//once the target dies we no longer has a target
    {
        hasTarget = false;
        currentState = State.Idle;
    }

	void Update () {

        if (hasTarget)
        {
            if (Time.time > nextAttackTime)//set the timer and distance for the attak by making a calculated square between enemy and player
            {
                float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
                if (sqrDstToTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
	}

    IEnumerator Attack()
    {
        currentState = State.Attacking;

        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius);

        float percent = 0;
        float attackSpeed = 3;

        bool hasAppliedDamage = false;

        while (percent <= 1)
        {
            if(percent >= .5f && !hasAppliedDamage && targetEntity != null)
            {
                hasAppliedDamage = true;
                targetEntity.TakeHit(damage, targetEntity.transform.position, targetEntity.transform.forward);//if player die do a function in the player script
            }

            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent,2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);
            Score.scoreValue -= 6;
            yield return null;
        }

        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()//instead of updating evry frame. update it in fixed time, this will spare compute power
    {
        float refreshRate = 0.25f;//timer is a quarter a 1 second of update

        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 originalPosition = transform.position;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget *(myCollisionRadius + targetCollisionRadius + attackDistanceThreshold/2);
                if (!dead)
                {
                    pathfinder.SetDestination(targetPosition);//updating the player pos
                    Score.scoreValue += 1;
                }
            }
            yield return new WaitForSeconds(refreshRate);//repeat on every refreshrate wich is 0.25f second
        }
    }
}
