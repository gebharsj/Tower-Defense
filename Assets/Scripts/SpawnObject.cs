using UnityEngine;
using System.Collections;

public class SpawnObject : MonoBehaviour {

    public GameObject obj;
    [SerializeField]
    AudioSource cauldronPour;

    void Start()
    {
        OVRTouchpad.Create();
        OVRTouchpad.TouchHandler += HandleTouchHandler;
    }

    void HandleTouchHandler(object sender, System.EventArgs e)
    {
        if (gameObject.name == "Launcher" && gameObject.activeInHierarchy)
        {
            OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

            if (touchArgs.TouchType == OVRTouchpad.TouchEvent.SingleTap)
            {
                Instantiate(obj, transform.position, Quaternion.identity);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Boulder")
        {
            if(other.tag == "Tower")
            {
                Instantiate(obj, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (other.tag == "Ground" || other.tag == "Enemy")
            {
                cauldronPour.Play();
                Instantiate(obj, transform.position, Quaternion.identity);
            }
        }
    }
}
