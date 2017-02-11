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
	private Animator animator;
	private int stomps;
	private bool isStomping;
	private int walkTimer = 0;

	void Start () {
		state = State.Idle;
		vFirePoint = transform.Find ("VFirePoint");
		grenadeFirePoint = transform.Find ("GrenadeFirePoint");
		stompFirePoint = transform.Find ("StompFirePoint");
		animator = GetComponent<Animator>();
		GetComponent<Enemy>().noFlip = false;
	}
	
	void Update () {
		if (walkTimer > 0) {
			walkTimer--;
		}
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
				if (rand < 6) {
					state = State.Walking;
					timeSinceLastAction = 0;
					walkTimer = 5;
					targetLocation = new Vector3 (target.position.x, target.position.y, -1);
				} else if (rand < 8) {
					state = State.VAttack;
					timeSinceLastAction = 0;
				} else if (rand < 9) {
					state = State.GrenadeAttack;
					timeSinceLastAction = 0;
				} else if (rand < 10) {
					state = State.StompAttack;
					timeSinceLastAction = 0;
				}
			}
		}
		if (IsInvoking("VWave")) {
			int rand = Random.Range (0, 400);
			if (rand < 1) {
				GrenadeWave ();
			} else if (rand < 2) {
				stomps = 1;
				animator.Play("TrikeStomp");
			}
		}
		//Walking Behaviour
		else if (state == State.Walking) {
			animator.Play("TrikeWalk");
			transform.position = Vector3.MoveTowards (transform.position, targetLocation, speed / 40);
			if (Vector3.Distance (targetLocation, transform.position) < 0.01) {
				state = State.Idle;
				animator.Play("TrikeIdle");
			}
			if ((transform.position.x > xPrev + 0.02) && !flipped) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				flipped = true;
				GetComponent<Enemy>().noFlip = true;
			}
			if ((transform.position.x < xPrev - 0.02) && flipped) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				flipped = false;
				GetComponent<Enemy>().noFlip = false;
			}
			xPrev = transform.position.x;
		}
		//Fire Grenades
		else if (state == State.GrenadeAttack && !IsInvoking("GrenadeWave")) {
			if ((target.position.x > transform.position.x) && !flipped) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				flipped = true;
				GetComponent<Enemy>().noFlip = true;
			}
			if ((target.position.x < transform.position.x) && flipped) {
				transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
				flipped = false;
				GetComponent<Enemy>().noFlip = false;
			}
			
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
		else if (state == State.StompAttack && !isStomping) {
			isStomping = true;
			animator.Play("TrikeStomp");
			stomps = 3;
		}
	}

	void VWave() {
		float angle = Mathf.Atan2(target.position.y-vFirePoint.position.y, target.position.x-vFirePoint.position.x)*180 / Mathf.PI;
		angle -= 90;
		angle -= 10;
		for (int i = 0; i < 2; i++) {
			GameMaster.CreateBullet (bulletPrefab, vFirePoint.position, bulletKnockbackForce, angle, 10, 10, 150, true, false, transform);
			angle += 20;
		}
	}

	void StompWave(){
		if ((target.position.x > transform.position.x) && !flipped) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			flipped = true;
			GetComponent<Enemy>().noFlip = true;
		}
		if ((target.position.x < transform.position.x) && flipped) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
			flipped = false;
			GetComponent<Enemy>().noFlip = false;
		}

		for(int i=0; i<=360; i+=20){
			GameMaster.CreateBullet (bulletPrefab, stompFirePoint.position, bulletKnockbackForce, i, 10, 5, 200, true, false, transform);
		}
	}

	void StompAgain() {
		stomps--;
		if (stomps > 0) {
			animator.Play ("TrikeStomp");
		} else {
			isStomping = false;
			state = State.Idle;
		}
	}

	void GrenadeWave(){
		float angle = Mathf.Atan2(target.position.y-vFirePoint.position.y, target.position.x-vFirePoint.position.x)*180 / Mathf.PI;
		angle -= 90;
		GameMaster.CreateGrenade (grenadePrefab, grenadeFirePoint.position, angle, 20, Random.Range(30, 40), 100, true, false);
	}

	void SetStateIdle() {
		state = State.Idle;
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "BossCollider" && state == State.Walking && walkTimer == 0) {
			state = State.Idle;
		}
	}
}

