using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProximityBlack : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        RedAnt ra = col.GetComponent<RedAnt>();
        RedSoldier rs = col.GetComponent<RedSoldier>(); 
        if (ra != null || rs != null)
        {
            var parent = this.gameObject.transform.parent.GetComponent<Ant>();
            parent.goTo = 3;
            parent.enemyLocation = col.transform.position;
            parent.isFighting = true;
            parent.isItAlive = col.gameObject;

        }

    }
}