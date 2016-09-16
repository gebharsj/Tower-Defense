using UnityEngine;
using System.Collections;

public class EnemyAiOne : MonoBehaviour 
{

	//public float moveSpeed;			//The enemy's speed at which it can move at
	float damage = 5f;				//The damage that the enemy will deal when attacking the target
	//public float chaseDist;
	//public float attackDist;		//The distance at which the enemy will stop moving towards the target to attack

	//public float dist;				//The distance at which the enemy is from the target
	//public float distance;
	public int scoreToGive;			//The score that will be added once the enemy is killed

	//public GameObject target;		//The GameObject of which the enemy will move towards and attack

	//public bool walking = true;
	//public bool punch = true;
	//public float timer = 1.0f;
	public Transform goal;
	//public float stoppingDistance;

	void Awake ()
	{
		
	}

	// Use this for initialization
	void Start () 
	{
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;
	}

	// Update is called once per frame
	//void Update () 
	//{
		//if (goal.position < 6) {
			
		//}
	//}

	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Tower") 
		{
			other.gameObject.GetComponent<TowerHealth> ().EnemyHit ();
		}
	}
}