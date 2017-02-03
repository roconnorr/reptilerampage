﻿using UnityEngine;

public class Crate : MonoBehaviour {

	//private AudioSource soundSource;

	public int health = 30;

	//public List<GameObject> weaponPrefabs;

	public GameObject pickup;
 
    
    void Start() {
		//soundSource = gameObject.GetComponent<AudioSource>();
    }

	public void TakeDamage(int amount) {
		health -= amount;
		if (health <= 0) {
			//AudioSource.PlayClipAtPoint (deathRoar, transform.position);
			//GameObject weapon = weaponPrefabs[Random.Range(0,weaponPrefabs.Count)];
 			//Instantiate(weapon, transform.position, transform.rotation);
			Destroy (gameObject);
			GameObject gun = Instantiate(pickup, transform.position, transform.rotation);
			PickupPrefab pickupPrefab = gun.GetComponent<PickupPrefab>();
			int gunNum = pickupPrefab.WeaponSprites.Length;
			pickupPrefab.type = (Player.WeaponType) Random.Range(0, gunNum);			
			
		}
	}
}