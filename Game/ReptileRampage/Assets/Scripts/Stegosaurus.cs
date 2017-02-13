using UnityEngine;

public class Stegosaurus : MonoBehaviour {

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
	private bool targetObstructed = true;
	private bool targetViewBlocked = true;
	[HideInInspector]
	public bool isChasing = false;
	private bool isWandering = false;
	private bool avoiding = false;
	private bool flipped = false;
	private bool disabled = true;
	private bool stopped;

	//private Animator animator;
	private float xPrev = 0;
	private Vector3 patrolLocation;
	private Vector3 wanderLocation;

	//Pathfinder variables
	private AStarPathfinder pathfinder = null;
	private Rigidbody2D rb;
	private Animator animator;
	private SpriteRenderer sr;

	void Start() {
		pathfinder = transform.GetComponent<AStarPathfinder> ();
		animator = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D> ();
		patrolLocation = transform.position;
		GetComponent<Enemy>().noFlip = true;
	}

	void FixedUpdate() {
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
				animator.Play ("StegoWalk");
			}

			//If chasing player
			if (targetInChaseRange) {
				if (isChasing) {
					if (!targetInStopRange) {
						if (stopped) {
							stopped = false;
							animator.Play ("StegoWalk");
						}
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
					} else if (!stopped) {
						animator.Play ("StegoIdle");
						stopped = true;
					}
				}
			} else {
				if (isChasing) {
					isChasing = false;
					patrolLocation = transform.position;
					animator.Play ("StegoIdle");
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
		if (GetComponent<Enemy> ().hasSeen && !isChasing) {
			isChasing = true;
			animator.Play ("StegoWalk");
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
			if ((transform.position.x > xPrev + 0.02) && !flipped) {
				sr.flipX = true;
				flipped = true;
				GetComponent<Enemy>().noFlip = false;
			}
			if ((transform.position.x < xPrev - 0.02) && flipped) {
				sr.flipX = false;
				flipped = false;
				GetComponent<Enemy>().noFlip = true;
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
					animator.Play ("StegoWalk");
				}
			} else {
				isWandering = false;
				animator.Play ("StegoIdle");
			}
		}
		if (isWandering) {
			rb.AddForce(Vector3.Normalize (wanderLocation - transform.position) * speed);
			if (Vector3.Distance (transform.position, wanderLocation) < 0.1) {
				isWandering = false;
				animator.Play ("StegoIdle");
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
			if (dist < nearestDistance && dist > 0 && obj.GetComponent<Stegosaurus>()) {
				nearestEnemy = obj.GetComponent<Transform> ();
				nearestDistance = dist;
			}
		}
		return nearestEnemy;
	}
}
