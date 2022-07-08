using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    public int destructionTime = 60;
 
    void Update()
    {
        
        Destroy(gameObject, destructionTime);
    }
}
