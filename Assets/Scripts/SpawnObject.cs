using UnityEngine;
using System.Collections;

public class SpawnObject : MonoBehaviour {

    public GameObject obj;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground" || other.tag == "Enemy")
        {
            Instantiate(obj, transform.position, Quaternion.identity);
        }
    }
}
