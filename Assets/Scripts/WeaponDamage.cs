using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDamage : MonoBehaviour {

    public int damage;
    List<Collider> hits;

	void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }        

        if((other.tag == "Ground" || other.tag == "Enemy") && gameObject.tag != "Explosion")
            Destroy(gameObject);
    }
}