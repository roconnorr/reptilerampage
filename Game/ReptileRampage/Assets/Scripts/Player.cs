using UnityEngine;

public class Player : MonoBehaviour {

   public float speed = 10;

   private AudioSource soundSource;

   public ParticleSystem dustParticles;

   private Rigidbody2D rb;

   private float horizontal;
   private float vertical;
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
 
         Vector2 movement = new Vector2(horizontal, vertical);

         rb.AddForce(movement * speed / Time.deltaTime);
         //rb.velocity = movement * speed;
 
         if (rb.velocity.magnitude > speed)
         {
             rb.velocity = rb.velocity.normalized * speed;
         }
         Quaternion rotation = Quaternion.LookRotation(movement);
         dustParticles.transform.rotation = Quaternion.Lerp(dustParticles.transform.rotation, Quaternion.Inverse(rotation), 0.1f);
/*
        if(Input.GetKey("d")) {
            dustParticles.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
		} else if(Input.GetKey("a")) {
			dustParticles.transform.rotation = Quaternion.Euler(180f, -90f, 0f);
		} else if(Input.GetKey("w")) {
			dustParticles.transform.rotation = Quaternion.Euler(90f, -90f, 0f);
		} else if(Input.GetKey("s")) {
            dustParticles.transform.rotation = Quaternion.Euler(270f, -90f, 0f);
		} else {
			//dustParticles.Stop();
		}
*/
         if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
             if(!soundSource.isPlaying){
                soundSource.Play();
             }
        }else{
            soundSource.Stop();
        }
   }
}