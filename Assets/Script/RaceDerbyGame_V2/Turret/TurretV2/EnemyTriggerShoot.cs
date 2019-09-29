using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerShoot : MonoBehaviour
{

    public Transform gunEnd;
    public GameObject bullet;
    public float fireRate = 2f;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("shooting");

        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("Shooting");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopCoroutine("Shooting");
        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            Debug.Log("shooting");
            Instantiate(bullet, gunEnd.position, gunEnd.rotation);
            yield return new WaitForSeconds(fireRate);
        }
    }
}