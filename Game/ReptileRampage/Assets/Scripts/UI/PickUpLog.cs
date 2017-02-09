using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PickUpLog : MonoBehaviour {

	private Text log;

	public static bool giveHealthLog;
	public static bool maxHealthLog;
	public static bool giveAmmoLog;
	public static bool maxAmmoLog;
	public bool healthLog;
	public bool ammoLog;

	void Start(){
		log = gameObject.GetComponent<Text>();
	}
	
	void Update(){
		if(healthLog){
			if(giveHealthLog){
				log.text = "You gained " + HealthPack.addedHealth + " HP";
				StartCoroutine(ShowMessage(1));
				giveHealthLog = false;
			} else if(maxHealthLog){
				log.text = "Maximum health already!";
				StartCoroutine(ShowMessage(1));
				maxHealthLog = false;
			}	
		}else if(ammoLog){
			if(giveAmmoLog){
				log.text = "Picked up ammo";
				StartCoroutine(ShowMessage(1));
				giveAmmoLog = false;
			}else if(maxAmmoLog){
				log.text = "Maximum ammo already!";
				StartCoroutine(ShowMessage(1));
				maxAmmoLog = false;
			}
		}
	}

 	IEnumerator ShowMessage (float delay) {
     	log.enabled = true;
     	yield return new WaitForSeconds(delay);
     	log.enabled = false;
 	}
}
