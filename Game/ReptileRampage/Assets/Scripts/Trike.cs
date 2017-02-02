using UnityEngine;

public class Trike : MonoBehaviour {
	
	private enum State {Idle, Walking, VAttack, GrenadeAttack, StompAttack};

	private State state;

	public Transform target;
	public Transform bulletPrefab;
	public Transform grenadePrefab;

	private Transform vFirePoint;
	private Transform grenadeFirePoint;
	private Transform stompFirePoint;

	public float speed;

	private int timeSinceLastAction = 0;


	private bool flipped;
	private float xPrev;

	private Vector3 targetLocation;
	


	// Use this for initialization
	void Start () {
		state = State.Idle;
		vFirePoint = transform.FindChild ("VFirePoint");
		grenadeFirePoint = transform.FindChild ("GrenadeFirePoint");
		stompFirePoint = transform.FindChild ("StompFirePoint");
	}
	
	// Update is called once per frame
	void Update () {
		//Idle Behaviour
		if (state == State.Idle) {
			if (timeSinceLastAction < 100) {
				timeSinceLastAction++;
			} else {
				int rand = Random.Range (0, 200);
				if (rand < 2) {
					state = State.Walking;
					timeSinceLastAction = 0;
					targetLocation = new Vector3 (target.position.x, target.position.y, -1);
				} else if (rand < 4) {
					state = State.StompAttack;
					timeSinceLastAction = 0;
				} else if (rand < 5) {
					state = State.GrenadeAttack;
					timeSinceLastAction = 0;
				} else if (rand < 6) {
					state = State.VAttack;
					timeSinceLastAction = 0;
				}
			}
		}
		//Walking Behaviour
		else if (state == State.Walking) {
			transform.position = Vector3.MoveTowards (transform.position, targetLocation, speed / 40);
			if (Vector3.Distance (targetLocation, transform.position) < 0.01) {
				state = State.Idle;
			}
			if ((transform.position.x > xPrev + 0.02) && !flipped) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				flipped = true;
			}
			if ((transform.position.x < xPrev - 0.02) && flipped) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				flipped = false;
			}
			xPrev = transform.position.x;
		}
		//Fire Grenades
		else if (state == State.GrenadeAttack) {
			GrenadeWave();
			for(float i = .1f; i <= .9f; i+=.2f){
				Invoke("GrenadeWave", i);
			}
			state = State.Idle;
		}
		//V attack
		else if (state == State.VAttack) {
			targetLocation = new Vector3 (target.position.x, target.position.y, -1);
			VWave ();
			for(float i = .1f; i <= .5f; i+=.1f){
				Invoke("VWave", i);
			}
			state = State.Idle;
		}
		//Stomp Attack
		else if (state == State.StompAttack) {
			StompWave();
			for(float i = .1f; i <= .5f; i+=.1f){
				Invoke("StompWave", i);
			}
			state = State.Idle;
		}
	}

	void VWave() {
		float angle = Mathf.Atan2(targetLocation.y-transform.position.y, targetLocation.x-transform.position.x)*180 / Mathf.PI;
		angle -= 90;
		angle -= 14;
		for (int i = 0; i < 2; i++) {
			GameMaster.CreateBullet (bulletPrefab, vFirePoint.position, angle, 10, 10, 80, true, false);
			angle += 20;
		}
	}

	void StompWave(){
		for(int i=0; i<=360; i+=30){
			GameMaster.CreateBullet (bulletPrefab, stompFirePoint.position, i, 10, 10, 50, true, false);
		}
	}

	void GrenadeWave(){
		GameMaster.CreateGrenade (grenadePrefab, grenadeFirePoint.position, Random.Range (0, 360), 20, Random.Range(5, 15), 100, true, false);
	}

	void OnCollisionEnter2D (Collision2D other) {
	}
}

