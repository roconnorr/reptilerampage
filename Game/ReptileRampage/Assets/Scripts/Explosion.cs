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

	public Sprite[] scorchMarks;
	public GameObject scorchPrefab;

	public AudioClip explosionSound;

	private Transform source = null;

	void Start() {
		GameObject scorch = Instantiate (scorchPrefab, transform.position, transform.localRotation);
		scorch.GetComponent<SpriteRenderer> ().sprite = scorchMarks [Random.Range (0, 8)];
		if (playerSource) {
			source = GameObject.Find ("Player").transform;
		}
		gameObject.GetComponent<CameraShake>().StartShaking(power/50);
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
					rb.GetComponent<Player> ().TakeDamage (explodeDamage / 3, Quaternion.Euler (0, 0, angle), power, source);
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
		Invoke ("Destroy", 1);
	}

	void Destroy() {
		Destroy (gameObject);
	}
}
