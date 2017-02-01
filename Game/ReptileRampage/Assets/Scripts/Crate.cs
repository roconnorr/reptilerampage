using UnityEngine;
using System.Collections.Generic;
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

			/*
			PickupPrefab pickupPrefab = pickup.GetComponent<PickupPrefab>();
			pickupPrefab.spriteRenderer.sprite = pickupPrefab.WeaponSprites[1];
			Instantiate(pickupPrefab, transform.position, transform.rotation);
			Destroy (gameObject);*/
		}
	}
}