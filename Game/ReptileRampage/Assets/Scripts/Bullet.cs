using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosionPrefab;
	
	public int moveSpeed;
	
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
			Destroy(gameObject);
		}
		if(other.gameObject.tag == "Player"){
			Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>());
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.tag == "Enemy"){
			Explode();
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}

	void Explode(){
		GameObject explosion = (GameObject)Instantiate(explosionPrefab);
		explosion.transform.position = transform.position;
	}
}
