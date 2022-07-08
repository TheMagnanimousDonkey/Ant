using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedNest : MonoBehaviour
{
    
    
    
    public GameObject RedAnt;
    public GameObject RedSoldier;
    public List<Vector2> foodLocations;
    private List<Vector2> removedfoodLocations;
    public Text totalText;
    public Text foodText;
    public Text soldierText;
    void Start()
    {
        ManagerScript.Instance.RedFoodCount = 20;
        StartCoroutine(SpawnAnt());
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
        return new Vector2(0, 0);
    }
    IEnumerator SpawnAnt()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);



            if (ManagerScript.Instance.RedFoodCount > 0 && ManagerScript.Instance.RedAntCount < 70)
            {
                Instantiate(RedAnt, transform.position, transform.rotation);
                ManagerScript.Instance.RedAntCount++;
                ManagerScript.Instance.RedFoodCount--;



            }
            else if (ManagerScript.Instance.RedFoodCount > 30 && ManagerScript.Instance.RedSoldierCount < 0)
            {

                Instantiate(RedSoldier, transform.position, transform.rotation);
                ManagerScript.Instance.RedSoldierCount++;
                ManagerScript.Instance.RedFoodCount = ManagerScript.Instance.RedFoodCount - 30;


            }
            foodText.text = ManagerScript.Instance.RedFoodCount.ToString();
            totalText.text = ManagerScript.Instance.RedAntCount.ToString();
            soldierText.text = ManagerScript.Instance.RedSoldierCount.ToString();

        }

    }

}
