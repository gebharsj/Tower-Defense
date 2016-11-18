using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireWrangler : MonoBehaviour {

    public List<GameObject> fires;
    public List<GameObject> towerParts;

    public void RemovePart(GameObject part)
    {
        if(towerParts.Contains(part))
            towerParts.Remove(part);
        ActivateFire();
    }

    void ActivateFire()
    {
        switch (towerParts.Count)
        {
            case 3:
                if (!fires[0].activeInHierarchy)
                    fires[0].SetActive(true);
                break;
            case 2:
                if (!fires[1].activeInHierarchy)
                    fires[1].SetActive(true);
                break;
            case 1:
                if (!fires[2].activeInHierarchy)
                    fires[2].SetActive(true);
                break;
            default:
                break;
        }
    }
}