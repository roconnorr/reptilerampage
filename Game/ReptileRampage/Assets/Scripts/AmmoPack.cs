using UnityEngine;

public class AmmoPack : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			other.GetComponentInChildren<Weapon>().AddAmmo();
			Destroy(gameObject);
		}
	}
}
