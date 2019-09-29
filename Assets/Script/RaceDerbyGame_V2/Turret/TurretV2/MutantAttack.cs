using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MutantAttack : MonoBehaviour
{

    public Transform player;
    Rigidbody rigidbody;
    public CapsuleCollider capCol;
    Vector3 spawnPoint;
    //public GameObject healthBar;
    public int currentHealth;
    public int maxHealth = 3;
    public int damageTaken = 1;
    private bool inDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
    }


    public void Damage(int damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();


        PlayerCar p = FindObjectOfType<PlayerCar>();//if there are any playercar with this script. follow the object with the player script as transform.pos
        if (p)
        {
            player = p.transform;
        }
        //navComponent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    void Update()
    {
        //rotation turret to player
        /*//this code doesent work with the lerp etc
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        if (Vector3.Distance(player.position, this.transform.position) < 80 && angle < 140)
        {

            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.2f);


        }
        */
        if (player == null)//keep searching bitch
        {
            PlayerCar p = FindObjectOfType<PlayerCar>();//if there are any playercar with this script. follow the object with the player script as transform.pos
            if (p)
            {
                player = p.transform;
            }
        }
        else
        {
            transform.LookAt(player.position);
        }

        

    }


}