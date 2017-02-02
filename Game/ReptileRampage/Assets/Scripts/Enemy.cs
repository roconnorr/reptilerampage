using UnityEngine;

public class Enemy : MonoBehaviour {
	
	public int health;
	public bool isTRex;
	public int meleeDamage = 10;

	public AudioClip deathRoar;
	
	public ParticleSystem bloodParticles;

	//public ParticleSystem dustParticles;
	//private Quaternion dustRotation;

	void Start() {
		//dustRotation = Quaternion.Euler(dir.eulerAngles.z - 90, -90, -5);
		//ParticleSystem localDustParticles = Instantiate(dustParticles, transform.position, dustRotation, transform) as ParticleSystem;
		//localDustParticles.Play();
	}

	void Update(){
	}

	public void TakeDamage(int amount, Quaternion dir) {
		if (isTRex && !GetComponent<TRex>().defencesDown) {
			//don't take damage
		} else {
			health -= amount;
			FireBloodParticles(dir);
			if (health <= 0) {
				AudioSource.PlayClipAtPoint (deathRoar, transform.position);
				Destroy (gameObject);
			}
		}
	}

	public void FireBloodParticles(Quaternion dir){
		Quaternion particleDir = Quaternion.Euler(dir.eulerAngles.z - 90, -90, -5);
		ParticleSystem localBloodParticles = Instantiate(bloodParticles, this.transform.position, particleDir) as ParticleSystem;
		localBloodParticles.Play();
	}


	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<Player>().TakeDamage (meleeDamage, transform.rotation);
			Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
			var dir = rb.transform.position - transform.position;
			rb.AddForce (dir.normalized * 6000f );
		}
	}
}
