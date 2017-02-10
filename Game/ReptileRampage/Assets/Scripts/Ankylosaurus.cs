using UnityEngine;

public class Ankylosaurus : MonoBehaviour {

	public int damage;
	public float fireRate;
	public float shotSpeed;
	public float range;
	public float knockbackForce;
	public Transform bulletPrefab;
	public AudioClip shotSound = null;

	public float speed;
	public float maxSpeed;
	public float sightRange;
	public float chaseRange;
	public float stopRange;
	public float patrolRange;
	public float disabledDistance;
	public GameObject target;

	//Boolean variables
	private bool targetInChaseRange = false;
	private bool targetInSightRange = false;
	private bool targetInStopRange = false;
	private bool targetViewBlocked = true;
	private bool targetObstructed = true;
	[HideInInspector]
	public bool isChasing = false;
	private bool isWandering = false;
	private bool avoiding = false;
	private bool flipped = false;
	private bool isCharging = false;
	private bool disabled = true;

	//private Animator animator;
	private float xPrev = 0;
	private Vector3 patrolLocation;
	private Vector3 wanderLocation;
	private Vector3 chargePosition;

	private int smashes = 0;
	private float timeToFire = 0;
	private Transform firePoint;
	private Animator animator;
	private Rigidbody2D rb;
	private float modifierOriginal;

	//Pathfinder variables
	private AStarPathfinder pathfinder = null;

	void Start() {
		pathfinder = transform.GetComponent<AStarPathfinder> ();
		//animator = GetComponent<Animator>();
		patrolLocation = transform.position;
		firePoint = transform.Find ("FirePoint");
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D> ();
		modifierOriginal = gameObject.GetComponent<Enemy>().knockbackModifier;
	}

	void FixedUpdate() {
		if(target == null){
			Destroy(gameObject);
			return;
		}
		if (!disabled) {
			//Get vision booleans
			targetViewBlocked = PositionHiddenByObstacles (target.transform.position);
			targetObstructed = PositionObstructedByObstacles (target.transform.position);
			targetInChaseRange = Vector3.Distance (gameObject.transform.position, target.transform.position) < chaseRange;
			targetInSightRange = Vector3.Distance (gameObject.transform.position, target.transform.position) < sightRange;
			targetInStopRange = Vector3.Distance (gameObject.transform.position, target.transform.position) < stopRange;

			//If seeing player when not chasing
			if (!isChasing && targetInSightRange && !targetViewBlocked) {
				isChasing = true;
				isWandering = false;
				//animator.Play ("velociraptor_run");
			}

			//If chasing player
			if (isChasing && targetInChaseRange) {
				if (!targetInStopRange) {
					if (!targetObstructed) {
						//Find nearest enemy and avoid if they're too close
						Transform nearestEnemy = GetNearestSameDino ();
						if (nearestEnemy != null) {
							float dist = Vector3.Distance (nearestEnemy.transform.position, transform.position);
							if (avoiding) {
								if (dist > 2) {
									avoiding = false;
								}
								Avoid (nearestEnemy);
							} else if (dist < 1.5) {
								avoiding = true;
								Avoid (nearestEnemy);
							}
						}
						//Move directly towards player
						MoveDirect ();
						//If in chase range but player is obstructed, pathfind to him
					} else {
						if (isChasing) {
							MovePathFind ();
						}
					}
				} else {//If in stop range
					if (!isCharging && Random.Range (0, 400) == 1) {
						isCharging = true;
						chargePosition = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
					}
					if (!isCharging && Time.time > timeToFire) {
						smashes = Random.Range (2, 5);
						animator.Play ("Ankylo_Smash");
						timeToFire = Time.time + 1 / fireRate;
					}
					if (isCharging) {
						gameObject.GetComponent<Enemy>().knockbackModifier = 0;
						if (Vector3.Distance (transform.position, chargePosition) > 0.5f) {
							rb.AddForce (Vector3.Normalize (chargePosition - transform.position) * speed * 10);
						} else {
							isCharging = false;
							gameObject.GetComponent<Enemy>().knockbackModifier = modifierOriginal;
						}
					}
				}
			} else {
				if (isChasing) {
					isChasing = false;
					patrolLocation = transform.position;
				}
				MovePatrol ();
			}
			if (rb.velocity.magnitude > maxSpeed) {
				rb.velocity = rb.velocity.normalized * maxSpeed;
			}
		}
	}

