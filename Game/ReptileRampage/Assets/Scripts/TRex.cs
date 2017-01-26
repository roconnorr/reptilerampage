using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRex : MonoBehaviour {
	
	private enum State {Idle, Walking, Roaring, Shooting};

	private State state;

	public Transform target;
	public Transform bulletPrefab;
	public Transform rocketPrefab;

	public float speed;
	public int health;

	private int timeSinceLastAction = 0;

	Vector3 targetLocation;

	// Use this for initialization
	void Start () {
		state = State.Idle;
	}
	
	// Update is called once per frame
	void Update () {
		//Idle Behaviour
		if (state == State.Idle) {
			if (timeSinceLastAction < 100) {
				timeSinceLastAction++;
			} else {
				int rand = Random.Range (0, 500);
				if (rand < 5) {
					state = State.Walking;
					timeSinceLastAction = 0;
					targetLocation = new Vector3 (target.position.x, target.position.y, -1);
				} else if (rand < 7) {
					state = State.Roaring;
					timeSinceLastAction = 0;
				} else if (rand < 9) {
					state = State.Shooting;
					timeSinceLastAction = 0;
				}
			}
		}
		//Walking Behaviour
		else if (state == State.Walking) {
			transform.position = Vector3.MoveTowards (this.transform.position, targetLocation, speed / 40);
			if (Vector3.Distance (targetLocation, transform.position) < 0.01) {
				state = State.Idle;
			}
		}
		//Fire Rockets
		else if (state == State.Shooting) {
			GameMaster.CreateHomingBullet (rocketPrefab, transform.position, Random.Range (45, 135), 10, 10, true, true, target);
			GameMaster.CreateHomingBullet (rocketPrefab, transform.position, Random.Range (45, 135), 10, 10, true, true, target);
			state = State.Idle;
		}
		//Roar
		else if (state == State.Roaring) {
			targetLocation = new Vector3 (target.position.x, target.position.y, -1);
			ShootWave ();
			Invoke("ShootWave", .1f);
			Invoke("ShootWave", .2f);
			state = State.Idle;
		}
	}

	void ShootWave() {
		float angle = Mathf.Atan2(targetLocation.y-transform.position.y, targetLocation.x-transform.position.x)*180 / Mathf.PI;
		angle -= 90;
		angle -= 10;
		for (int i = 0; i < 5; i++) {
			GameMaster.CreateBullet (bulletPrefab, transform.position, angle, 10, 10, true, false);
			angle += 5;
		}
	}
}
