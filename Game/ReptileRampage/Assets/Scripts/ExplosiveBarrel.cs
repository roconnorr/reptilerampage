using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {

	//private AudioSource soundSource;

	public int health = 30;
	public float radius = 5.0F;
    public float power = 300.0F;
	public int explodeDamage = 20;
    
    void Start() {
		//soundSource = gameObject.GetComponent<AudioSource>();
    }

	public void TakeDamage(int amount) {
		health -= amount;
		if (health <= 0) {
			//AudioSource.PlayClipAtPoint (deathRoar, transform.position);
			Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, radius);
			foreach(Collider2D col in colliders){
				Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
				if(rb != null){
					//Player takes damage if in radius
					if(rb.tag == "Player"){
						AddExplosionForce(rb, power * 100, transform.position, radius);
						rb.GetComponent<Player>().TakeDamage (explodeDamage, transform.rotation);
					}
					if(rb.tag == "Enemy"){
						AddExplosionForce(rb, power * 100, transform.position, radius);
						rb.GetComponent<Enemy>().TakeDamage (explodeDamage, transform.rotation);
					}
				}
			}
			Destroy (gameObject);
		}
	}

	void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius){
			var dir = (body.transform.position - expPosition);
			float calc = 1 - (dir.magnitude / expRadius);
			if (calc <= 0) {
				calc = 0;		
			}
			body.AddForce (dir.normalized * expForce * calc);
	}
}
