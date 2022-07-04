using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSoldier : MonoBehaviour
{
    public float maxSpeed = 2;
    public float steerStrength = 1;
    public float wanderStrength = 1;
    public float rotationSpeed = 720;


    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;
    public int maxHealth = 200;
    public Vector2 enemyLocation;
    public int goTo = 0;


    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
                transform.right = enemyLocation - transformPosition;
                Vector2 position = Vector2.MoveTowards(transform.position, enemyLocation, (maxSpeed * 2) * Time.deltaTime);
                rb.MovePosition(position);
                break;
            case 2:
                transformPosition = transform.position;
                Vector2 home = new Vector2(0, 0);
                transform.right = home - transformPosition;
                position = Vector2.MoveTowards(transform.position, home, (maxSpeed * 2) * Time.deltaTime);
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
                else if (attacker.foodLocation != new Vector2(0, 0))
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
    }
   
}
