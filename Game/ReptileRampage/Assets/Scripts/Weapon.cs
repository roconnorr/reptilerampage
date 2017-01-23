﻿using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	public float strayFactor = 4;
	
	public Transform BulletTrailPrefab;
	public Transform MuzzleFlashPrefab;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;

	float timeToFire = 0;
	Transform firePoint;

	void Awake () {
		firePoint = transform.FindChild ("FirePoint");
	}
	
	void Update () {
		if (fireRate == 0) {
			if (Input.GetButton ("Fire1")) {
				Shoot();
				gameObject.GetComponent<CameraShake>().StartShaking(0.05f);
			}
		} else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
				gameObject.GetComponent<CameraShake>().StartShaking(0.05f);
			}
		}
	}
	
	void Shoot () {
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100f, whatToHit);
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
		}
	}
	
	void Effect () {
		float randomNumberX = Random.Range(-strayFactor, strayFactor);
     	float randomNumberY = Random.Range(-strayFactor, strayFactor);
     	float randomNumberZ = Random.Range(-strayFactor, strayFactor);
     	Transform bullet = Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation);
    	bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
		Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		clone.parent = firePoint;
		float size = Random.Range (0.05f, 0.1f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);
	}
}
