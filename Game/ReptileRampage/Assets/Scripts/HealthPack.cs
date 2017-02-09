using UnityEngine;

public class HealthPack : MonoBehaviour {

	public int addHealth = 10;

	public static int addedHealth;
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			if (other.GetComponent<Player> ().health != Player.playerMaxHP) {
				int originalHealth = other.GetComponent<Player> ().health;
				other.GetComponent<Player> ().health = Mathf.Min (Player.playerMaxHP, other.GetComponent<Player> ().health + addHealth);
				int newHealth = other.GetComponent<Player> ().health;
				addedHealth = newHealth - originalHealth;
				PickUpLog.giveHealthLog = true;
				Destroy (gameObject);
			} else if (other.GetComponent<Player> ().health == Player.playerMaxHP){
				PickUpLog.maxHealthLog = true;
			}
		}
	}
}