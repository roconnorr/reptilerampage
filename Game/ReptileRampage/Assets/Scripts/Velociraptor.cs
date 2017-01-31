using UnityEngine;

public class Velociraptor : MonoBehaviour {

	//Public variables
	public float speed;
	public float sightRange;
	public float chaseRange;
	public float patrolRange;
	public GameObject target;

	//Boolean variables
	private bool targetInChaseRange = false;
	private bool targetInSightRange = false;
	private bool targetViewBlocked;
	private bool isChasing = false;
	private bool isWandering = false;
	private bool avoiding = false;
	private bool flipped = false;

	private float xPrev = 0;
	private Vector3 patrolLocation;
	private Vector3 wanderLocation;

	//Component variables
	private AStarPathfinder pathfinder = null;
	private Animator animator;

	//Run on game start
	void Start() {
		pathfinder = transform.GetComponent<AStarPathfinder> ();
		animator = GetComponent<Animator>();
		patrolLocation = transform.position;
	}

	//Run every tick
	void Update() {
		//Get vision booleans
		targetViewBlocked = PositionHiddenByObstacles (target.transform.position);
		targetInChaseRange = Vector3.Distance(gameObject.transform.position, target.transform.position) < chaseRange;
		targetInSightRange = Vector3.Distance (gameObject.transform.position, target.transform.position) < sightRange;

		//If seeing player when not chasing
		if (!isChasing && targetInSightRange && !targetViewBlocked) {
			isChasing = true;
			animator.Play ("velociraptor_run");
		}

		//If chasing player
		if (isChasing && targetInChaseRange) {
			if (!targetViewBlocked) {
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
			if ((transform.position.x > xPrev + 0.05) && !flipped) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				flipped = true;
			}
			if ((transform.position.x < xPrev - 0.05) && flipped) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				flipped = false;
			}
		}
		xPrev = transform.position.x;
	}


	bool PositionHiddenByObstacles(Vector3 position)  {
		float distanceToPlayer = Vector2.Distance(transform.position, position);
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, position - transform.position, distanceToPlayer);
		Debug.DrawRay(transform.position, position - this.transform.position, Color.blue); // draw line in the Scene window to show where the raycast is looking

		foreach (RaycastHit2D hit in hits) {
			// if anything other than the player is hit then it must be between the player and the enemy's eyes (since the enemy can only see as far as the player)
			if (hit.transform.tag == "Wall") {
				if (targetViewBlocked == false){
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
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed / 40);
		}
	}

	void MovePathFind() {
		pathfinder.GoTowards (target, speed);
	}

	void MovePatrol() {
		if (Random.Range (0, 200) == 1) {
			if (!isWandering) {
				bool foundLocation = false;
				int tries = 0;
				while (foundLocation == false && tries < 5) {
					wanderLocation = new Vector3 (patrolLocation.x + Random.Range (-patrolRange, patrolRange), patrolLocation.y + Random.Range (-patrolRange, patrolRange));
					if (PositionHiddenByObstacles (wanderLocation)) {
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
			transform.position = Vector3.MoveTowards (transform.position, wanderLocation, speed / 80);
			if (Vector3.Distance (transform.position, wanderLocation) < 0.01) {
				isWandering = false;
				animator.Play ("velociraptor_idle");
			}
		}
	}

	void Avoid(Transform obj) {
		transform.position = Vector3.MoveTowards (transform.position, obj.transform.position, -speed /80);
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
