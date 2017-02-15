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
	public float knockBackForce = 100;
	public int stars;
	public int maxAmmo;
	public int startingAmmo;
	public int ammo;
	public int ammoPickup;
	public bool automaticFire;
	public bool infiniteAmmo;
	public Player.WeaponType type;
	public Sprite sprite1;
	public Sprite sprite2;
	public Transform bulletPrefab;
	public Transform muzzleFlashPrefab;
	public AudioClip shotSound = null;
	public AudioSource noBulletSound;
	float timeToFire = 0;
	public int rotationOffset = 0;
	public bool noRotation;

	private SpriteRenderer spriteRenderer;

	private Transform firePoint;
	private Transform firePoint1;
	private Transform firePoint2;
	private bool flipped;
	private Player player;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		firePoint1 = transform.FindChild ("FirePoint1");
		firePoint2 = transform.FindChild ("FirePoint2");
		firePoint = firePoint1;
		player = GetComponentInParent<Player>();
		noBulletSound = gameObject.GetComponent<AudioSource>();
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

		if (automaticFire && Input.GetButton ("Fire") || !automaticFire && Input.GetButtonDown ("Fire")) {
			if (Time.time > timeToFire) {
				if (player.canShoot && ammo > 0 && Time.timeScale != 0) {
					timeToFire = Time.time + 1 / fireRate;
					CreateBullet ();
					if (!infiniteAmmo) {
						ammo--;
					}
				}else if(player.canShoot && ammo == 0 && Time.timeScale != 0 && !noBulletSound.isPlaying){
					noBulletSound.Play();
					if(gameObject.GetComponentInParent<Player>().slotActive == 0){
						PickUpLog.noAmmoLog1 = true;
					} else if(gameObject.GetComponentInParent<Player>().slotActive == 1){
						PickUpLog.noAmmoLog2 = true;
					}
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
				GameMaster.CreateBullet (bulletPrefab, firePoint.position, knockBackForce, firePoint.rotation.eulerAngles.z + strayValue - 90 + angle, damage, shotSpeed, range, false, true, player.transform);
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
		if (muzzleFlashPrefab != null) {
			Transform flash = Instantiate (muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
			flash.parent = firePoint;
//			float size = Random.Range (0.1f, 0.13f);
//			flash.localScale = new Vector3 (size, size, size);
			Destroy (flash.gameObject, 0.1f);
		}
	}

	public void AddAmmo(int amount) {
		if (amount == -2) {
			ammo = Mathf.Min (maxAmmo, ammo + ammoPickup);
		} else if (amount == -1) {
			ammo = Mathf.Min (maxAmmo, ammo + startingAmmo);
		} else {
			ammo = Mathf.Min (maxAmmo, ammo + amount);
		}
	}
}
