using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour {

    [SerializeField] Transform observeable;
    [SerializeField] float aheadSpeed;
    [SerializeField] float followDamping;
    [SerializeField] float cameraHeight;

    Rigidbody _observeableRigidBody;

    void Start () {
        _observeableRigidBody = observeable.GetComponent<Rigidbody>();
    }
	
	void Update () {
        if (observeable == null)//dont update if there is no observeable incase the camera is faster then the playerspeed
            return;

        Vector3 targetPosition = observeable.position + Vector3.up * cameraHeight + _observeableRigidBody.velocity * aheadSpeed;//camera follow the car plus go ahead
        transform.position = Vector3.Lerp(transform.position, targetPosition, followDamping * Time.deltaTime);//lerp effect camera

    }
}
