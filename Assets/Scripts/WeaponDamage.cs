using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDamage : MonoBehaviour {

    public int damage;
    List<Collider> hits;

	void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Boulder")
        {
            if(other.tag == "Tower")
            {
                other.GetComponent<Health>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.tag == "Enemy" || other.tag == "Troll")
            {
                other.GetComponent<Health>().TakeDamage(damage);
            }

            if ((other.tag == "Ground" || other.tag == "Enemy" || other.tag == "Troll") && gameObject.tag != "Explosion")
                Destroy(gameObject);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy" || other.tag == "Troll")
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }

        if ((other.tag == "Ground" || other.tag == "Enemy" || other.tag == "Troll") && gameObject.tag != "Explosion")
            Destroy(gameObject, .25f);
    }
}