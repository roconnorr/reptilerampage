using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosionPrefab;
	
	public AudioClip deathRoar;
	
	public float moveSpeed;
	public float damage;
	public bool dmgPlayer;


	public AudioClip wallHitSound = null;
	
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		// Check if the game object is visible, if not, destroy self   
		if(!Utility.isVisible(GetComponent<Renderer>(), Camera.main)) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Wall"){
			Explode();
			if (wallHitSound != null){
				AudioSource.PlayClipAtPoint(wallHitSound, transform.position);
			}
			Destroy(gameObject);
		}
		if(other.gameObject.tag == "Player"){
			Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>());
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.tag == "Enemy"){
			Explode();
			AudioSource.PlayClipAtPoint(deathRoar, transform.position);
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}

	void Explode(){
		GameObject explosion = (GameObject)Instantiate(explosionPrefab);
		explosion.transform.position = transform.position;
	}
}
