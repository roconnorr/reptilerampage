using UnityEngine;

public class BulletHoming : MonoBehaviour {

	public GameObject explosionPrefab;

	public float moveSpeed;
	public float turnSpeed;
	public int turnDelay;
	public int damage;
	public bool dmgPlayer;
	public Transform target;

	private int turnCount = 0;

	public AudioClip wallHitSound = null;

	void Update () {
		Vector3 difference = target.position - transform.position;
		float rotation = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		if (rotation < 0) {
			rotation += 360;
		}
		float a = rotation -transform.rotation.eulerAngles.z;
		if (a < 0) {
			a += 360;
		}
		if (turnCount == 0) {
			if (a < 80 || a > 270) {
				transform.Rotate (0, 0, -turnSpeed);
			} else if (a > 100 && a < 270) {
				transform.Rotate (0, 0, turnSpeed);
			} else {
				turnCount = turnDelay;
			}
		} else {
			turnCount--;
		}

		transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);
		// Check if the game object is visible, if not, destroy self   
//		if(!Utility.isVisible(GetComponent<Renderer>(), Camera.main)) {
//			Destroy(gameObject);
//		}
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

		if (other.gameObject.tag == "Enemy" && !dmgPlayer) {
			Explode ();
			other.GetComponent<Enemy>().TakeDamage (damage);
			Destroy (gameObject);
		}
	}

	void Explode(){
		GameObject explosion = (GameObject)Instantiate(explosionPrefab);
		explosion.transform.position = transform.position;
	}
}
