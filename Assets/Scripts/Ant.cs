using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public float maxSpeed = 2;
    public float steerStrength = 2;
    public float wanderStrength = 1;
    public float rotationSpeed = 720;
    public GameObject phermonePrefab;
    public bool isCarryingFood = false;

    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;
    public Vector2 foodLocation;
    public int goTo = 0;
    

    Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        //StartCoroutine(Delay());
    }
    void Update()
    {
        Vector2 desiredVelocity;
        Vector2 desiredSteeringForce;
        Vector2 acceleration;
        float angle;

        switch (goTo)
        { 
            case 0:
                desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength).normalized;
                desiredVelocity = desiredDirection * maxSpeed;
                desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
                acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;
                velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
                angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
                rb.velocity = velocity;
                break;
            case 1:
                Vector2 transformPosition = transform.position;
                transform.right = desiredDirection - transformPosition;
                Vector2 position = Vector2.MoveTowards(transform.position, desiredDirection, (maxSpeed * 2) * Time.deltaTime);
                rb.MovePosition(position);
                break;
            case 2:
                transformPosition = transform.position;
                Vector2 home = new Vector2(0,0);
                transform.right = home - transformPosition;
                position = Vector2.MoveTowards(transform.position, home, (maxSpeed * 2) * Time.deltaTime);
                rb.MovePosition(position);
                
                     
                break;


        }


        
        
        




    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Ant ant = col.GetComponent<Ant>();
        if (ant != null)
        {
            this.transform.position += transform.right * Time.deltaTime * maxSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)

    {
        Food food = col.GetComponent<Food>();
        Aphid aphid = col.GetComponent<Aphid>();
        Nest nest = col.GetComponent<Nest>();
        Pheromone pheromone = col.GetComponent<Pheromone>();

        if (pheromone != null)
        {
            if (isCarryingFood)
            {
                pheromone.SetLocation(foodLocation);
            }
            else 
            {
                foodLocation = pheromone.foodLocation;
                desiredDirection = foodLocation;
                goTo = 1;
            }
            
        }
        if (aphid != null)
        {
            isCarryingFood = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            goTo = 2;
            StartCoroutine(LayPheromone(isCarryingFood));
        }

        if (food != null && this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == false)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            desiredDirection = food.transform.position;
            foodLocation = desiredDirection;
            goTo = 1;
        }

        if (nest != null)
        {
            
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            
            this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            if (foodLocation != new Vector2(0,0))
            {
                desiredDirection = foodLocation;
                isCarryingFood = false;
                goTo = 1; 
            }
            else
            {
                isCarryingFood = false;
                goTo = 0; 
            }
            
        }
    }

    IEnumerator LayPheromone(bool isCarrying)
    {
        while (isCarrying)
        {
            yield return new WaitForSeconds(10f);

            
            Instantiate(phermonePrefab, transform.position, Quaternion.Euler(0, 0, transform.rotation.z));

        }



      

    }
}
