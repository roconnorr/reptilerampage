using UnityEngine;

public class Weapon : MonoBehaviour {

	public int damage;
	public float fireRate;
	public float shotSpeed;
	public float range;
	public float strayFactor;
	public float screenShake;
	public float bulletCount;
	public float bulletSpread;
	
	public Transform bulletPrefab;
	public Transform muzzleFlashPrefab;
	public AudioClip shotSound = null;
	float timeToFire = 0;
	public int rotationOffset = 0;

	private SpriteRenderer spriteRenderer;

	private Transform firePoint;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		Cursor.visible = false;
		firePoint = transform.FindChild ("FirePoint");
	}

	void Update () {
		//Rotation
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
		if (rotZ < 0) {
			rotZ += 360;
		}
		spriteRenderer.flipY = !(rotZ > 0 && rotZ < 90 || rotZ > 270 && rotZ < 360);
		if (rotZ > 45 && rotZ < 135) {
			spriteRenderer.sortingOrder = -9999;
		} else {
			spriteRenderer.sortingOrder = 9999;
		}

		if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
			timeToFire = Time.time + 1/fireRate;
			CreateBullet ();
		}
	}

	void CreateBullet () {
		float angle = bulletCount / 2 * -bulletSpread;
		for (int i = 0; i < bulletCount; i++) {
			//Create bullet with stray modifier
			float strayValue = Random.Range (-strayFactor, strayFactor);
			GameMaster.CreateBullet (bulletPrefab, firePoint.position, firePoint.rotation.eulerAngles.z + strayValue - 90 + angle, damage, shotSpeed, range, false, true);
			angle += bulletSpread;
		}
		//Play sound
		if(shotSound != null){
			AudioSource.PlayClipAtPoint(shotSound, transform.position);
		}
		//Shake screen
		gameObject.GetComponent<CameraShake>().StartShaking(screenShake);
		//Create muzzle flash - needs to have a custom one
		Transform flash = Instantiate (muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		flash.parent = firePoint;
		float size = Random.Range (0.1f, 0.13f);
		flash.localScale = new Vector3 (size, size, size);
		Destroy (flash.gameObject, 0.02f);
	}
}
