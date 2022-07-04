using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedNest : MonoBehaviour
{
    int totalFood = 20;
    public GameObject foodText;
    public GameObject RedAnt;
    public GameObject RedSoldier;
    private List<Vector2> foodLocations;
    void Start()
    {
        StartCoroutine(SpawnAnt());
      //  StartCoroutine(SpawnRed());
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
        return new Vector2(0, 0);
    }
    IEnumerator SpawnAnt()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (totalFood > 30)
            {
                Instantiate(RedSoldier, transform.position, transform.rotation);
                totalFood = totalFood - 30;
                foodText.gameObject.transform.GetChild(1).GetComponent<Text>().text = totalFood.ToString();

            }
            else if (totalFood > 0)
            {
               // var wanderer = RedAnt.gameObject.transform.GetComponent<RedAnt>();
               // wanderer.wanderOnly = false;
                Instantiate(RedAnt, transform.position, transform.rotation);
                totalFood--;
                foodText.gameObject.transform.GetChild(1).GetComponent<Text>().text = totalFood.ToString();

            }


        }

    }
    IEnumerator SpawnRed()
    {

        yield return new WaitForSeconds(100f);

        Instantiate(RedAnt, new Vector3(1.67f, -1.64f, 0), transform.rotation);

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
