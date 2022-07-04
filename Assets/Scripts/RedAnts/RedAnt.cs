using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAnt : MonoBehaviour
{
    public float maxSpeed = 2;
    public float steerStrength = 2;
    public float wanderStrength = 1;
    public float rotationSpeed = 720;
    public bool isCarryingFood = false;
    public bool isFighting = false;
    Vector2 nestLocation = new Vector2(3.47f, -0.48f);
    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;
    public Vector2 foodLocation;
    public int goTo = 0;
    public int maxHealth = 50;
    public Vector2 enemyLocation;
    public bool wanderOnly = false;
    public GameObject isItAlive;


    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

    }

    private void FixedUpdate()
    {
        if (isItAlive == null)
        {
            returnToWhatItWasDoing();
        }
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
                transform.right = foodLocation - transformPosition;
                Vector2 position = Vector2.MoveTowards(transform.position, foodLocation, (maxSpeed * 2) * Time.deltaTime);
                rb.MovePosition(position);
                break;
            case 2:
                transformPosition = transform.position;
                transform.right = nestLocation - transformPosition;
                position = Vector2.MoveTowards(transform.position, nestLocation, (maxSpeed * 2) * Time.deltaTime);
                rb.MovePosition(position);
                break;
            case 3:
                transformPosition = transform.position;
                transform.right = enemyLocation - transformPosition;
                position = Vector2.MoveTowards(transform.position, enemyLocation, (maxSpeed * 2) * Time.deltaTime);
                rb.MovePosition(position);
                break;

        }

    }

    private void OnTriggerStay2D(Collider2D col)
    {

        Nest nest = col.GetComponent<Nest>();
        BlackWeapon weapon = col.GetComponent<BlackWeapon>();

        if (weapon != null)
        {
            maxHealth = maxHealth - weapon.weaponStrength;
            if (maxHealth <= 0)
            {
                var attacker = col.gameObject.transform.parent.GetComponent<Ant>();
                if (attacker.foodLocation == new Vector2(0, 0))
                {
                    attacker.goTo = 0;
                }
                else if(attacker.foodLocation != new Vector2(0, 0))
                {
                    if (attacker.isCarryingFood)
                    {
                        attacker.goTo = 2;
                    }
                    else 
                    {
                        attacker.goTo = 1;
                    }
                }
                attacker.isFighting = false;
                Destroy(this.gameObject);
            }
        }
        if (wanderOnly == false)
        {
            if (nest != null)
            {

                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                if (this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == true)
                {
                    nest.handleFood(true);
                }
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                if (foodLocation != new Vector2(0, 0))
                {
                    nest.SetFoodLocation(foodLocation);
                    foodLocation = nest.GetFoodLocation();
                    isCarryingFood = false;
                    goTo = 1;
                }
                else
                {
                    foodLocation = nest.GetFoodLocation();
                    isCarryingFood = false;
                    if (foodLocation != new Vector2(0, 0))
                    {
                        goTo = 1;
                    }
                    goTo = 0;
                }

            }
        }
        

    }


    private void OnTriggerEnter2D(Collider2D col)

    {

        Food food = col.GetComponent<Food>();
        Aphid aphid = col.GetComponent<Aphid>();
        RedNest nest = col.GetComponent<RedNest>();
        RedAnt ant = col.GetComponent<RedAnt>();
        if (wanderOnly == false)
        {
            if (ant != null)
            {
                if (foodLocation == new Vector2(0, 0) && isFighting == false)
                {
                    if (ant.foodLocation != new Vector2(0, 0))
                    {
                        foodLocation = ant.foodLocation;
                        goTo = 1;
                    }
                }
            }
            if (aphid != null)
            {
                isCarryingFood = true;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                goTo = 2;

            }

            if (food != null && this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == false)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                foodLocation = food.transform.position;

                goTo = 1;
            }

            if (nest != null)
            {

                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                if (this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled == true)
                {
                    nest.handleFood(true);
                }
                this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                if (foodLocation != new Vector2(0, 0))
                {
                    nest.SetFoodLocation(foodLocation);
                    foodLocation = nest.GetFoodLocation();
                    isCarryingFood = false;
                    goTo = 1;
                }
                else
                {
                    foodLocation = nest.GetFoodLocation();
                    isCarryingFood = false;
                    if (foodLocation != new Vector2(0, 0))
                    {
                        goTo = 1;
                    }
                    goTo = 0;
                }

            }
        }
       
    }
    void OnTriggerExit(Collider col)
    {
        BlackWeapon bw = col.GetComponent<BlackWeapon>();

        returnToWhatItWasDoing();

        
    }

    private void returnToWhatItWasDoing()
    {
        if (foodLocation == new Vector2(0, 0))
        {
            goTo = 0;
        }
        else if (foodLocation != new Vector2(0, 0))
        {
            if (isCarryingFood)
            {
                goTo = 2;
            }
            else
            {
                goTo = 1;
            }
        }
        isFighting = false;
    }

}