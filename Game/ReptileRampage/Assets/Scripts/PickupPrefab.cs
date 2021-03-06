﻿using UnityEngine;

public class PickupPrefab : MonoBehaviour {

	public Player.WeaponType type;

	public Sprite[] WeaponSprites;

	public GameObject[] Stars;

	public GameObject EIcon;

	private Player playerScript;

	private SpriteRenderer spriteRenderer; 

	public int ammo = -1;
	public static int addedAmmo;
	public static Player.WeaponType weaponLog;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		playerScript = GameObject.Find("Player").GetComponent<Player>();
		spriteRenderer.sprite = WeaponSprites[(int)type];
		for(int i=0; i<Stars.Length; i++){
            Stars[i].SetActive(false);
        }
		EIcon.SetActive(false);
	}

	public void ChangeType(Player.WeaponType newType, int ammoCount){
		ammo = ammoCount;
		type = newType;
		spriteRenderer.sprite = WeaponSprites[(int)type];
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			Player player = other.GetComponent<Player> ();
			if (type == Player.WeaponType.golddeserteagle) {
				Destroy (player.slot [2]);  
				player.slot [2] = Instantiate (player.weaponsprefabs [(int)type], player.transform.position + player.weaponsprefabs [(int)type].transform.position, new Quaternion (0, 0, 0, 0), player.transform);
				player.slot3type = type;
				if (GameMaster.currentLevel == 2) {
					GameMaster.levelStartSlot3Type = Player.WeaponType.golddeserteagle;
				}
				Destroy (gameObject);
			} else {
				if (other.GetComponent<Player> ().slot [0] != null && other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().type == type && other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().ammo != other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().maxAmmo) {
					PickUpLog.giveAmmoLog1 = true;
					int originalAmmo = other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().ammo;
					other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().AddAmmo (ammo);
					int newAmmo = other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().ammo;
					addedAmmo = newAmmo - originalAmmo;
					Destroy (gameObject);
				} else if (other.GetComponent<Player> ().slot [1] != null && other.GetComponent<Player> ().slot [1].GetComponent<Weapon> ().type == type && other.GetComponent<Player> ().slot [1].GetComponent<Weapon> ().ammo != other.GetComponent<Player> ().slot [1].GetComponent<Weapon> ().maxAmmo) {
					PickUpLog.giveAmmoLog2 = true;
					int originalAmmo = other.GetComponent<Player> ().slot [1].GetComponent<Weapon> ().ammo;
					other.GetComponent<Player> ().slot [1].GetComponent<Weapon> ().AddAmmo (ammo);
					int newAmmo = other.GetComponent<Player> ().slot [1].GetComponent<Weapon> ().ammo;
					addedAmmo = newAmmo - originalAmmo;
					weaponLog = other.GetComponent<Player> ().slot [1].GetComponent<Weapon> ().type;
					Destroy (gameObject);
				} else if (other.GetComponent<Player> ().slot [0] != null && other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().type == type && other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().ammo == other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().maxAmmo) {
					PickUpLog.maxAmmoLog1 = true;
				} else if (other.GetComponent<Player> ().slot [0] != null && other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().type == type && other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().ammo == other.GetComponent<Player> ().slot [0].GetComponent<Weapon> ().maxAmmo) {
					PickUpLog.maxAmmoLog2 = true;
				}
			}
		}
	}

	public void DisplayStars(){
		if (type != Player.WeaponType.golddeserteagle) {
			for(int i=0; i<Stars.Length; i++){
	            Stars[i].SetActive(false);
	        }
			for(int i=0; i<playerScript.weaponsprefabs[(int)type].GetComponent<Weapon>().stars; i++){
	            Stars[i].SetActive(true);
	        }
			EIcon.SetActive(true);
		}
	}

	public void HideStars(){
		if (type != Player.WeaponType.golddeserteagle) {
			for(int i=0; i<Stars.Length; i++){
	            Stars[i].SetActive(false);
	        }
			EIcon.SetActive(false);
		}
	}
}
