using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWeapon : MonoBehaviour
{
    public int weaponStrength = 0;

    private void Start()
    {
        var theClass = this.gameObject.transform.parent.gameObject.name;
        switch (theClass)
        {
            case "RedAnt(Clone)":
                weaponStrength = 1;
                break;
            case "RedSoldier(Clone)":
                weaponStrength = 3;
                break;

        }
    }
}
