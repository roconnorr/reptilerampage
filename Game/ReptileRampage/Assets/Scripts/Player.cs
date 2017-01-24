using UnityEngine;

public class Player : MonoBehaviour {

   public float speed = 10;

   private AudioSource soundSource;

   private Animator animator;
   Rigidbody2D rb;

   float horizontal;
   float vertical;
   void Start() {
        rb = GetComponent<Rigidbody2D>();
        soundSource = gameObject.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

   void OnCollisionEnter2D(Collision2D other){
    	rb.velocity = Vector3.zero;
    }

    void Update() {

         if(Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s")) {
             animator.Play("MoveRight");
         }
         if(Input.GetKey("d") && Input.GetKey("w")) { 
             animator.Play("MoveUp_Right");
         }
         if(Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s")) {
             animator.Play("MoveLeft");
         }
         if(Input.GetKey("a") && Input.GetKey("w")) {
             animator.Play("MoveUp_Left");
         }
         if(Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("d")) {
             animator.Play("MoveUp");
         }
         if(Input.GetKey("s") && Input.GetKey("d")) {
             animator.Play("MoveDown_Right");
         }
         if(Input.GetKey("s") && !Input.GetKey("d") && !Input.GetKey("a")) {
             animator.Play("MoveDown");
         }
         if(Input.GetKey("s") && Input.GetKey("a")) {
             animator.Play("MoveDown_Left");
         }


         if(horizontal == 0 && vertical == 0) {
             animator.Play("Idle");
         }

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