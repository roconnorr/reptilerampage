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
	public bool automaticFire;
	public Player.WeaponType type;
	public Sprite sprite1;
	public Sprite sprite2;
	public Transform bulletPrefab;
	public Transform muzzleFlashPrefab;
	public AudioClip shotSound = null;
	float timeToFire = 0;
	public int rotationOffset = 0;
	public bool noRotation;

	private SpriteRenderer spriteRenderer;

	private Transform firePoint;
	private Transform firePoint1;
	private Transform firePoint2;
	private bool flipped;

	private GameObject player;

	private Player playerScript;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		Cursor.visible = false;
		firePoint1 = transform.FindChild ("FirePoint1");
		firePoint2 = transform.FindChild ("FirePoint2");
		firePoint = firePoint1;
		playerScript = GameObject.Find("Player").GetComponent<Player>();

	}

	void Update () {
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		if (!noRotation) {
			//Rotation
			transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
			if (rotZ < 0) {
				rotZ += 360;
			}
			if ((rotZ > 90 && rotZ < 270) && !flipped) {
				transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
				flipped = true;
			} else if ((rotZ > 0 && rotZ < 90 || rotZ > 270 && rotZ < 360) && flipped) {
				transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
				flipped = false;
			}
			if (rotZ > 45 && rotZ < 135) {
				spriteRenderer.sortingOrder = -9999;
				spriteRenderer.sprite = sprite2;
				firePoint = firePoint2;
			} else if (rotZ > 225 && rotZ < 315) {
				spriteRenderer.sortingOrder = 9999;
				spriteRenderer.sprite = sprite2;
				firePoint = firePoint2;
			} else {
				spriteRenderer.sortingOrder = 9999;
				spriteRenderer.sprite = sprite1;
				firePoint = firePoint1;
			}
		} else {
			firePoint.transform.rotation = Quaternion.Euler (0f, 0f, rotZ + rotationOffset);
			if (rotZ > 45 && rotZ < 135) {
				spriteRenderer.enabled = false;
			} else {
				spriteRenderer.enabled = true;
				spriteRenderer.sortingOrder = 9999;
			}
		}

		if (automaticFire && Input.GetButton ("Fire1") || !automaticFire && Input.GetButtonDown ("Fire1")) {
			if (Time.time > timeToFire) {
				if (playerScript.canShoot) {
					timeToFire = Time.time + 1 / fireRate;
					CreateBullet ();
				}
			}
		}
	}

	void CreateBullet () {
		float angle = bulletCount / 2 * -bulletSpread;
		for (int i = 0; i < bulletCount; i++) {
			//Create bullet with stray modifier
			float strayValue = Random.Range (-strayFactor, strayFactor);
			if (type == Player.WeaponType.grenade) {
				GameMaster.CreateGrenade (bulletPrefab, firePoint.position, firePoint.rotation.eulerAngles.z + strayValue - 90 + angle, damage, shotSpeed, range, false, true);
			} else {
				GameMaster.CreateBullet (bulletPrefab, firePoint.position, firePoint.rotation.eulerAngles.z + strayValue - 90 + angle, damage, shotSpeed, range, false, true);
			}
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
