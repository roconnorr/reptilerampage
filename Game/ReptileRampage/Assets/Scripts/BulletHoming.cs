using UnityEngine;

public class BulletHoming : MonoBehaviour {

	public GameObject explosionPrefab;

	public float moveSpeed;
	public int damage;
	public bool dmgPlayer;
	public bool dmgEnemy;
	public Transform target;
	private Rigidbody2D rb;
	public int iFrames;

	public AudioClip wallHitSound = null;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		iFrames = 50;
	}

	void Update () {
		if (iFrames > 0) {
			iFrames--;
		}
		if(rb.velocity.magnitude > moveSpeed) {
			rb.velocity = rb.velocity.normalized * moveSpeed;
		}
		rb.AddForce(Vector3.Normalize (target.position - transform.position) * 0.08f);
		float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
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
		if (other.gameObject.tag == "Player" && iFrames == 0) {
			if (dmgPlayer) {
				//damage player
			} else {
				Physics2D.IgnoreCollision (other.collider, gameObject.GetComponent<Collider2D> ());
			}
		} else {
			Physics2D.IgnoreCollision (other.collider, gameObject.GetComponent<Collider2D> ());
		}
	}

	//Collide with enemy
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy" && dmgEnemy && iFrames == 0) {
			Explode ();
			other.GetComponent<Enemy>().TakeDamage (damage, transform.rotation);
			Destroy (gameObject);
		}
	}

	void Explode(){
		GameObject explosion = (GameObject)Instantiate(explosionPrefab);
		explosion.transform.position = transform.position;
	}
}
