using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickUpLog : MonoBehaviour {

	private Text log;

	public static bool giveHealthLog;
	public static bool maxHealthLog;

	public static bool giveAmmoLog1;
	public static bool giveAmmoLog2;
	public static bool maxAmmoLog1;
	public static bool maxAmmoLog2;
	public static bool noAmmoLog1;
	public static bool noAmmoLog2;

	public static bool giveGrenadeLog;
	public static bool maxGrenadeLog;
	public static bool noGrenadeLog;

	public bool healthLog;
	public bool ammoLog1;
	public bool ammoLog2;
	public bool ammoLog3;

	void Start(){
		log = gameObject.GetComponent<Text>();
	}
	
	void Update(){
		//Health
		if(healthLog){
			if(giveHealthLog){
				log.text = "+ " + HealthPack.addedHealth + " HP";
				StartCoroutine(ShowMessage(1));
				giveHealthLog = false;
			} else if(maxHealthLog){
				log.text = "Maximum";
				StartCoroutine(ShowMessage(1));
				maxHealthLog = false;
			}	
		//Ammo
		}else if(ammoLog1){
			if(giveAmmoLog1){
				if(PickupPrefab.addedAmmo != 0){
					log.text = "+ " + PickupPrefab.addedAmmo + " Ammo";
				}else if(AmmoPack.addedAmmo != 0){
					log.text = "+ " + AmmoPack.addedAmmo + " Ammo";
				}
				StartCoroutine(ShowMessage(1));
				giveAmmoLog1 = false;
			}else if(maxAmmoLog1){
				log.text = "Maximum";
				StartCoroutine(ShowMessage(1));
				maxAmmoLog1 = false;
			}else if(noAmmoLog1){
				log.text = "Empty";
				StartCoroutine(ShowMessage(1));
				noAmmoLog1 = false;
			}
		}else if(ammoLog2){
			if(giveAmmoLog2){
				if(PickupPrefab.addedAmmo != 0){
					log.text = "+ " + PickupPrefab.addedAmmo + " Ammo";
				}else if(AmmoPack.addedAmmo != 0){
					log.text = "+ " + AmmoPack.addedAmmo + " Ammo";
				}
				StartCoroutine(ShowMessage(1));
				giveAmmoLog2 = false;
			}else if(maxAmmoLog2){
				log.text = "Maximum";
				StartCoroutine(ShowMessage(1));
				maxAmmoLog2 = false;
			}else if(noAmmoLog2){
				log.text = "Empty";
				StartCoroutine(ShowMessage(1));
				noAmmoLog2 = false;
			}
		//Grenade
		}else if(ammoLog3){
			if(giveGrenadeLog){
				log.text = "+ 1 grenade";
				StartCoroutine(ShowMessage(1));
				giveGrenadeLog = false;
			}else if(maxGrenadeLog){
				log.text = "Maximum";
				StartCoroutine(ShowMessage(1));
				maxGrenadeLog = false;
			}else if(noGrenadeLog){
				log.text = "Empty";
				StartCoroutine(ShowMessage(1));
				noGrenadeLog = false;
			}
		}
	}

 	IEnumerator ShowMessage (float delay) {
     	log.enabled = true;
     	yield return new WaitForSeconds(delay);
     	log.enabled = false;
 	}
}
