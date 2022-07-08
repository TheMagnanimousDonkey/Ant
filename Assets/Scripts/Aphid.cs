using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aphid : MonoBehaviour
{
    public int totalFood = 10;

    public void Update()
    {
        if (totalFood <= 0)
        {
           var getParent = gameObject.GetComponentInParent<Food>();
           getParent.destroyFood();
        }
    }
}
