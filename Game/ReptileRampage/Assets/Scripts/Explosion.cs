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

	void FixedUpdate () {
		//AudioSource.PlayClipAtPoint (deathRoar, transform.position);
		Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, radius);
		foreach(Collider2D col in colliders){
			if(col.tag == "DestructibleWall"){
				col.GetComponent<DestructibleWall>().destroy();
			}
			Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
			if(rb != null){
				//Player takes damage if in radius
				if(rb.tag == "Player"){
					AddExplosionForce(rb, power * 1000, position, radius);
					rb.GetComponent<Player>().TakeDamage (explodeDamage, transform.rotation);
				}
				if(rb.tag == "Enemy"){
					AddExplosionForce(rb, power * 1000, position, radius);
					rb.GetComponent<Enemy>().TakeDamage (explodeDamage, transform.rotation);
				}
				if(rb.tag == "Crate"){
					AddExplosionForce(rb, power * 1000, position, radius);
					rb.GetComponent<Crate>().TakeDamage (explodeDamage);
				}
			}
		}
		Destroy (gameObject);
	}

	void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius){
		var dir = (body.transform.position - expPosition);
		float calc = 1 - (dir.magnitude / expRadius);
		if (calc <= 0) {
			calc = 0;		
		}
		body.AddForce (dir.normalized * expForce * calc);
		//body.velocity = Vector3.Lerp(body.velocity, dir*200 , 1000 * Time.deltaTime);
	}
}
