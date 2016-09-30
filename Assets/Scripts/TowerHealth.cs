using UnityEngine;
using System.Collections;

public class TowerHealth : MonoBehaviour 
{


	public float maxHealth = 100f;
	public float curHealth = 0f;

	public bool alive = true;
	// Use this for initialization
	void Start () 
	{
		alive = true;
		curHealth = maxHealth;
	}
	public void EnemyHit(int amount)
	{    
	curHealth -=  amount;
	float myHealth = curHealth / maxHealth;
		if (curHealth <= 0) 
		{

			Die ();
		}
	}
	void Die()
	{   
	Destroy (this.gameObject);
	}
	void Destroy()
	{   
	Destroy (this.gameObject);
	}
}


