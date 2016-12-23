using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    public GameObject _camera;

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(-90, 0, _camera.transform.eulerAngles.y);
    }
}
