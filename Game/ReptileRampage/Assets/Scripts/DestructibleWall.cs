using UnityEngine;

public class DestructibleWall : MonoBehaviour {

	//private AudioSource soundSource;

	public int health = 100;
    
    void Start() {
		//soundSource = gameObject.GetComponent<AudioSource>();
    }

	public void TakeDamage(int amount) {
		health -= amount;
		if (health <= 0) {
			//AudioSource.PlayClipAtPoint (deathRoar, transform.position);
			Destroy (gameObject);
		}
	}
}