	void Update() {
		if (GetComponent<Enemy> ().hasSeen) {
			isChasing = true;
		}
		if (!isChasing) {
			GetComponent<Enemy> ().hasSeen = false;
		}
		if(target == null){
			Destroy(gameObject);
			return;
		}
		disabled = Vector3.Distance (transform.position, target.transform.position) > disabledDistance;
		if (!disabled) {
			//Direction
			//Has different check for isWandering because they go so slow that it doesn't trigger the 0.05
			if (isWandering) {
				if ((transform.position.x > xPrev) && !flipped) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flipped = true;
				}
				if ((transform.position.x < xPrev) && flipped) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flipped = false;
				}
				//Has a buffer of 0.05 so that they don't freak out when travelling directly up or when they're inside the player
			} else {
				if ((transform.position.x > xPrev + 0.02) && !flipped) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flipped = true;
				}
				if ((transform.position.x < xPrev - 0.02) && flipped) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flipped = false;
				}
			}
			xPrev = transform.position.x;
		}
	}


	bool PositionHiddenByObstacles(Vector3 position)  {
		float distanceToPlayer = Vector2.Distance(transform.position, position);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, position - transform.position, distanceToPlayer);
		Debug.DrawRay(transform.position, position - this.transform.position, Color.blue); // draw line in the Scene window to show where the raycast is looking

		foreach (RaycastHit2D hit in hits) {
			// if anything other than the player is hit then it must be between the player and the enemy's eyes (since the enemy can only see as far as the player)
			if (hit.transform.tag == "Wall" && hit.transform.gameObject.layer == 0) {
				return true;
			}
		}
		// if no objects were closer to the enemy than the player return false (player is not hidden by an object)
		return false; 
	}

	bool PositionObstructedByObstacles(Vector3 position)  {
		float distanceToPlayer = Vector2.Distance(transform.position, position);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, position - transform.position, distanceToPlayer);
		Debug.DrawRay(transform.position, position - this.transform.position, Color.blue); // draw line in the Scene window to show where the raycast is looking

		foreach (RaycastHit2D hit in hits) {
			// if anything other than the player is hit then it must be between the player and the enemy's eyes (since the enemy can only see as far as the player)
			if (hit.transform.tag == "Wall") {
				if (targetObstructed == false && isChasing){
					pathfinder.Reset(transform.position, position);
				}
				return true;
			}
		}
		// if no objects were closer to the enemy than the player return false (player is not hidden by an object)
		return false; 
	}

	void MoveDirect() {
		if (Vector3.Distance (transform.position, target.transform.position) > 0.2) {
			rb.AddForce(Vector3.Normalize (target.transform.position - transform.position) * speed);
		}
	}

	void MovePathFind() {
		pathfinder.GoTowards (target, speed, maxSpeed);
	}

	void MovePatrol() {
		if (Random.Range (0, 200) == 1) {
			if (!isWandering) {
				bool foundLocation = false;
				int tries = 0;
				while (foundLocation == false && tries < 5) {
					wanderLocation = new Vector3 (patrolLocation.x + Random.Range (-patrolRange, patrolRange), patrolLocation.y + Random.Range (-patrolRange, patrolRange), transform.position.z);
					if (PositionObstructedByObstacles (wanderLocation)) {
						tries++;
					} else {
						foundLocation = true;
					}
				}
				if (tries < 5) {
					isWandering = true;
					//animator.Play ("velociraptor_run");
				}
			} else {
				isWandering = false;
				//animator.Play ("velociraptor_idle");
			}
		}
		if (isWandering) {
			rb.AddForce(Vector3.Normalize (wanderLocation - transform.position) * speed);
			if (Vector3.Distance (transform.position, wanderLocation) < 0.1) {
				isWandering = false;
			}
			if(rb.velocity.magnitude > maxSpeed/4) {
				rb.velocity = rb.velocity.normalized * maxSpeed/4;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Wall") {
			rb.velocity = Vector3.zero;
		}
	}

	void Avoid(Transform obj) {
		rb.AddForce(Vector3.Normalize (transform.position - obj.transform.position) * speed/4);
	}

	Transform GetNearestSameDino() {
		float nearestDistance = 9999;
		Transform nearestEnemy = null;
		var objects = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (var obj in objects) {
			float dist = Vector3.Distance (obj.transform.position, transform.position);
			if (dist < nearestDistance && dist > 0 && obj.GetComponent<Ankylosaurus>()) {
				nearestEnemy = obj.GetComponent<Transform> ();
				nearestDistance = dist;
			}
		}
		return nearestEnemy;
	}

	void FireBullets() {
		for(int i=0; i<=360; i+=20){
			Transform bullet = GameMaster.CreateBullet (bulletPrefab, firePoint.position, knockbackForce, i, damage, shotSpeed, range, true, false, transform);
			Physics2D.IgnoreCollision (bullet.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
		}
		if(shotSound != null){
			AudioSource.PlayClipAtPoint(shotSound, transform.position);
		}
		gameObject.GetComponent<CameraShake>().StartShaking(1f);
		smashes--;
		if (smashes > 0) {
			animator.Play ("Ankylo_Smash");
		}
	}
}
