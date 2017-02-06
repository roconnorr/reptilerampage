using UnityEngine;

public class Pteradactyl : MonoBehaviour {

	public float circleDistance;
	public float speed;
	public float maxSpeed;
	public float circleSpeed;
	public float sightRange;
	public int damage;
	public float fireRate;
	public Transform target;
	public Transform grenadePrefab;

	private Vector3 targetLocation;
	private Vector3 spawnLocation;
	private float angle = 0;
	private bool seen = false;
	private float timeToFire = 0;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		angle = Random.Range (0, 360);
		spawnLocation = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!seen) {
			targetLocation = spawnLocation + (Quaternion.Euler (0, 0, angle) * Vector3.up * circleDistance);
			rb.AddForce(Vector3.Normalize (targetLocation - transform.position) * speed);
			angle += circleSpeed;
			seen = Vector3.Distance (gameObject.transform.position, target.transform.position) < sightRange;
		}
		if (seen) {
			targetLocation = target.transform.position + (Quaternion.Euler (0, 0, angle) * Vector3.up * circleDistance);
			rb.AddForce(Vector3.Normalize (targetLocation - transform.position) * speed);
			angle += circleSpeed;
			if (Random.Range (0, 500) == 1) {
				angle = (angle + 180) % 360;
			}
			if (Vector3.Distance (transform.position, target.transform.position) < 3 && Time.time > timeToFire) {
				float angle = Mathf.Atan2(target.transform.position.y-transform.position.y, target.transform.position.x-transform.position.x)*180 / Mathf.PI;
				angle -= 90;
				GameMaster.CreateGrenade (grenadePrefab, transform.position, angle, damage, 5, 70, true, true);
				timeToFire = Time.time + 1/(fireRate);
			}
		}
		//Max Speed
		if (rb.velocity.magnitude > maxSpeed) {
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}

	void Update() {
		if (GetComponent<Enemy> ().hasSeen) {
			seen = true;
		}
		if (!seen) {
			GetComponent<Enemy> ().hasSeen = false;
		}
	}
}
