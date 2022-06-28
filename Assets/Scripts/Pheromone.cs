using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pheromone : MonoBehaviour
{
    public Vector2 foodLocation;

    public void SetLocation(Vector2 location)
    { 
        foodLocation = location;
    }
}
