using UnityEngine;

public class AmmoPack : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			if (other.GetComponentInChildren<Weapon> ().ammo != other.GetComponentInChildren<Weapon> ().maxAmmo) {
				other.GetComponentInChildren<Weapon> ().AddAmmo (-2);
				Destroy (gameObject);
			}
		}
	}
}
