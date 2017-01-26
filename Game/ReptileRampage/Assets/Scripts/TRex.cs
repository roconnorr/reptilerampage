using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex : MonoBehaviour {
	
	private enum State {Idle, Resting, Walking, Roaring, Shooting};

	private State state;

	public Transform target;
	public Transform bulletPrefab;
	public Transform rocketPrefab;

	public float speed;
	public int health;

	Vector3 targetLocation;

	// Use this for initialization
	void Start () {
		state = State.Idle;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (state);
		//Idle Behaviour
		if (state == State.Idle) {
			if (Random.Range (0, 100) == 1) {
				state = State.Walking;
				targetLocation = new Vector3(target.position.x, target.position.y, -1);
			}
		}
		//Walking Behaviour
		else if (state == State.Walking) {
			transform.position = Vector3.MoveTowards (this.transform.position, targetLocation, speed/40);
			if (Vector3.Distance (targetLocation, transform.position) < 0.01) {
				state = State.Idle;
			}
		}
	}
}
