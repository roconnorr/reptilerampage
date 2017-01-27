using UnityEngine;
using System.Collections.Generic;

public class Velociraptor : MonoBehaviour {

	public float speed;

	public int sightRange;

	public GameObject target;
	private Transform destination;

	//Boolean variables
	private bool targetInRange;
	private bool targetViewBlocked;
	private bool followingPath;
	private bool hasSeenTarget = false;
	private bool avoiding = false;

	//Pathfinder variables
	private AStarPathfinder pathfinder = null;

	void Start() {
		targetInRange = false;
		pathfinder = transform.GetComponent<AStarPathfinder> ();
	}

	void FixedUpdate() {
		//Check obstruction
		targetViewBlocked = TargetHiddenByObstacles ();
		targetInRange = Vector3.Distance(gameObject.transform.position, target.transform.position) < sightRange;
		if (targetInRange && !targetViewBlocked) {
			Transform nearestEnemy = GetNearestEnemy();
			float dist = Vector3.Distance (nearestEnemy.transform.position, transform.position);
			if (avoiding) {
				if (dist > 2) {
					avoiding = false;
				}
				Avoid (nearestEnemy);
			} else if (nearestEnemy != null && dist < 1.5) {
				avoiding = true;
				Avoid (nearestEnemy);
			}
			MoveDirect ();
			hasSeenTarget = true;
		} else {
			if (hasSeenTarget) {
				MovePathFind ();
			} else {
				MovePatrol ();
			}
		}
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
		transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed/40);
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
