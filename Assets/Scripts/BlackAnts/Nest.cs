using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Nest : MonoBehaviour
{
    public GameObject BlackAnt;
    //public GameObject BlackSoldier;
    private List<Vector2> foodLocations;
    private List<Vector2> removedfoodLocations;
    public Text totalText;
    public Text foodText;
    public Text soldierText;

    void Start()
    {
        StartCoroutine(SpawnAnt());
        ManagerScript.Instance.BlackFoodCount = 20;
        foodLocations = new List<Vector2>();
        removedfoodLocations = new List<Vector2>();
    }

    public void AddFoodLocation(Vector2 nest)
    {
        if (!foodLocations.Contains(nest) && !removedfoodLocations.Contains(nest))
        {
            foodLocations.Add(nest);
        }
    }

    public void RemoveFoodLocation(Vector2 nest)
    {
        if (foodLocations.Contains(nest))
        {
            removedfoodLocations.Add(nest);
            foodLocations.Remove(nest);
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
            if (ManagerScript.Instance.BlackFoodCount > 0 && ManagerScript.Instance.BlackAntCount < 70)
            {
                Instantiate(BlackAnt, transform.position, transform.rotation);
                ManagerScript.Instance.BlackAntCount++;
                ManagerScript.Instance.BlackFoodCount--;
            }
            foodText.text = ManagerScript.Instance.BlackFoodCount.ToString();
            totalText.text = ManagerScript.Instance.BlackAntCount.ToString();
            soldierText.text = ManagerScript.Instance.BlackSoldierCount.ToString();

        }

    }

}
