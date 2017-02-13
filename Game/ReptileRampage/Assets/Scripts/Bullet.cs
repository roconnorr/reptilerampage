﻿using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject animationPrefab;
	public GameObject explosionScriptPrefab;
	[HideInInspector]
	public float moveSpeed;
	[HideInInspector]
	public int damage;
	[HideInInspector]
	public float range;
	[HideInInspector]
	public bool dmgPlayer;
	[HideInInspector]
	public bool dmgEnemy;
	[HideInInspector]
	public float knockBackForce;
	[HideInInspector]
	public Transform source;
	public bool isExplosive;
	public bool isRPG;
	public AudioClip wallHitSound = null;

	private AudioSource audioSource;

	private static bool clipPlaying;

	private float timer = 0.0f;
	private float timeBetweenSounds = 0.01f;

	void Start(){
		audioSource = gameObject.GetComponent<AudioSource>();
		if(isRPG){
			audioSource.Play();
		}
	}
	
	void Update () {
		transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);

		if (range > 0) {
			range--;
		} else {
			Explode();
		}
	}

	//Collide with wall and player
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Wall" || other.gameObject.tag == "DestructibleWall"){
			if (wallHitSound != null){
				//AudioSource.PlayClipAtPoint(wallHitSound, transform.position);
				//audioSource.clip = wallHitSound;
				//audioSource.Play();
				if(!clipPlaying){
					PlayClipAt();
				}	
			Explode();
		}
		if(other.gameObject.tag == "Player" && dmgPlayer){
			if (!isExplosive) {
				other.gameObject.GetComponent<Player> ().TakeDamage (damage, transform.rotation, knockBackForce, source);
			}
			Explode ();
		}
		if (other.gameObject.tag == "Explosive"){
			other.gameObject.GetComponent<ExplosiveBarrel>().Destroy();
			Explode ();	
		}
		if (other.gameObject.tag == "Crate"){
			other.gameObject.GetComponent<Crate>().TakeDamage (damage);
			Explode ();	
		}
		if (other.gameObject.tag == "Enemy" && dmgEnemy) {
			if (!isExplosive) {
				other.gameObject.GetComponent<Enemy> ().TakeDamage (damage, transform.rotation, knockBackForce, source, false);
			}
			Explode ();
		}
	}
}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy" && dmgEnemy) {
			other.gameObject.GetComponent<EnemyBulletCollider>().TakeDamage (damage, transform.rotation, knockBackForce, source, false);
			Explode ();
		}
	}

	void Explode(){
		if(isRPG){
			gameObject.GetComponent<AudioSource>().Stop();
		}
		if (isExplosive) {
			GameMaster.CreateExplosion (animationPrefab, explosionScriptPrefab, transform.position, damage, 200, 2, !dmgPlayer);
		} else {
			if (explosionPrefab != null) {
				GameObject explosion = (GameObject)Instantiate (explosionPrefab);
				explosion.transform.position = transform.position;
			}
		}
		Destroy (gameObject);
	}

	bool PlayClipAt(){
		GameObject temp = new GameObject("TempAudio");
		temp.transform.position = gameObject.transform.position;
		temp.AddComponent<AudioSource>();
		AudioSource tempSource = temp.GetComponent<AudioSource>();
		tempSource.clip = wallHitSound;
		tempSource.volume = 0.04f;
		tempSource.Play();
		clipPlaying = true;
		Destroy(temp, tempSource.clip.length);
		clipPlaying = false;
		return temp;
	}
}
