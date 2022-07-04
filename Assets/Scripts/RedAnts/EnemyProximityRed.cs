using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProximityRed : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        Ant a = col.GetComponent<Ant>();
        if (a != null)
        {

            if (this.gameObject.transform.parent.GetComponent<RedAnt>())
            {
                var parent = this.gameObject.transform.parent.GetComponent<RedAnt>();
                parent.goTo = 3;
                parent.enemyLocation = col.transform.position;
                parent.isFighting = true;
                parent.isItAlive = col.gameObject;
            }

            else if (this.gameObject.transform.parent.GetComponent<RedSoldier>())
            {
                var parent = this.gameObject.transform.parent.GetComponent<RedSoldier>();
                parent.goTo = 3;
                parent.enemyLocation = col.transform.position;
               
            }


        }
    }
}
