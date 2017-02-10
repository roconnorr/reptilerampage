using UnityEngine;

public class Velociraptor : MonoBehaviour {

	//Public variables
	public float speed;
	public float maxSpeed;
	public float sightRange;
	public float chaseRange;
	public float patrolRange;
	public float disabledDistance;
	public GameObject target;

	//Boolean variables
	private bool targetInChaseRange = false;
	private bool targetInSightRange = false;
	private bool targetViewBlocked = true;
	private bool targetObstructed = true;
	private bool isChasing = false;
	private bool isWandering = false;
	private bool avoiding = false;
	private bool flipped = false;
	private bool disabled = true;

	private float xPrev = 0;
	private Vector3 patrolLocation;
	private Vector3 wanderLocation;

	//Component variables
	private AStarPathfinder pathfinder = null;
	private Animator animator;
	private Rigidbody2D rb;

	//Run on game start
	void Start() {
		pathfinder = transform.GetComponent<AStarPathfinder> ();
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D> ();
		patrolLocation = transform.position;
		GetComponent<Enemy>().noFlip = false;
	}

	//Run every tick
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

			//If seeing player when not chasing
			if (!isChasing && targetInSightRange && !targetViewBlocked) {
				isChasing = true;
				animator.Play ("velociraptor_run");
			}

			//If chasing player
			if (isChasing && targetInChaseRange) {
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
			} else {
				if (isChasing) {
					isChasing = false;
					patrolLocation = transform.position;
				}
				MovePatrol ();
			}
			//Max Speed
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
					GetComponent<Enemy>().noFlip = true;
				}
				if ((transform.position.x < xPrev) && flipped) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flipped = false;
					GetComponent<Enemy>().noFlip = false;
				}
				//Has a buffer of 0.05 so that they don't freak out when travelling directly up or when they're inside the player
			} else {
				if ((transform.position.x > xPrev + 0.05) && !flipped) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flipped = true;
					GetComponent<Enemy>().noFlip = true;
				}
				if ((transform.position.x < xPrev - 0.05) && flipped) {
					transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					flipped = false;
					GetComponent<Enemy>().noFlip = false;
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
					animator.Play ("velociraptor_run");
				}
			} else {
				isWandering = false;
				animator.Play ("velociraptor_idle");
			}
		}
		if (isWandering) {
			rb.AddForce(Vector3.Normalize (wanderLocation - transform.position) * speed);
			if (Vector3.Distance (transform.position, wanderLocation) < 0.1f) {
				isWandering = false;
				animator.Play ("velociraptor_idle");
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
			if (dist < nearestDistance && dist > 0 && obj.GetComponent<Velociraptor>()) {
				nearestEnemy = obj.GetComponent<Transform> ();
				nearestDistance = dist;
			}
		}
		return nearestEnemy;
	}
}
