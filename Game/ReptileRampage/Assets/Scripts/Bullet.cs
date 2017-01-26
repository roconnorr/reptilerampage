using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosionPrefab;
	
	public float moveSpeed;
	public int damage;
	public bool dmgPlayer;
	public bool dmgEnemy;


	public AudioClip wallHitSound = null;
	
	void Update () {
		transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);
		// Check if the game object is visible, if not, destroy self   
		if(!Utility.isVisible(GetComponent<Renderer>(), Camera.main)) {
			Destroy(gameObject);
		}
	}

	//Collide with wall and player
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Wall"){
			Explode();
			if (wallHitSound != null){
				AudioSource.PlayClipAtPoint(wallHitSound, transform.position);
			}
			Destroy(gameObject);
		}
		if(other.gameObject.tag == "Player"){
			if (dmgPlayer) {
				//damage player
			} else {
				Physics2D.IgnoreCollision (other.collider, gameObject.GetComponent<Collider2D> ());
			}
		}
	}

	//Collide with enemy
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy" && dmgEnemy) {
			other.GetComponent<Enemy>().TakeDamage (damage, transform.rotation);
			Explode ();
			Destroy (gameObject);
		}
	}

	void Explode(){
		GameObject explosion = (GameObject)Instantiate(explosionPrefab);
		explosion.transform.position = transform.position;
	}
}
