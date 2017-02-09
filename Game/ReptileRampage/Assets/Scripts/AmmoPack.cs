using UnityEngine;

public class AmmoPack : MonoBehaviour {

	public static int addedAmmo;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			if (other.GetComponentInChildren<Weapon> ().ammo != other.GetComponentInChildren<Weapon> ().maxAmmo) {
				if(other.GetComponent<Player>().slotActive == 0){
					PickUpLog.giveAmmoLog1 = true;
				} else if(other.GetComponent<Player>().slotActive == 1){
					PickUpLog.giveAmmoLog2 = true;
				}
				int originalAmmo = other.GetComponentInChildren<Weapon>().ammo;
				other.GetComponentInChildren<Weapon> ().AddAmmo (-2);
				int newAmmo = other.GetComponentInChildren<Weapon>().ammo;
				addedAmmo = newAmmo - originalAmmo;
				Destroy (gameObject);
			} else if(other.GetComponentInChildren<Weapon> ().ammo == other.GetComponentInChildren<Weapon> ().maxAmmo){
				if(other.GetComponent<Player>().slotActive == 0){
					PickUpLog.maxAmmoLog1 = true;
				} else if(other.GetComponent<Player>().slotActive == 1){
					PickUpLog.maxAmmoLog2 = true;
				}
			}
		}
	}
}
