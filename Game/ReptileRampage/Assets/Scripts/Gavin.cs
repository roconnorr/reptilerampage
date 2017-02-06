using UnityEngine;

public class Gavin : MonoBehaviour {

	private enum State {Idle, Shooting, Laser, Spawning};

	private State state;

	private SpriteRenderer sr;
	private Transform laser;
	private Transform firePoint;
	private Transform arm1;
	private Transform arm2;
	private Transform spawnPoint1;
	private Transform spawnPoint2;

	public Transform target;
	public Transform bulletPrefab;
	public Transform spawnPrefab;
	public Transform muzzleFlashPrefab;
	public Transform grid;
	public AudioClip shotSound = null;

	public int bulletDamage;
	public float fireRate;
	public float shotSpeed;
	public float range;
	public float knockbackForce;
	public float strayFactor;
	public float speed;
	public int laserDamage;

	[HideInInspector]
	public int dir = 1;
	private bool isLaser = false;
	private bool bulletAttack1 = false;
	private bool bulletAttack2 = false;
	private float timeToFire = 0;
	private float timeToSpawn = 0;
	private int bullet1Count = 0;
	private int bullet2Count = 0;
	private int spawnCount = 0;

	private HUDManager hudManager;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		laser = transform.FindChild ("Laser");
		arm1 = transform.FindChild ("Arm1");
		arm2 = transform.FindChild ("Arm2");
		firePoint = arm1.FindChild ("FirePoint");
		spawnPoint1 = GameObject.Find ("SpawnPoint1").transform;
		spawnPoint2 = GameObject.Find ("SpawnPoint2").transform;
		hudManager = GameObject.Find("Canvas").GetComponent<HUDManager>();
		hudManager.levelBoss = this.gameObject;
		hudManager.inBossFight = true;
		hudManager.SetBossHealthActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null){
			Destroy(gameObject);
			return;
		}
		transform.Translate (new Vector3 (0, speed * 0.02f * dir));
		float angle =Mathf.Atan2(target.transform.position.y-transform.position.y, target.transform.position.x-transform.position.x)*180 / Mathf.PI;
		angle += 180;
		arm1.localRotation = Quaternion.Euler (0, 0, angle);
		arm2.localRotation = Quaternion.Euler (0, 0, angle);

		float random = Random.Range (0, 600);
		if (state == State.Idle) {
			if (random < 4) {
				state = State.Laser;
			} else if (random < 8) {
				state = State.Shooting;
				bulletAttack1 = false;
				bullet1Count = 0;
			} else if (random == 8) {
				state = State.Spawning;
			}
		}
		if (state == State.Laser && !isLaser) {
			isLaser = true;
			sr.color = Color.blue;
			Invoke ("FireLaser", 1f);
		}
		if (state == State.Shooting && Time.time > timeToFire) {
			bulletAttack2 = true;
			timeToFire = Time.time + 1/(fireRate);
			CreateBullet();
			bullet2Count++;
			if (bullet2Count > 50) {
				bullet2Count = 0;
				bulletAttack2 = false;
				state = State.Idle;
			}
		}
		if (state == State.Spawning && Time.time > timeToSpawn) {
			Transform a = Instantiate (spawnPrefab, spawnPoint1.transform.position, new Quaternion(0,0,0,0));
			Transform b = Instantiate (spawnPrefab, spawnPoint2.transform.position, new Quaternion(0,0,0,0));
			a.GetComponent<Velociraptor> ().target = target.gameObject;
			b.GetComponent<Velociraptor> ().target = target.gameObject;
			a.GetComponent<AStarPathfinder> ().gridObject = grid.gameObject;
			b.GetComponent<AStarPathfinder> ().gridObject = grid.gameObject;
			a.GetComponent<Velociraptor> ().sightRange = 100;
			b.GetComponent<Velociraptor> ().sightRange = 100;
			a.GetComponent<Velociraptor> ().chaseRange = 100;
			b.GetComponent<Velociraptor> ().chaseRange = 100;
			timeToSpawn = Time.time + 0.2f;
			spawnCount++;
			if (spawnCount > 2) {
				spawnCount = 0;
				state = State.Idle;
			}
		}
		//Fire bullets
		if (!bulletAttack1 && !bulletAttack2 && Random.Range (0, 400) == 1) {
			bulletAttack1 = true;
		}

		if(bulletAttack1 && Time.time > timeToFire) {
			timeToFire = Time.time + 1/fireRate;
			CreateBullet();
			bullet1Count++;
			if (bullet1Count > 30) {
				bulletAttack1 = false;
				bullet1Count = 0;
			}
		}
	}

	void FireLaser() {
		laser.gameObject.SetActive (true);
		Invoke ("CancelLaser", 3f);
		sr.color = Color.white;
	}

	void CancelLaser() {
		laser.gameObject.SetActive (false);
		isLaser = false;
		state = State.Idle;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "GavinCollider") {
			dir *= -1;
		}
	}

	void CreateBullet () {
		//Create bullet with stray modifier
		float strayValue = Random.Range(-strayFactor, strayFactor);
		if (bulletAttack1) {
			GameMaster.CreateBullet (bulletPrefab, firePoint.position, knockbackForce, firePoint.rotation.eulerAngles.z + strayValue + 90, bulletDamage, shotSpeed/2, range, true, false);
		} else {
			GameMaster.CreateBullet (bulletPrefab, firePoint.position, knockbackForce, firePoint.rotation.eulerAngles.z + 90 + 5, bulletDamage, shotSpeed, range, true, false);
			GameMaster.CreateBullet (bulletPrefab, firePoint.position, knockbackForce, firePoint.rotation.eulerAngles.z + 90 - 5, bulletDamage, shotSpeed, range, true, false);
		}
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
