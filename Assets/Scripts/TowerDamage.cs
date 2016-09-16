using UnityEngine;
using System.Collections;

public class TowerDamage : MonoBehaviour 
{
	float damage = 10f;

	void Start () 
	{

	}

	void Update () 
	{
		
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Tower") 
		{			
			other.gameObject.GetComponent<TowerHealth> ().EnemyHit ();
		}
	}
}