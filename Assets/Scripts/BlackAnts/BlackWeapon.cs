using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackWeapon : MonoBehaviour
{
    public int weaponStrength = 0;

    private void Start()
    {
        var theClass = this.gameObject.transform.parent.gameObject.name;
        switch (theClass)
        {
            case "ant(Clone)":
                weaponStrength = 1;
                break;
                
        }
            
    }
}
