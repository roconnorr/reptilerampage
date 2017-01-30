using UnityEngine;

public class Velociraptor : MonoBehaviour {

	//Public variables
	public float speed;
	public float sightRange;
	public float followRange;
	public GameObject target;

	//Boolean variables
	private bool targetInChaseRange = false;
	private bool targetInSightRange = false;
	private bool targetViewBlocked;
	private bool isChasing = false;
	private bool avoiding = false;
	private bool flipped = false;

	private float xPrev = 0;

	//Component variables
	private AStarPathfinder pathfinder = null;
	private Animator animator;

	//Run on game start
	void Start() {
		pathfinder = transform.GetComponent<AStarPathfinder> ();
		animator = GetComponent<Animator>();
	}

	//Run every tick
	void Update() {
		//Get vision booleans
		targetViewBlocked = TargetHiddenByObstacles ();
		targetInChaseRange = Vector3.Distance(gameObject.transform.position, target.transform.position) < followRange;
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
				Transform nearestEnemy = GetNearestEnemy ();
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
			isChasing = false;
			MovePatrol ();
		}

		if((transform.position.x > xPrev + 0.05) && !flipped){
			transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
			flipped = true;
		}
		if((transform.position.x < xPrev - 0.05) && flipped){
			transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
			flipped = false;
		}
		xPrev = transform.position.x;
	}


	bool TargetHiddenByObstacles()  {
		float distanceToPlayer = Vector2.Distance(this.transform.position, target.transform.position);
		RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, target.transform.position - transform.position, distanceToPlayer);
		Debug.DrawRay(this.transform.position, target.transform.position - this.transform.position, Color.blue); // draw line in the Scene window to show where the raycast is looking

		foreach (RaycastHit2D hit in hits) {           
			// ignore the enemy's own colliders (and other enemies)
			if (hit.transform.tag == "Enemy") {
				continue;
			}

			// if anything other than the player is hit then it must be between the player and the enemy's eyes (since the player can only see as far as the player)
			if (hit.transform.tag != "Player") {
				if (targetViewBlocked == false){
					pathfinder.Reset(this.transform.position, target.transform.position);
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
		//patrol
	}

	void Avoid(Transform obj) {
		transform.position = Vector3.MoveTowards (transform.position, obj.transform.position, -speed/80);
	}

	Transform GetNearestEnemy() {
		float nearestDistance = 9999;
		Transform nearestEnemy = null;
		var objects = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (var obj in objects) {
			float dist = Vector3.Distance (obj.transform.position, transform.position);
			if (dist < nearestDistance && dist > 0) {
				nearestEnemy = obj.GetComponent<Transform> ();
				nearestDistance = dist;
			}
		}
		return nearestEnemy;
	}
}
