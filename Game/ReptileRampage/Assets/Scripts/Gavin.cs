using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gavin : MonoBehaviour {

	private enum State {Idle, Laser, Shooting, Spawning};

	private State state;

	private SpriteRenderer sr;
	private Transform laser;
	private Transform firePoint;

	public Transform target;
	public Transform bulletPrefab;

	public float speed;
	public int laserDamage;

	[HideInInspector]
	public int dir = 1;
	private bool isLaser = false;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		laser = transform.FindChild ("Laser");
		firePoint = transform.FindChild ("FirePoint");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0, speed * 0.02f * dir));
		if (state == State.Idle && Random.Range (0, 200) == 1) {
			state = State.Laser;
		}
		if (state == State.Laser && !isLaser) {
			isLaser = true;
			sr.color = Color.blue;
			Invoke ("FireLaser", 1f);
		}
	}

	void FireLaser() {
		laser.gameObject.SetActive (true);
		Invoke ("CancelLaser", 3f);
		sr.color = Color.white;
	}

	void CancelLaser() {
		laser.gameObject.SetActive (false);
		isLaser = false;
		state = State.Idle;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "GavinCollider") {
			dir *= -1;
		}
	}
}
