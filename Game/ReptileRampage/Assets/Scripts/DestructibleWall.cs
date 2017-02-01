using UnityEngine;

public class DestructibleWall : MonoBehaviour {

	//private AudioSource soundSource;

    
    void Start() {
		//soundSource = gameObject.GetComponent<AudioSource>();
    }

	public void destroy() {
		//AudioSource.PlayClipAtPoint (deathRoar, transform.position);
		Destroy (gameObject);
	}
}
