using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public float maxHealth = 100f;
	public float curHealth = 0f;
    public MeshRenderer rend;

	public bool alive = true;

    bool indicating;

	// Use this for initialization
	void Start () 
	{
		alive = true;
		curHealth = maxHealth;
	}

	public void TakeDamage(int damage)
	{    
	    curHealth -= damage;
        StartCoroutine(DamageIndicator());

		if (curHealth <= 0) 
		{
			Die ();
		}
	}

	void Die()
	{
        if(gameObject.tag == "Tower")
        {
            transform.parent.GetComponent<FireWrangler>().RemovePart(gameObject);
        }
	    Destroy (this.gameObject);
	}

    IEnumerator DamageIndicator()
    {
        if(!indicating)
        {
            indicating = true;
            rend.material.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            rend.material.color = Color.white;
            indicating = false;
        }
    }
}