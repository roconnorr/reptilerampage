using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

	[HideInInspector]
	public Rigidbody2D rb;
	
	public int health;
	public bool isTrike;
	public bool isTRex;
	public bool isGavin;
	public float knockbackModifier;
	public int meleeDamage = 10;
	public AudioClip deathRoar;
	
	public ParticleSystem bloodParticles;

	private Vector3 knockback;
	private int knockbackTimer = 0;

	//public ParticleSystem dustParticles;
	//private Quaternion dustRotation;

	void Start() {
		//dustRotation = Quaternion.Euler(dir.eulerAngles.z - 90, -90, -5);
		//ParticleSystem localDustParticles = Instantiate(dustParticles, transform.position, dustRotation, transform) as ParticleSystem;
		//localDustParticles.Play();
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		//Knockback
		if (knockbackTimer > 0) {
			rb.AddForce (knockback);
			knockbackTimer -= 1;
		}
	}

	public void TakeDamage(int amount, Quaternion dir, float force) {
		knockback = dir * Vector2.up;
		knockback *= (force / 2);
		knockback *= knockbackModifier;
		knockbackTimer = 3;
		if (isTRex && !GetComponent<TRex>().defencesDown) {
			//don't take damage
		} else {
			health -= amount;
			FireBloodParticles(dir);
			if (health <= 0) {
				AudioSource.PlayClipAtPoint (deathRoar, transform.position);
				if(isTrike){
					//possibly some more dialogue
         			SceneManager.LoadScene("Level2");
				}else if(isTRex){
					//possibly some more dialogue
					SceneManager.LoadScene("Level3");
				}else if(isGavin){
					//you win
				}
				Destroy (gameObject);
			}
		}
	}

	/*Vector2 GetXYDirection(float angle, float magnitude){
    	return Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * magnitude;
	}*/

	public void FireBloodParticles(Quaternion dir){
		Quaternion particleDir = Quaternion.Euler(dir.eulerAngles.z - 90, -90, -5);
		ParticleSystem localBloodParticles = Instantiate(bloodParticles, this.transform.position, particleDir) as ParticleSystem;
		localBloodParticles.Play();
	}


	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			float angle =Mathf.Atan2(other.transform.position.y-transform.position.y, other.transform.position.x-transform.position.x)*180 / Mathf.PI;
			angle -= 90;
			other.gameObject.GetComponent<Player>().TakeDamage (meleeDamage, Quaternion.Euler(0, 0, angle), 500);
		}
	}
}
