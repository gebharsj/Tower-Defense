using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class WeaponsWheel : MonoBehaviour
{
    public GameObject _camera;
    public GameObject disc;

    public GameObject[] weapons;
    
    public GameObject currentWeapon;

    private int weaponNumber = 0;
    float rotationAmount;
    float newY;
    float timer;
    bool rotating = false;

    enum Direction
    {
        Right,
        Left,
    }

    Direction dir = Direction.Right;

    void Start()
    {
        OVRTouchpad.Create();
        OVRTouchpad.TouchHandler += HandleTouchHandler;
        //Set Current weapon to 0
        currentWeapon = weapons[0];
        rotationAmount = 360.0f / weapons.Length;
        SetWeapon();
        //foreach (GameObject weapon in weapons)
        //{
        //    if (weapon == currentWeapon)
        //        weapon.transform.FindChild("Launcher").gameObject.SetActive(true);
        //    else if (weapon != currentWeapon)
        //        weapon.transform.FindChild("Launcher").gameObject.SetActive(false);
        //}
    }

    void HandleTouchHandler(object sender, System.EventArgs e)
    {
        if (!rotating)
        {
            OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

            if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Left)
            {
                if (weaponNumber == weapons.Length - 1)
                    weaponNumber = 0;
                else
                    weaponNumber++;

                if (!rotating)
                    dir = Direction.Left;

                StartCoroutine(RotateCoroutine());
            }
            else if (touchArgs.TouchType == OVRTouchpad.TouchEvent.Right)
            {
                if (weaponNumber == 0)
                    weaponNumber = weapons.Length - 1;
                else
                    weaponNumber--;

                if (!rotating)
                    dir = Direction.Right;

                StartCoroutine(RotateCoroutine());
            }
        }      
    }

    IEnumerator RotateCoroutine()
    {
        if(!rotating)
        {
            rotating = true;
            if (dir == Direction.Left)
                newY = Camera.main.transform.eulerAngles.y - (rotationAmount * weaponNumber);
            else if (dir == Direction.Right)
                newY = Camera.main.transform.eulerAngles.y - (rotationAmount * weaponNumber);

            yield return new WaitUntil(RotateWeapons);
            timer = 0;
            SetWeapon();
            rotating = false;
        }
    }

    bool RotateWeapons()
    {
        timer += 1 / 60.0f;
        disc.transform.localRotation = Quaternion.Lerp(disc.transform.localRotation, Quaternion.Euler(-90, 0, newY), Time.deltaTime * 8f);
        if (dir == Direction.Left)
        { 
            if (disc.transform.eulerAngles.y <= newY + .05f)
            {
                disc.transform.eulerAngles = new Vector3(-90, 0, newY);
                return true;
            }
            else
                return false;
        }
        else if (dir == Direction.Right)
        {
            if (disc.transform.eulerAngles.z >= newY - .05f)
            {
                disc.transform.eulerAngles = new Vector3(-90, 0, newY);
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    void Update()
    {
        if (timer >= .6f)
        {
            StopAllCoroutines();
            timer = 0;
            SetWeapon();
            rotating = false;
        }
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);
    }

    void SetWeapon()
    {
        disc.transform.eulerAngles = new Vector3(-90, 0, newY);        
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);    //.transform.FindChild("Launcher").gameObject
        }
        currentWeapon = weapons[weaponNumber];
        currentWeapon.SetActive(true);  //transform.FindChild("Launcher").gameObject.
    }
}