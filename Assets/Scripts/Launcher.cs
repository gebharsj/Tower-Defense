using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class Launcher : MonoBehaviour 
{
    public LayerMask _layerMask;
    // handles
    [SerializeField] private Transform _bullseye;    // target transform
    public GameObject projectile;

    // Editor variables
    [SerializeField]
    public float _targetRange;
    [SerializeField]
    public float _angle;      // shooting angle
    [SerializeField]
    public float maxHeight;
    public float fireDelay = 1.5f;
    public bool isTroll;
    public AudioSource loadSound;
    public AudioSource fireSound;

    public bool _targetReady;
    bool firing;

    RaycastHit hit;

    void Start()
    {
        OVRTouchpad.Create();
        OVRTouchpad.TouchHandler += HandleTouchHandler;
    }

    void HandleTouchHandler(object sender, System.EventArgs e)
    {
        if (!isTroll)
        {
            OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

            if (touchArgs.TouchType == OVRTouchpad.TouchEvent.SingleTap)
            {
                _targetReady = true;
                //Launch();
                StartCoroutine(FireCoroutine());
            }
        }
    }

    void Update()
    {
        
    }    

	void FixedUpdate () 
    {
        if (!isTroll)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if (Physics.Raycast(ray, out hit, _layerMask))
            {
                if (hit.transform.tag == "Ground")
                {
                    _targetReady = true;
                    AcquireTarget(hit.point);
                }
            }
        }
    }

    void CallCoroutine(string coroutine)
    {
        StartCoroutine(coroutine);
    }

    IEnumerator FireCoroutine()
    {
        loadSound.Play();
        if (!firing)
        {
            firing = true;
            Launch();
            yield return new WaitForSeconds(fireDelay);
            firing = false;
        }
    }

    private void Launch()
    {
        fireSound.Play();
        GameObject clone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;       // + new Vector3(0, -1.5f, 0) if launching from camera

        // source and target positions
        Vector3 pos = transform.position;
        Vector3 target = _bullseye.position;

        // distance between target and source
        float dist = Vector3.Distance(pos, target);

        // rotate the object to face the target
        clone.transform.LookAt(target);

        // calculate initival velocity required to land the cube on target using the formula (9)
        float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle)));
        float Vy, Vz;   // y,z components of the initial velocity

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

        //create the velocity vector in local space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        // transform it to global vector
        Vector3 globalVelocity = clone.transform.TransformVector(localVelocity);

        // launch the cube by setting its initial velocity
        clone.GetComponent<Rigidbody>().velocity = globalVelocity * .75f;

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

    public void TrollLaunch(GameObject projectile)
    {
        // source and target positions
        Vector3 pos = transform.position;
        Vector3 target = Vector3.zero;

        // distance between target and source
        float dist = Vector3.Distance(pos, target);

        // rotate the object to face the target
        projectile.transform.LookAt(target);

        // calculate initival velocity required to land the cube on target using the formula (9)
        float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle)));
        float Vy, Vz;   // y,z components of the initial velocity

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

        //create the velocity vector in local space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        // transform it to global vector
        Vector3 globalVelocity = projectile.transform.TransformVector(localVelocity);
        //print(projectile.GetComponent<Rigidbody>().velocity);

        // launch the cube by setting its initial velocity
        projectile.GetComponent<Rigidbody>().velocity = globalVelocity;

        // after launch revert the switch
        _targetReady = false;
    }
}