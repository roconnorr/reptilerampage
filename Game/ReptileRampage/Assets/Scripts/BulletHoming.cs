using UnityEngine;

public class BulletHoming : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject explosionScriptPrefab;
	[HideInInspector]
	public float moveSpeed;
	[HideInInspector]
	public int damage;
	[HideInInspector]
	public float range;
	[HideInInspector]
	public bool dmgPlayer;
	[HideInInspector]
	public bool dmgEnemy;
	[HideInInspector]
	public float initialAngle;
	[HideInInspector]
	public Transform target;
	[HideInInspector]
	private Rigidbody2D rb;
	[HideInInspector]
	public int iFrames;
	[HideInInspector]
	public Transform source;
	public AudioClip wallHitSound = null;
	public static bool bridgeExploded;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		iFrames = 50;
		rb.AddForce ((Quaternion.Euler(0, 0, initialAngle) * Vector2.up) * moveSpeed);
	}

	void Update () {
		if(Time.timeScale != 0){
			if (range > 0) {
				range--;
			} else {
				if(Player.scene.name == "Level2" && WayPoints.arrived){
					Explode ();
					bridgeExploded = true;
				}else{
					Explode ();
				}
			}
			if (iFrames > 0) {
				iFrames--;
			}
		}

		if(rb.velocity.magnitude > moveSpeed) {
			rb.velocity = rb.velocity.normalized * moveSpeed;
		}
		rb.AddForce(Vector3.Normalize (target.position - transform.position) * 0.16f);
		float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
	}

	//Collide with wall and player
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Wall"){
			if (wallHitSound != null){
				AudioSource.PlayClipAtPoint(wallHitSound, transform.position);
			}
			Explode();
		}
		if (other.gameObject.tag == "Player" && iFrames == 0) {
			if (dmgPlayer) {
				Explode ();
			}
		}
		if (other.gameObject.tag == "Enemy" && dmgEnemy && iFrames == 0) {
			Explode ();
		}
		if (other.gameObject.tag == "Bridge" && iFrames == 0){
			Debug.Log("hit");
			Explode ();
		}
	}

	void Explode(){
		GameMaster.CreateExplosion(explosionPrefab, explosionScriptPrefab, transform.position, damage, 200, 3f, !dmgPlayer);
		Destroy(gameObject);
	}
}
