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

        foreach (GameObject weapon in weapons)
        {
            if (weapon == currentWeapon)
                weapon.transform.FindChild("Launcher").gameObject.SetActive(true);
            else if (weapon != currentWeapon)
                weapon.transform.FindChild("Launcher").gameObject.SetActive(false);
        }
    }

    void HandleTouchHandler(object sender, System.EventArgs e)
    {
        OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;

        if(touchArgs.TouchType == OVRTouchpad.TouchEvent.Left && !rotating)
        {
            if (weaponNumber == weapons.Length - 1)
            {
                weaponNumber = 0;
            }
            else
            {
                weaponNumber = (weaponNumber + 1);
            }

            if(!rotating)
                dir = Direction.Left;

            StartCoroutine(RotateCoroutine());
        }
        else if(touchArgs.TouchType == OVRTouchpad.TouchEvent.Right && !rotating)
        {
            if (weaponNumber == 0)
            {
                weaponNumber = weapons.Length - 1;
            }
            else
            {
                weaponNumber = (weaponNumber - 1);
            }

            if (!rotating)
                dir = Direction.Right;

            StartCoroutine(RotateCoroutine());
        }

        currentWeapon = weapons[weaponNumber];
        foreach(GameObject weapon in weapons)
        {
            if (weapon == currentWeapon)
                weapon.transform.FindChild("Launcher").gameObject.SetActive(true);
            else if (weapon != currentWeapon)
                weapon.transform.FindChild("Launcher").gameObject.SetActive(false);
        }
    }

    IEnumerator RotateCoroutine()
    {
        if(!rotating)
        {
            rotating = true;
            if (dir == Direction.Left)
            {
                newY = disc.transform.localRotation.eulerAngles.y - rotationAmount;
            }
            else if (dir == Direction.Right)
            {
                newY = disc.transform.localRotation.eulerAngles.y + rotationAmount;
            }

            yield return new WaitUntil(RotateWeapons);
            timer = 0;
            rotating = false;
        }
    }

    bool RotateWeapons()
    {
        timer += 1 / 60.0f;
        if (dir == Direction.Left)
        {
            disc.transform.localRotation = Quaternion.Lerp(disc.transform.localRotation, Quaternion.Euler(0, newY, 0), Time.deltaTime * 8f);
            if (disc.transform.localRotation.eulerAngles.y <= newY + .05f)
                return true;
            else
                return false;
        }
        else if (dir == Direction.Right)
        {
            disc.transform.localRotation = Quaternion.Lerp(disc.transform.localRotation, Quaternion.Euler(0, newY, 0), Time.deltaTime * 8f);
            if (disc.transform.localRotation.eulerAngles.y >= newY - .05f)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    void Update()
    {
        if (timer >= .75f)
        {
            StopAllCoroutines();
            timer = 0;
            rotating = false;
        }
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
    }
}