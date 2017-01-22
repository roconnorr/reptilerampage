using UnityEngine;

public class Player : MonoBehaviour {

   public float speed = 10;

   Rigidbody2D rb;

   void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

   void OnCollisionEnter2D(Collision2D other){
    	rb.velocity = Vector3.zero;
    }

   void FixedUpdate () {

         float horizontal = Input.GetAxis("Horizontal");
         float vertical = Input.GetAxis("Vertical");
 
         Vector3 movement = new Vector2(horizontal, vertical);

         rb.AddForce(movement * speed / Time.deltaTime);
 
         if (rb.velocity.magnitude > speed)
         {
             rb.velocity = rb.velocity.normalized * speed;
         }
   }
}