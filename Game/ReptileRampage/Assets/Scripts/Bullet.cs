using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosionPrefab;
	
	public float moveSpeed;
	public int damage;
	public float range;
	public bool dmgPlayer;
	public bool dmgEnemy;


	public AudioClip wallHitSound = null;
	
	void Update () {
		transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);

		if (range > 0) {
			range--;
		} else {
			Explode ();
		}
	}

	//Collide with wall and player
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Wall"){
			if (wallHitSound != null){
				AudioSource.PlayClipAtPoint(wallHitSound, transform.position);
			}
			Explode();
		}
		if(other.gameObject.tag == "Player"){
			if (dmgPlayer) {
				Explode ();
			}
		}
	}

	//Collide with enemy
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy" && dmgEnemy) {
			other.GetComponent<Enemy>().TakeDamage (damage, transform.rotation);
			SumScore.Add(100);
			SumScore.SaveHighScore();
			Explode ();
		}
	}

	void Explode(){
		GameObject explosion = (GameObject)Instantiate(explosionPrefab);
		explosion.transform.position = transform.position;
		Destroy (gameObject);
	}
}
