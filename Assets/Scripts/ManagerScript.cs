
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
public static ManagerScript Instance { get; private set; }

    public int RedAntCount = 0;
    public int BlackAntCount = 0;
    public int RedSoldierCount = 0;
    public int BlackSoldierCount = 0;
    public int RedFoodCount = 0;
    public int BlackFoodCount = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
    }
}
