using UnityEngine;

public class HealthPack : MonoBehaviour {

	public int addHealth = 10;
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			if (other.GetComponent<Player> ().health != other.GetComponent<Player> ().maxHealth) {
				other.GetComponent<Player> ().health = Mathf.Min (other.GetComponent<Player> ().maxHealth, other.GetComponent<Player> ().health + addHealth);
				Destroy (gameObject);
			}
		}
	}
}