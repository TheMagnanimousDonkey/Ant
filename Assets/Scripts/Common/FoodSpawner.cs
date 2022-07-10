using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject Food;
    void Start()
    {
        StartCoroutine(SpawnFood());
    }

    IEnumerator SpawnFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            var x = Random.Range(-2.5f, 3.5f);
            var y = Random.Range(1f, 3.5f);
            Instantiate(Food, new Vector3(x, y, 0), Quaternion.identity);
        }
        
    }
}
