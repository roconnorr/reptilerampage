using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float damage = 10;
	public float strayFactor = 4;
	
	public Transform bulletPrefab;
	public Transform muzzleFlashPrefab;
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
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}
	}

	void Effect () {
		//Create bullet with stray modifier
		float randomNumberZ = Random.Range(-strayFactor, strayFactor);
		GameMaster.CreateBullet (bulletPrefab, firePoint.position, firePoint.rotation.eulerAngles.z + randomNumberZ, damage, 20, false);
		//Create muzzle flash
		Transform flash = Instantiate (muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		flash.parent = firePoint;
		float size = Random.Range (0.05f, 0.1f);
		flash.localScale = new Vector3 (size, size, size);
		Destroy (flash.gameObject, 0.02f);
	}
}
