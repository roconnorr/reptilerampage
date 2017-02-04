using UnityEngine;

public class Grenade : MonoBehaviour {

	public GameObject animationPrefab;
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
	public float radius = 3.0F;
	[HideInInspector]
    public float power = 300.0F;
	[HideInInspector]
	public int explodeDamage = 30;

	private float amplitude = 2f;
	private float height = 0;
	private float oldHeight = 0;
	private float time = 0;
	private float period = 1f;

	public AudioClip wallHitSound = null;

	void Update () {
		amplitude *= 0.93f; //Lower = faster bounce height diminished
		time += 0.1f; //Lower = slower overall
		period += 0.08f; //Higher = faster bounce length diminish
		moveSpeed *= 0.95f; //Lower = faster speed diminish
		oldHeight = height;

		//Bounce
		height = Mathf.Abs(Mathf.Sin(time * period)) * -amplitude;
		transform.position = new Vector3 (transform.position.x, transform.position.y + oldHeight, transform.position.z);
		transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);
		transform.position = new Vector3 (transform.position.x, transform.position.y - height, transform.position.z);

		if (range > 0) {
			range--;
		} else {
			Explode ();
		}
	}

	//Collide with wall and player
	void OnCollisionEnter2D(Collision2D other){
		if (!dmgPlayer && other.gameObject.tag != "Player") {
			amplitude /= 2;
			moveSpeed /= 2;
		}
	}

	void Explode(){
		radius = 3.0f;
		GameMaster.CreateExplosion(animationPrefab, explosionScriptPrefab, transform.position, explodeDamage, power, radius);
		Destroy (gameObject);
	}
}
