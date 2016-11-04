using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class Launcher : MonoBehaviour 
{
    // handles
    [SerializeField] private Transform _bullseye;    // target transform
    public GameObject projectile;

    // Editor variables
    public float _targetRange;  
    public float _angle;      // shooting angle
    public float maxHeight;
    float time = 2;

    private bool _targetReady;

    RaycastHit hit;

    float g;

    void Start()
    {
        g = Physics.gravity.y;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _targetReady)
        {
            Launch();
        }
    }    

	void FixedUpdate () 
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.tag == "Ground")
            {
                AcquireTarget(hit.point);
            }
        }
    }

    private void Launch()
    {
        GameObject clone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;

        // source and target positions
        Vector3 pos = Vector3.zero;
        Vector3 target = _bullseye.position;

        // distance between target and source
        float dist = Vector3.Distance(pos, target);

        // rotate the object to face the target
        clone.transform.LookAt(target);

        // calculate initival velocity required to land the cube on target using the formula (9)
        float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle) * 2));
        float Vy, Vz;   // y,z components of the initial velocity

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

        //create the velocity vector in local space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        // transform it to global vector
        Vector3 globalVelocity = clone.transform.TransformVector(localVelocity);

        // launch the cube by setting its initial velocity
        clone.GetComponent<Rigidbody>().velocity = globalVelocity;

        // after launch revert the switch
        _targetReady = false;
    }

    // Returns a random target from the target pool
    private void AcquireTarget()
    {   
        //  Target Pool (no scale)
        //
        //   -1     0     1
        //  1 X-----X-----X 1
        //    |           |
        //    |     0     |
        //  0 X     X     X 0
        //    |           |
        //    |           |
        // -1 X-----X-----X -1
        //   -1     0     1

        Vector3[] targetPool =
        {
            new Vector3(0,0,0),
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
            new Vector3(1, 0, 1),
            new Vector3(1, 0, -1),
            new Vector3(-1, 0, 1),
            new Vector3(-1, 0, -1),
        };

        // scale target positions according to the range
        for (int i = 0; i < targetPool.Length; i++)
        {
            targetPool[i] *= _targetRange;
        }

        // get a random vector from the target pool and set
        // the Bullseye's position to the newly acquired target
        int index = Random.Range(0, targetPool.Length);
        _bullseye.position = targetPool[index];

        // get ready to launch
        _targetReady = true;
    }

    public void AcquireTarget(Vector3 target)
    {
        _bullseye.position = target;
        // get ready to launch
        _targetReady = true;
    }
}