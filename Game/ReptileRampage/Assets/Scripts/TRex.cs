using UnityEngine;

public class TRex : MonoBehaviour {
	
	private enum State {Idle, Walking, Roaring, Shooting};

	private State state;

	public Transform target;
	public Transform bulletPrefab;
	public Transform rocketPrefab;
	private Transform firePoint;
	private Transform rocketFirePoint;
	public float bulletKnockbackForce;
	public float speed;
	private int timeSinceLastAction = 0;
	[HideInInspector]
	public bool defencesDown;
	[HideInInspector]
	public int defenceTimer = 0;
	private int blocked = 0;
	private int actions = 0;
	private bool flipped;
	private float xPrev;
	private Vector3 targetLocation;
	private SpriteRenderer sr;
	private Animator animator;
	private int walkTimer = 0;
	private bool spawned;
	private int spawnIdle;
	private AudioSource roarSource;
	public AudioClip roarSound;

	// Use this for initialization
	void Start () {
		state = State.Idle;
		defencesDown = false;
		sr = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator>();
		firePoint = transform.Find ("FirePoint");
		rocketFirePoint = transform.Find ("RocketFirePoint");
		GetComponent<Enemy>().noFlip = true;
		animator.Play("TrexSpawn");
		animator.speed = 0.5f;
		GameObject.Find("Player").GetComponent<PlayDialog>().PlayTrexDialog();
		Invoke("setSpawnAnimationToFinished", 4f);
		roarSource = gameObject.GetComponent<AudioSource>();
	}

	void setSpawnAnimationToFinished(){
		spawnIdle = 150;
		WayPoints.trexSpawnAnimationFinished = true;
		transform.Find ("Shadow").GetComponent<SpriteRenderer>().enabled = true;
		spawned = true;
		animator.speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnIdle > 0) {
			spawnIdle--;
		} else {
			if (walkTimer > 0) {
				walkTimer--;
			}
			//Idle Behaviour
			if (state == State.Idle) {
				if (timeSinceLastAction < 50) {
					timeSinceLastAction++;
				} else {
					int rand = Random.Range (0, 100);
					if (actions > 4) {
						state = State.Shooting;
						timeSinceLastAction = 0;
						actions = 0;
					} else if (rand < 2) {
						state = State.Walking;
						timeSinceLastAction = 0;
						walkTimer = 5;
						targetLocation = new Vector3 (target.position.x, target.position.y, -1);
						actions++;
					} else if (rand < 4) {
						state = State.Roaring;
						timeSinceLastAction = 0;
						actions++;
					}
				}
			}
		//Walking Behaviour
		else if (state == State.Walking && spawned && Time.timeScale != 0) {
				animator.Play ("TrexWalk");
				transform.position = Vector3.MoveTowards (transform.position, targetLocation, speed / 40);
				if (Vector3.Distance (targetLocation, transform.position) < 0.01) {
					state = State.Idle;
					animator.Play ("TrexIdle");
				}
				if ((transform.position.x > xPrev + 0.02) && !flipped) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flipped = true;
					GetComponent<Enemy> ().noFlip = false;
				}
				if ((transform.position.x < xPrev - 0.02) && flipped) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flipped = false;
					GetComponent<Enemy> ().noFlip = true;
				}
				xPrev = transform.position.x;
			}
		//Fire Rockets
		else if (state == State.Shooting && spawned) {
				//ShootRocket();
				animator.Play ("TrexShoot");
				state = State.Idle;
			}
		//Roar
		else if (state == State.Roaring && spawned) {
				animator.Play ("TrexRoar");
				if(!roarSource.isPlaying){
					roarSource.PlayOneShot(roarSound, 0.2f);
				}
				//PlayRoar(roarSound, this.transform.position);
				//AudioSource.PlayClipAtPoint(roarSound, this.transform.position);
				state = State.Idle;
			}
			//Sprites
			if (blocked > 0) {
				sr.color = new Color (0.8f, 0.8f, 0.8f);
				blocked--;
			} else if (defenceTimer > 0) {
				defenceTimer--;
				sr.color = Color.red;
			} else {
				defencesDown = false;
				sr.color = Color.white;
			}
		}
	}

	void ShootRocket(){
		GameMaster.CreateHomingBullet (rocketPrefab, rocketFirePoint.position, Random.Range (315, 405), 100, 12, 300, true, true, target, transform);
		GameMaster.CreateHomingBullet (rocketPrefab, rocketFirePoint.position, Random.Range (315, 405), 100, 12, 300, true, true, target, transform);
	}

	void CallShootWave(){
		if ((target.position.x > transform.position.x) && !flipped) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			flipped = true;
			GetComponent<Enemy>().noFlip = false;
		}
		if ((target.position.x < transform.position.x) && flipped) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			flipped = false;
			GetComponent<Enemy>().noFlip = true;
		}
		targetLocation = new Vector3 (target.position.x, target.position.y, -1);
		ShootWave ();
		Invoke("ShootWave", .1f);
		Invoke("ShootWave", .2f);
	}

	void ShootWave() {
		float angle = Mathf.Atan2(targetLocation.y-transform.position.y, targetLocation.x-transform.position.x)*180 / Mathf.PI;
		angle -= 90;
		angle -= 14;
		for (int i = 0; i < 5; i++) {
			GameMaster.CreateBullet (bulletPrefab, firePoint.position, bulletKnockbackForce, angle, 10, 10, 80, true, false, transform);
			angle += 7;
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if(other.gameObject.GetComponent<Bullet>() && !defencesDown) {
			blocked = 3;
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "BossCollider" && state == State.Walking && walkTimer == 0) {
			state = State.Idle;
		}
	}
}
