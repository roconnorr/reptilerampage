using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject animationPrefab;
	public GameObject explosionScriptPrefab;
	[HideInInspector]
	public float moveSpeed;
	[HideInInspector]
	public int damage;
	[HideInInspector]
	public float range;
	[HideInInspector]
	public bool dmgPlayer;
	[HideInInspector]
	public bool dmgEnemy;
	[HideInInspector]
	public float knockBackForce;
	public bool isExplosive;
	public bool isRPG;
	public AudioClip wallHitSound = null;

	void Start(){
		if(isRPG){
			gameObject.GetComponent<AudioSource>().Play();
		}
	}
	
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
		if(other.gameObject.tag == "Wall" || other.gameObject.tag == "DestructibleWall"){
			if (wallHitSound != null){
				AudioSource.PlayClipAtPoint(wallHitSound, transform.position);
			}
			Explode();
		}
		if(other.gameObject.tag == "Player" && dmgPlayer){
			other.gameObject.GetComponent<Player>().TakeDamage (damage, transform.rotation, knockBackForce);
			Explode ();
		}
		if (other.gameObject.tag == "Explosive"){
			other.gameObject.GetComponent<ExplosiveBarrel>().Destroy();
			Explode ();	
		}
		if (other.gameObject.tag == "Crate"){
			other.gameObject.GetComponent<Crate>().TakeDamage (damage);
			Explode ();	
		}
		if (other.gameObject.tag == "Enemy" && dmgEnemy) {
			other.gameObject.GetComponent<Enemy>().TakeDamage (damage, transform.rotation, knockBackForce);
			Explode ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy" && dmgEnemy) {
			other.gameObject.GetComponent<EnemyBulletCollider>().TakeDamage (damage, transform.rotation, knockBackForce);
			Explode ();
		}
	}

	void Explode(){
		if(isRPG){
			gameObject.GetComponent<AudioSource>().Stop();
		}
		if (isExplosive) {
			GameMaster.CreateExplosion (animationPrefab, explosionScriptPrefab, transform.position, damage, 200, 2, !dmgPlayer);
		} else {
			if (explosionPrefab != null) {
				GameObject explosion = (GameObject)Instantiate (explosionPrefab);
				explosion.transform.position = transform.position;
			}
		}
		Destroy (gameObject);
	}
}
