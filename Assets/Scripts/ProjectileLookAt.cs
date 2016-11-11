using UnityEngine;
using System.Collections;

public class ProjectileLookAt : MonoBehaviour {

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vel = GetComponent<Rigidbody>().velocity;
        transform.rotation = Quaternion.LookRotation(vel);
    }
}
