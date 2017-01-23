using UnityEngine;

public class Weapon : MonoBehaviour {

	public float damage;
	public float fireRate;
	public float shotSpeed;
	public float strayFactor;
	public float screenShake;
	
	public Transform bulletPrefab;
	public Transform muzzleFlashPrefab;
	public Transform crossHairPrefab;
	public AudioClip shotSound = null;
	float timeToFire = 0;

	Transform firePoint;
	Transform crossHair;

	void Awake () {
		firePoint = transform.FindChild ("FirePoint");
		crossHair = Instantiate (crossHairPrefab, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), transform.rotation) as Transform;
	}
	
	void Update () {
		crossHair.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
			timeToFire = Time.time + 1/fireRate;
			CreateBullet ();
		}
	}

	void CreateBullet () {
		//Create bullet with stray modifier
		float strayValue = Random.Range(-strayFactor, strayFactor);
		GameMaster.CreateBullet (bulletPrefab, firePoint.position, firePoint.rotation.eulerAngles.z + strayValue, damage, shotSpeed, false);
		//Play sound
		if(shotSound != null){
			AudioSource.PlayClipAtPoint(shotSound, transform.position);
		}
		//Shake screen
		gameObject.GetComponent<CameraShake>().StartShaking(screenShake);
		//Create muzzle flash - needs to have a custom one
		Transform flash = Instantiate (muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		flash.parent = firePoint;
		float size = Random.Range (0.05f, 0.1f);
		flash.localScale = new Vector3 (size, size, size);
		Destroy (flash.gameObject, 0.02f);
	}
}
