using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public GameObject parent;
    public float maxRadius;
    bool expanding;
    SphereCollider col;
    float timer;

    void Start()
    {
        col = GetComponent<SphereCollider>();
        if(parent == null)
            parent = transform.parent.gameObject;
        StartCoroutine(ExplosionCoroutine());
    }

    void Update()
    {
        timer += 1 / 60.0f;
        if (timer >= .9f)
        {
            StopAllCoroutines();
            Destroy(parent);
        }
    }

    IEnumerator ExplosionCoroutine()
    {
        if(!expanding)
        {
            expanding = true;
            yield return new WaitUntil(Expand);
            expanding = false;
        }
    }

    bool Expand()
    {
        
        col.radius = Mathf.Lerp(col.radius, maxRadius, .9f);

        if (maxRadius - col.radius <= .1f)
            return true;
        else
            return false;
    }
}
