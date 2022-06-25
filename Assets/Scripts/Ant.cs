using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public float maxSpeed = 2;
    public float steerStrength = 2;
    public float wanderStrength = 1;
    public float rotationSpeed = 720;

    private LineRenderer line;
    private Vector3 origin;
    private Vector3 endPoint;

    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;
    
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        line = this.gameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = true;
        //StartCoroutine(Delay());
    }
    void Update()
    {

        desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength).normalized;
        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.velocity = velocity;






        //shoot a line
        line.enabled = false;
        
        if (Input.GetMouseButton(0))
        {     

            line.startWidth = 0.05f;
            line.endWidth = 0.05f;
            //Find origin and end point
            origin = this.transform.position + this.transform.right * 0.3f * this.transform.lossyScale.y;
            RaycastHit2D hit = Physics2D.Raycast(origin, this.transform.right);

            line.SetPosition(0, origin);
            line.SetPosition(1, hit.point);
            line.enabled = true;
        }
    }


    IEnumerator Delay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength).normalized;
            Vector2 desiredVelocity = desiredDirection * maxSpeed;
            Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
            Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce,steerStrength) / 1;

            velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle );
            rb.velocity = velocity;
           

        }

    }
}
