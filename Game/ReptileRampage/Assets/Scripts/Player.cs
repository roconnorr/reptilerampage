using UnityEngine;

public class Player : MonoBehaviour {

   public float speed = 10;

   private AudioSource soundSource;

   Rigidbody2D rb;

   float horizontal;
   float vertical;
   void Start() {
        rb = GetComponent<Rigidbody2D>();
        soundSource = gameObject.GetComponent<AudioSource>();
    }

   void OnCollisionEnter2D(Collision2D other){
    	rb.velocity = Vector3.zero;
    }

   void FixedUpdate () {

         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");
 
         Vector3 movement = new Vector2(horizontal, vertical);

         rb.AddForce(movement * speed / Time.deltaTime);
         //rb.velocity = movement * speed;
 
         if (rb.velocity.magnitude > speed)
         {
             rb.velocity = rb.velocity.normalized * speed;
         }

         if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
             if(!soundSource.isPlaying){
                soundSource.Play();
             }
        }else{
            soundSource.Stop();
        }
   }
}