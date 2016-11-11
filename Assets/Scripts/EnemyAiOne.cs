using UnityEngine;
using System.Collections;

public class EnemyAiOne : MonoBehaviour 
{	
	public int scoreToGive;			//The score that will be added once the enemy is killed
	public int damage;
		
	public float timer = 1.0f;

	public Transform goal;   //The GameObject of which the enemy will move towards and attack

	// Use this for initialization
	void Start () 
	{
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;
	}

	public void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Tower")
		{
			if (timer <= 0) 
			{
				other.gameObject.GetComponent<Health> ().TakeDamage (damage);
				timer = 0.5f;
			}
			timer -= Time.deltaTime;
		}
	}
}