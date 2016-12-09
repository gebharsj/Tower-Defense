using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public float maxHealth = 100f;
	public float curHealth = 0f;
    public Renderer rend;

	public bool alive = true;

    bool indicating;
    bool dying;

    public AudioSource damageSound;
    public AudioSource crumbleSound;

    // Use this for initialization
    void Start () 
	{
		alive = true;
		curHealth = maxHealth;
	}

	public void TakeDamage(int damage)
	{    
	    curHealth -= damage;
        damageSound.Play();
        StartCoroutine(DamageIndicator());

		if (curHealth <= 0) 
		{
			Die ();
		}
	}

	void Die()
	{
        if (gameObject.tag == "Enemy")
            StartCoroutine(Death());
        if (gameObject.tag == "Tower")
        {
            crumbleSound.Play();
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