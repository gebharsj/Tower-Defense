using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDamage : MonoBehaviour {

    public int damage;
    List<Collider> hits;

	void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "Grenade")
        {
            if (!hits.Contains(other))
            {
                hits.Add(other);
            }

            if (hits.Count > 0)
            {
                foreach (Collider col in hits)
                {
                    if (col.tag == "Enemy")
                    {
                        col.GetComponent<Health>().TakeDamage(damage);
                    }
                }
            }
        }
        else
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<Health>().TakeDamage(damage);
            }
        }

        if(other.tag == "Ground" || other.tag == "Enemy")
            Destroy(gameObject);
    }
}
