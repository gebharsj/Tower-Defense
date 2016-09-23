using UnityEngine;
using System.Collections;

public class TowerHealth : MonoBehaviour {

	//public int playerAmount;
	public float maxHealth = 100f;
	public float curHealth = 0f;
	//public GameObject healthBar;
	public bool alive = true;
	// Use this for initialization
	void Start () {
		alive = true;
		curHealth = maxHealth;
	}

	// Update is called once per frame
	void Update () {



	}
	/*public void TakeDamage (float amount) 	{ 		if (!alive)  		{ 			return;  		} 		if (curHealth <= 0)  		{ 			curHealth = 0; 			alive = false; 			//gameObject.SetActive (false); 		}  		curHealth -= amount; 		SetHealthBar (); 	}  */


	public void EnemyHit(int amount)
	{    

		curHealth -=  amount;
		float myHealth = curHealth / maxHealth;


		if (curHealth <= 0) {

			Die ();
		}
	}
	void Die()
	{   


		Destroy (this.gameObject);

	}
	/*public void PlayerHit(int playerAmount) 	{ 		curHealth -= playerAmount; 		float myHealth = curHealth / maxHealth;   		if (curHealth <= 0) {  			Destroy (); 		} 	} 	*/void Destroy()
	{   


		Destroy (this.gameObject);
	}
}


