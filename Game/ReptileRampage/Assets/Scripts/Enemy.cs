using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float speed;

	public int sightRange;

	public GameObject target;
	private Transform destination;

	//Boolean variables
	private bool targetInRange;
	private bool targetViewBlocked;
	private bool followingPath;
	private bool hasSeenTarget = false;

	//Pathfinder variables
	private AStarPathfinder pathfinder = null;
//    public Transform[] navpoints;
//    private int destPoint = 0;

	void Start() {
		targetInRange = false;
		pathfinder = transform.GetComponent<AStarPathfinder> ();
	}

	void FixedUpdate() {
		//Check obstruction
		targetViewBlocked = TargetHiddenByObstacles ();
		targetInRange = Vector3.Distance(gameObject.transform.position, target.transform.position) < sightRange;
		//If in view move direct
		if (targetInRange && !targetViewBlocked) {
			//Try change to steering to stop clumping
			transform.position = Vector3.MoveTowards (this.transform.position, target.transform.position, speed/40);
			hasSeenTarget = true;
		//If not in view...
		} else {
			//Pathfind if target has been seen previously
			if (hasSeenTarget) {
				pathfinder.GoTowards (target, speed);
			//Patrol if target has not been seen previously
			} else {
				//patrol
			}
		}
	}


	bool TargetHiddenByObstacles()  {
		float distanceToPlayer = Vector2.Distance(this.transform.position, target.transform.position);
		RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, target.transform.position - transform.position, distanceToPlayer);
		Debug.DrawRay(this.transform.position, target.transform.position - this.transform.position, Color.blue); // draw line in the Scene window to show where the raycast is looking
        List<float> distances = new List<float>();
     
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
}
