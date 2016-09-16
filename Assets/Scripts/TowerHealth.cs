using UnityEngine;
using System.Collections;

public class TowerHealth : MonoBehaviour {

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

	public void EnemyHit()
	{
		curHealth -= 5;
		float myHealth = curHealth / maxHealth;

		if (curHealth <= 0) {
			Die ();
		}
	}

	void Die()
	{
		Destroy (this.gameObject);

	}

	public void PlayerHit()
	{
		curHealth -= 5;
		float myHealth = curHealth / maxHealth;

		if (curHealth <= 0) {
			Destroy ();
		}
	}

	void Destroy()
	{  
		Destroy (this.gameObject);
	}
}

