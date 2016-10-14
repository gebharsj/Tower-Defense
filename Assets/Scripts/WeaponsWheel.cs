using UnityEngine;
using System.Collections;

public class WeaponsWheel : MonoBehaviour
{
    public GameObject[] weapons;

    public GameObject currentWeapon;

    private int weaponNumber = 0;

    void Start()
    {
        //Set Current weapon to 0
        currentWeapon = weapons[0];
    }

    void Update()
    {
        // Get Input From The Mouse Wheel
        // if mouse wheel gives a positive value add 1 to weaponNumber
        // if it gives a negative value decrease weaponNumber with 1
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (weaponNumber == weapons.Length-1)
            {
                weaponNumber = 0;
            }
            else
            {
                weaponNumber = (weaponNumber + 1);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 )
        {
            if (weaponNumber ==0)
            {
                weaponNumber = weapons.Length - 1;
            }
            else
            {
                weaponNumber = (weaponNumber - 1);
            }
        }
        currentWeapon = weapons[weaponNumber];
    }
}