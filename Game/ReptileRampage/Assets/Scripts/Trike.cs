using UnityEngine;

public class Trike : MonoBehaviour {
	
	private enum State {Idle, Walking, VAttack, GrenadeAttack, StompAttack};

	private State state;

	public Transform target;
	public Transform bulletPrefab;
	public Transform grenadePrefab;

	public float bulletKnockbackForce;

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
		if(target == null){
			Destroy(gameObject);
			return;
		}
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
					state = State.VAttack;
					timeSinceLastAction = 0;
				} else if (rand < 5) {
					state = State.GrenadeAttack;
					timeSinceLastAction = 0;
				} else if (rand < 6) {
					state = State.StompAttack;
					timeSinceLastAction = 0;
				}
			}
		}
		if (IsInvoking("VWave")) {
			int rand = Random.Range (0, 200);
			if (rand < 1) {
				GrenadeWave ();
			} else if (rand < 2) {
				StompWave ();
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
		else if (state == State.GrenadeAttack && !IsInvoking("GrenadeWave")) {
			for(float i = 1f; i <= 3f; i+=1f){
				Invoke("GrenadeWave", i);
			}
			Invoke("SetStateIdle", 3f);
		}
		//V attack
		else if (state == State.VAttack && !IsInvoking("VWave")) {
			targetLocation = new Vector3 (target.position.x, target.position.y, -1);
			for(float i = 0f; i <= 5f; i+=.1f){
				Invoke("VWave", i);
			}
			Invoke("SetStateIdle", 5f);
		}
		//Stomp Attack
		else if (state == State.StompAttack && !IsInvoking("StompWave")) {
			for(float i = 1f; i <= 3f; i+=1f){
				Invoke("StompWave", i);
			}
			Invoke("SetStateIdle", 3f);
		}
	}

	void VWave() {
		float angle = Mathf.Atan2(target.position.y-vFirePoint.position.y, target.position.x-vFirePoint.position.x)*180 / Mathf.PI;
		angle -= 90;
		angle -= 10;
		for (int i = 0; i < 2; i++) {
			GameMaster.CreateBullet (bulletPrefab, vFirePoint.position, bulletKnockbackForce, angle, 10, 10, 80, true, false);
			angle += 20;
		}
	}

	void StompWave(){
		for(int i=0; i<=360; i+=10){
			GameMaster.CreateBullet (bulletPrefab, stompFirePoint.position, bulletKnockbackForce, i, 10, 5, 150, true, false);
		}
	}

	void GrenadeWave(){
		float angle = Mathf.Atan2(target.position.y-vFirePoint.position.y, target.position.x-vFirePoint.position.x)*180 / Mathf.PI;
		angle -= 90;
		GameMaster.CreateGrenade (grenadePrefab, grenadeFirePoint.position, angle, 20, Random.Range(20, 30), 100, true, false);
	}

	void SetStateIdle() {
		state = State.Idle;
	}

	void OnCollisionEnter2D (Collision2D other) {
	}
}

