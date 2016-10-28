using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject cameraAnchor;

	// Use this for initialization
	IEnumerator Start () {
        if (cameraAnchor != null)
        {            
            yield return new WaitUntil(LerpTo);
            StartCoroutine(Start());
        }
	}

    bool LerpTo()
    {
        transform.position = Vector3.Lerp(transform.position, cameraAnchor.transform.position + new Vector3(0, 2.85f, 0), Time.deltaTime * 3);

        if (Vector3.Distance(transform.position, cameraAnchor.transform.position + new Vector3(0, 2.85f, 0)) <= 0.5)
            return true;
        else
            return false;
    }
}