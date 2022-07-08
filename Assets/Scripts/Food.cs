using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Food : MonoBehaviour
{
    public GameObject FoodGonePrefab;
    public void destroyFood()
    {
        Instantiate(FoodGonePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
