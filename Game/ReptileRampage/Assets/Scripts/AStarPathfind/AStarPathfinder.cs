﻿/*
	Created by: Lech Szymanski
				lechszym@cs.otago.ac.nz
				Dec 29, 2015			
*/

using UnityEngine;
using System.Collections.Generic;

/* This script can be used as a component of game object that needs to
 * find its path along the grid.  The script figures out the path to be
 * taken to destination and moves the game obejct (via change in transform
 * position) to move the object at desired speed towards the target.
 */
public class AStarPathfinder : MonoBehaviour {

	public GameObject gridObject = null; //Reference to the grid for finding the shortest path
										 //(initialise via the Inspector Panel)
	public float updateFrequency = 5.0f; //Path update frepqency (in path updates per second).  This
										 //can be used to reduce number of updates so as not to slow
										 //down the game too much.

	private AStarGrid grid = null; 	//Referendce to the grid (attached to the gridObject)

	private Vector2 previousTargetPosition; //Keeps track of change in target (for path updates)
	private List<Vector2> moves = null; //Moves to execute
	private int moveIndex = 0; //Index of the next move to execute

	private float timeLeftUntilPathUpdate;	//Internal variables for keeping track of time
	private float timeKeeper;			    //for 

	private Rigidbody2D rb;


#if UNITY_EDITOR
	/* Temp variables for drawing of the path and nodes examined - debugging only */
	public bool DrawPathInEditor = false; //Indicates whether to draw shortest path in the "Scene" view
									      //(set via the Inspector Panel)
	public Color PathColor = Color.yellow; //Color of the path 
										   //(set via the Inspector Panel)
	private float GizmoTime = 0.5f;
	private float GizmoTimeKeeper = 0.0f;
#endif

	void Start() {
		//Get the referende to the grid attached to the gridObject.  
		if (gridObject == null) {
			Debug.LogError ("Pathfinding grid object not initialised!");
			return;
		}
		grid = gridObject.GetComponent<AStarGrid> ();
		previousTargetPosition = transform.position;
		timeLeftUntilPathUpdate = 0;
		timeKeeper = Time.time;
		rb = GetComponent<Rigidbody2D> ();
	}

	/* Moves game object at certain speed by changing its transform postion to follow
	 * shortest path along the grid towards target game object
	 * 
	 * param: target - game object to travel towards
	 * param: atSpeed - speed at which to travel (in units/s)
	 */
	public void GoTowards(GameObject target, float speed, float maxSpeed) {
		if (target != null) {
			GoTowards (target.transform.position, speed, maxSpeed);
		}
	}

	/* Moves game object at certain speed by changing its transform postion to follow
	 * shortest path along the grid towards target position
	 * 
	 * param: targetPosition - position to travel towards
	 * param: atSpeed - speed at which to travel (in units/s)
	 */
	public void  GoTowards(Vector2 targetPosition, float speed, float maxSpeed) {

		if (grid == null) {
			Debug.LogError ("Pathfinding grid object is missing AStarGrid component!");
			return;
		}

		Vector2 currentPos = new Vector2();

		// Get the shortest path from object's position to the target position
		Vector2 fromPosition = transform.position;

		float timeDelta = Time.time-timeKeeper;
		//If its been a while since path finding then just reset the time between frames
		if (timeDelta > 0.1f) {
			timeDelta = 0.02f;
		}
		if (targetPosition != previousTargetPosition && timeLeftUntilPathUpdate <= 0.0f) {
			moves = grid.ShortestPath (fromPosition, targetPosition);
			moveIndex = 0;
			previousTargetPosition = targetPosition;
			if (updateFrequency > 0.0f) {
				timeLeftUntilPathUpdate = 1.0f / updateFrequency;
			} else {
				timeLeftUntilPathUpdate = updateFrequency;
			}
#if UNITY_EDITOR
			/* Draw nodes examined for path - debugging only */
			GizmoTimeKeeper = Time.time;
#endif
		} else {
			if (timeLeftUntilPathUpdate > 0.0f) {
				if(timeDelta > 0) {
					timeLeftUntilPathUpdate -= timeDelta;
				}
			}
		}
		timeKeeper = Time.time;

		if (moves == null) {
			return;
		}

#if UNITY_EDITOR
		if (DrawPathInEditor) {
			// Draw the path in the "Scene" view - debugging only
			bool firstMove = true;
			int index = 0;
			foreach (Vector2 nextPos in moves) {
				if(index++ < moveIndex) {
					continue;
				}
				if (!firstMove) {

					Vector3 start = new Vector3 (currentPos.x, currentPos.y, -2);
					Vector3 end = new Vector3 (nextPos.x, nextPos.y, -2);

					UnityEngine.Debug.DrawLine (start, end, PathColor, timeDelta, true);
				}
				currentPos = nextPos;
				firstMove = false;
			}
		}
#endif

		// Travel along the designated path
		if (moveIndex < moves.Count) {
			Vector3 nextPos = new Vector3 (moves [moveIndex].x, moves [moveIndex].y, transform.position.z);
			rb.AddForce(Vector3.Normalize (nextPos - transform.position) *speed);
			if (Vector3.Distance (nextPos, transform.position) < 0.3f) {
				moveIndex++;
			}
		}
	}

	public void ClearPath(){
		moves = null;
	}

	public void Reset(Vector2 start, Vector2 target){
		ClearPath();
		moves = grid.ShortestPath(start, target);
	}

#if UNITY_EDITOR
	/* Draw nodes examined for path - debugging only */
	void OnDrawGizmosSelected() {
		if (DrawPathInEditor && grid != null) {
			if (GizmoTimeKeeper + GizmoTime > Time.time) {
				grid.DrawNodesConsidered (PathColor);
			}
		}
	}
#endif
}
