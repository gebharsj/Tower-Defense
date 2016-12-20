using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public float maxHealth = 100f;
	public float curHealth = 0f;
    public Renderer rend;
    [SerializeField]
    AudioSource towerClash;
    [SerializeField]
    AudioSource towerCrumble;
   
  

	public bool alive = true;

    bool indicating;
    bool dying;

	// Use this for initialization
	void Start () 
	{
		alive = true;
		curHealth = maxHealth;
	}

	public void TakeDamage(int damage)
	{    
	    curHealth -= damage;
        towerClash.Play();
        StartCoroutine(DamageIndicator());

		if (curHealth <= 0) 
		{
			Die ();
		}
	}

	void Die()
	{
        if (gameObject.tag == "Enemy" || gameObject.tag == "Troll")
            StartCoroutine(Death());
        if (gameObject.tag == "Tower")
        {
            towerCrumble.Play();
            transform.parent.GetComponent<FireWrangler>().RemovePart(gameObject);
            Destroy(this.gameObject);
        }
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

    IEnumerator Death()
    {
        if(!dying)
        {
            dying = true;
            GetComponent<NavMeshAgent>().Stop();
            GetComponentInChildren<Animator>().SetBool("isDead", true);
            yield return new WaitForSeconds(4.6f);
            Destroy(this.gameObject);
            dying = false;
        }
    }
}