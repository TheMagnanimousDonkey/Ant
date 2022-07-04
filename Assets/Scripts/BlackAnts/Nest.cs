using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Nest : MonoBehaviour
{
    int totalFood = 20;
    public Text foodText;
    public GameObject Ant;
    //public GameObject RedAnt;
    private List<Vector2> foodLocations;
    void Start()
    {
        StartCoroutine(SpawnAnt());
       // StartCoroutine(SpawnRed());
        foodLocations = new List<Vector2>();
    }

    public void SetFoodLocation(Vector2 nest)
    {
        if (!foodLocations.Contains(nest))
        {
            foodLocations.Add(nest);
        }
    }

    public Vector2 GetFoodLocation()
    {
        
        if (foodLocations.Count > 0)
        {
            var rnd = Random.Range(0, foodLocations.Count);
            return foodLocations[rnd];
        }
        return new Vector2(0,0);
    }
    IEnumerator SpawnAnt()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (totalFood > 0)
            {
                Instantiate(Ant, transform.position, transform.rotation);
                totalFood--;
                foodText.text = totalFood.ToString();

            }
               

        }

    }
    IEnumerator SpawnSoilder()
    {
       
            yield return new WaitForSeconds(100f);

           // Instantiate(RedAnt, new Vector3(1.67f, -1.64f, 0), transform.rotation);
            

        

    }

    public void handleFood(bool food)
    {
        if (food)
        {
            totalFood++;
        }
        else
        {
            totalFood--;
        }
    }
}
