using UnityEngine;
using System.Collections;

public class EnemyAiOne : MonoBehaviour 
{	
	public int scoreToGive;			//The score that will be added once the enemy is killed
	public int damage;
		
	public float timer = 3.0f;
    public Animator aiAnimator;
    NavMeshAgent agent;
    bool isTrolling;
    GameObject tower;
    GameObject hand;

    // Use this for initialization
    void Start () 
	{
        agent = GetComponent<NavMeshAgent>();
        tower = GameObject.Find("Tower");
        if (gameObject.tag == "Troll")
        {
            hand = transform.FindChild("Hand").gameObject;
            if (hand == null)
                Debug.LogError("No Hand");
        }
        agent.SetDestination(tower.transform.position);
        if (aiAnimator != null)
            aiAnimator.SetBool("isRunning", true);
    }

    //IEnumerator TrollThings()
    //{
    //    if(!isTrolling)
    //    {
    //        isTrolling = true;
    //        if()
    //    }
    //}

	public void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Tower")
		{
            if (Vector3.Distance(transform.position, other.transform.position) <= 2)
            {
                if (timer <= 0)
                {
                    if (aiAnimator != null)
                    {
                        aiAnimator.SetBool("isRunning", false);
                        aiAnimator.SetBool("isAttacking", true);
                    }
                    other.gameObject.GetComponent<Health>().TakeDamage(damage);
                    timer = 0.5f;
                }
                timer -= Time.deltaTime;
            }
		}

        if (gameObject.tag == "Troll")
        {
            if (other.gameObject.tag == "Boulder")
            {
                if (Vector3.Distance(transform.position, other.transform.position) <= 4f)
                {
                    agent.Stop();
                    aiAnimator.SetBool("isRunning", false);
                    aiAnimator.SetBool("isLifting", true);
                    other.transform.position = hand.transform.position;
                    transform.LookAt(tower.transform);
                    hand.GetComponent<Launcher>().TrollLaunch(other.gameObject);
                    StartCoroutine(WaitToMove());
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Troll")
        {
            if (other.gameObject.tag == "Boulder")
            {
                //moving to boulder
                if (agent.destination != other.transform.position)
                    agent.SetDestination(other.transform.position);
            }
        }
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(1f);
        agent.SetDestination(tower.transform.position);
        agent.Resume();
    }
}