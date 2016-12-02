using UnityEngine;
using System.Collections;

public class SpawnObject : MonoBehaviour {

    public GameObject obj;

    void Start()
    {
        OVRTouchpad.Create();
        OVRTouchpad.TouchHandler += HandleTouchHandler;
    }

    void HandleTouchHandler(object sender, System.EventArgs e)
    {
        OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

        if (touchArgs.TouchType == OVRTouchpad.TouchEvent.SingleTap)
        {
            Instantiate(obj, transform.position, Quaternion.identity);
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
                Instantiate(obj, transform.position, Quaternion.identity);
            }
        }
    }
}
