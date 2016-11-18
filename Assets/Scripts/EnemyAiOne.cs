using UnityEngine;
using System.Collections;

public class EnemyAiOne : MonoBehaviour 
{	
	public int scoreToGive;			//The score that will be added once the enemy is killed
	public int damage;
		
	public float timer = 3.0f;
    NavMeshAgent agent;

    // Use this for initialization
    void Start () 
	{
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GameObject.Find("Tower").transform.position);
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