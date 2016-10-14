using UnityEngine;
using System.Collections;

public class WeaponsWheel : MonoBehaviour
{
    
    public GameObject[] theWeapons;

    public GameObject CurrentWeapon;

    private int WeaponNumber = 0;

    void Start()
    {

        //Set Current weapon to 0
        CurrentWeapon = theWeapons[0];

    }

    void Update()
    {

        //Get Input From The Mouse Wheel
        // if mouse wheel gives a positive value add 1 to WeaponNumber
        // if it gives a negative value decrease WeaponNumber with 1

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (WeaponNumber == theWeapons.Length-1)
            {
                WeaponNumber = 0;

            }
            else 
            {
                WeaponNumber = (WeaponNumber + 1);
            }
        }
      
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 )
        {
            if (WeaponNumber ==0)
            {
                WeaponNumber = theWeapons.Length - 1;
            }
            else
            {
                WeaponNumber = (WeaponNumber - 1);
            }
         }
        
        CurrentWeapon = theWeapons[WeaponNumber];

    }

}