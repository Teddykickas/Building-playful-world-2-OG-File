using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTurret : LivingEntity
{

    public GameObject currenTarget;
    public Transform turretHead;

    public float attackDist;
    public float attackDamage;
    public float attackCoolDown;
    public float attackTimer;
    public float attackLookSpeed = 2f;

    public bool showRange = false;

    public TurretShootBase turretShotScript;

    void Start ()
    {
        InvokeRepeating("checkForTarget", 0, 0.5f);
        turretShotScript = GetComponent < TurretShootBase> ();

    }
	
	void Update ()
    {
		if(currenTarget !=null)
        {
            followTarget();
        }

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            if(currenTarget != null)
            {
                attackTimer = 0;
                shoot();
            }
        }
	}


    private void checkForTarget()// this can also be don in update, but this is way more efficent as it only called when needed
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);//check for target coliders within the sphere
        float distAway = Mathf.Infinity;//do this infinit times

        for (int i = 0; i < colls.Length; i++)//if colider is in range
        {
            if(colls[i].tag == "Player")//if colider is enemy
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);//devine dist
                if(dist < distAway)//if target is closer then other target
                {
                    currenTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
            
        }
    }

    private void followTarget()
    {
        Vector3 targetDir = currenTarget.transform.position - transform.position;//Direction of turret os current transformPos - transformPos
        targetDir.y = 0;//Devine the transform y to 0
        turretHead.forward = targetDir;

    }

    private void shoot()
    {
        //turretShotScript.Shoot(currenTarget);//Thise code is bugged due to lag of var?
    }


    private void OnDrawGizmos()//see able variables in unity scene
    {
        if (showRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackDist);
        }
    }
}
