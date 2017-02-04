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
				float angle =Mathf.Atan2(rb.transform.position.y-transform.position.y, rb.transform.position.x-transform.position.x)*180 / Mathf.PI;
				angle -= 90;
				//Player takes damage if in radius
				if(rb.tag == "Player"){
					rb.GetComponent<Player>().TakeDamage (explodeDamage, Quaternion.Euler(0, 0, angle), power);
				}
				if(rb.tag == "Enemy"){
					rb.GetComponent<Enemy>().TakeDamage (explodeDamage, Quaternion.Euler(0, 0, angle), power);
				}
				if(rb.tag == "Crate"){
					rb.GetComponent<Crate>().TakeDamage (explodeDamage);
				}
				if(rb.tag == "Explosive"){
					rb.GetComponent<ExplosiveBarrel>().TakeDamage (explodeDamage);
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
