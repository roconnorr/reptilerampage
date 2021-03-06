﻿using UnityEngine;
using System.Collections;

public class StegoTurret : MonoBehaviour {

	public int damage;
	public float fireRate;
	public float shotSpeed;
	public float range;
	public float knockbackForce;
	public float strayFactor;
	public GameObject target;
	public Transform bulletPrefab;
	public Transform muzzleFlashPrefab;
	public AudioClip shotSound = null;
	public int rotationOffset = 0;
	public int burstBulletLimit = 5;
	public float timeTilNextBurst = 3f;
	public int shootRange;

	private float timeToFire = 0;
	private bool trigger = true;
	private bool targetInShootRange = false;
	private SpriteRenderer spriteRenderer;
	private Transform firePoint;
	private int bulletCount;
	private bool flipped = false;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		firePoint = transform.Find ("FirePoint");
	}

	void Update () {
		if (GetComponentInParent<Stegosaurus> ().flipped && !flipped) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			flipped = true;
		} else if (!GetComponentInParent<Stegosaurus> ().flipped && flipped) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			flipped = false;
		}
		targetInShootRange = Vector3.Distance(gameObject.transform.position, target.transform.position) < shootRange;

		if(targetInShootRange && GetComponentInParent<Stegosaurus>().isChasing){
			Vector3 dir = target.transform.position - transform.position;
			dir.Normalize();
			float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
			if (rotZ < 0) {
				rotZ += 360;
			}
			spriteRenderer.flipY = (rotZ > 0 && rotZ < 90 || rotZ > 270 && rotZ < 360);

			if(trigger && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				CreateBullet();
				bulletCount++;	
			}
			if (trigger && bulletCount >= burstBulletLimit){
				StartCoroutine(Wait());
			}
		}
	}

	IEnumerator Wait() {
    	trigger = false;
    	yield return new WaitForSeconds(timeTilNextBurst);
		bulletCount = 0;
    	trigger = true;
 	}

	void CreateBullet () {
		//Create bullet with stray modifier
		float strayValue = Random.Range(-strayFactor, strayFactor);
		GameMaster.CreateBullet (bulletPrefab, firePoint.position, knockbackForce, firePoint.rotation.eulerAngles.z + strayValue + 90, damage, shotSpeed, range, true, false, GetComponentInParent<Stegosaurus>().transform);
		//Play sound
		if(shotSound != null){
			AudioSource.PlayClipAtPoint(shotSound, transform.position);
		}
		//Create muzzle flash - needs to have a custom one
		Transform flash = Instantiate (muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		flash.parent = firePoint;
		float size = Random.Range (0.1f, 0.15f);
		flash.localScale = new Vector3 (size, size, size);
		Destroy (flash.gameObject, 0.02f);
	}
}
