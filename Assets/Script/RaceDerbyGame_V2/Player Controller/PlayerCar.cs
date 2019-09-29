using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (GunController))]

public class PlayerCar : LivingEntity{//this class is set in code livingEntity 

    [SerializeField] float accelaration = 10;
    [SerializeField] float turnSpeed = 5;//SerializeFiel betekend een voor ingevulde iets, speed rotation = 5

    public GameObject deathEffect;

    //private Rigidbody playerRigidBody;//pos player
    public float dashSpeed;//speed of dash
    private float dashTime;//dash type how long it last
    public float startDashTime;//decrease the dash type in game and reset the value
    private float accelarationBase;

    Quaternion targetRotation;//var van Quaternion is gelinkt aan tergetRotation
    Rigidbody _rigidBody;

    //Vector3 originalPosition = transform.position;//ability jump forward

    GunController gunController;

    protected override void Start()
    {
        base.Start();
        _rigidBody = GetComponent<Rigidbody>();
        gunController = GetComponent<GunController>();

        accelarationBase = accelaration;
        //playerRigidBody = GetComponent<Rigidbody>();
        //dashTime = startDashTime;
    }



    void Update()//Update car pos,rotation,direction and speed rotation
    {
        SetRotationPoint();
       
        //Weapon input
        if (Input.GetMouseButton(1))
        {
            gunController.Shoot();
        }

        //dash ability
        if (dashTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dashTime = startDashTime;
            }
        }
        else //Je bent wel aan het dashen
        {
            accelaration = accelarationBase + dashSpeed;//Snelheid omhoog
            dashTime -= Time.deltaTime;//Timer af laten tellen.
            if (dashTime <= 0)
            {
                accelaration = accelarationBase;
            }
        }
    }

    private void SetRotationPoint()//Camera script top down view follow car
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Mouse position is the input
        Plane plane = new Plane(Vector3.up, Vector3.zero);//Plane to triangilate the mouse pos and camera ray
        float distance;
        if(plane.Raycast(ray, out distance))//Direction of the car towards the mouse position on the plane
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;//Pos of the car
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;//Direction of the car
            targetRotation = Quaternion.Euler(0, rotationAngle, 0);//Rotation of the car
        }
    }

    private void FixedUpdate()//Drive car script
    {
        float accelarationInput = accelaration * (Input.GetMouseButton(0) ? 1 : Input.GetMouseButton(1) ? -1 : 0) * Time.fixedDeltaTime;// de Input.GetMouseButton(0) ? 1 is hetzelfde als een if statement voor de muis maar verkort (alles wat voor de "?:" staat is true erachter is false)
        _rigidBody.AddRelativeForce(Vector3.forward * accelarationInput);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);//turnspeed

    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            //Debug.Log("deadsun");
            Instantiate(deathEffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection));//code is about before somethin dies, this will hapen trigger the effect and  destroy game object player
            Destroy(gameObject);
            GameUi.self.OnGameOver();
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }
}
