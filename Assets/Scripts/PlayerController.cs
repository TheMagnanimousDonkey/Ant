using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
   

    

    private Rigidbody2D rb;
    

  



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
 

 
    private void FixedUpdate()
    {

        

    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float jump = Input.GetAxisRaw("Jump");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal * moveSpeed, rb.velocity.y);






 
        rb.velocity = movement;
    }
}
