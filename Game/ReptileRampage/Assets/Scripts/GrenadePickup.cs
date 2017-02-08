using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			if (other.GetComponent<Player> ().grenadeCount < 5) {
				other.GetComponent<Player> ().grenadeCount++;
				Destroy (gameObject);
			}
		}
	}
}
