using UnityEngine;

public class Explosion : MonoBehaviour {

	[HideInInspector]
	public float radius;
	[HideInInspector]
    public float power;
	[HideInInspector]
	public int explodeDamage;
	[HideInInspector]
	public Vector3 position;
	[HideInInspector]
	public bool playerSource;

	public AudioClip explosionSound;

	private Transform source = null;

	void Start() {
		if (playerSource) {
			source = GameObject.Find ("Player").transform;
		}
		gameObject.GetComponent<CameraShake>().StartShaking(power/50);
	}

	void FixedUpdate () {
		AudioSource.PlayClipAtPoint (explosionSound, transform.position);
		Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, radius);
		foreach(Collider2D col in colliders){
			if(col.tag == "DestructibleWall"){
				col.GetComponent<DestructibleWall>().destroy();
			}
			Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
			if(rb != null){
				float angle =Mathf.Atan2(rb.transform.position.y-transform.position.y, rb.transform.position.x-transform.position.x)*180 / Mathf.PI;
				angle -= 90;
				//Player takes damage if in radius
				if(rb.tag == "Player"){
					rb.GetComponent<Player> ().TakeDamage (explodeDamage / 2, Quaternion.Euler (0, 0, angle), power, source);
				}
				if(rb.tag == "Enemy"){
					if (rb.GetComponent<Enemy> ()) {
						rb.GetComponent<Enemy> ().TakeDamage (explodeDamage, Quaternion.Euler (0, 0, angle), power, source, true);
					}
				}
				if(rb.tag == "Crate"){
					rb.GetComponent<Crate>().TakeDamage (explodeDamage);
				}
				if(rb.tag == "Explosive"){
					rb.GetComponent<ExplosiveBarrel>().Destroy();
				}
			}
		}
		Destroy (gameObject);
	}

//	void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius){
//		var dir = (body.transform.position - expPosition);
//		float calc = 1 - (dir.magnitude / expRadius);
//		if (calc <= 0) {
//			calc = 0;		
//		}
//		body.AddForce (dir.normalized * expForce * calc);
//		//body.velocity = Vector3.Lerp(body.velocity, dir*200 , 1000 * Time.deltaTime);
//	}
}
