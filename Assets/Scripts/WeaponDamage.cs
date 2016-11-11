using UnityEngine;
using System.Collections;

public class WeaponDamage : MonoBehaviour {

    public int damage;

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }

        if(other.tag == "Ground" || other.tag == "Enemy")
            Destroy(gameObject);
    }
}
