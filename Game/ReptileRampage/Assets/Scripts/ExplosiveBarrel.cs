using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {

	public int health = 30;
	public GameObject animationPrefab;
	public GameObject explosionScriptPrefab;
	public float radius = 3.0F;
    public float power = 300.0F;
	public int explodeDamage = 30;
    
    void Start() {
		//soundSource = gameObject.GetComponent<AudioSource>();
    }

	public void TakeDamage(int amount) {
		health -= amount;
		if (health <= 0) {
			GameMaster.CreateExplosion(animationPrefab, explosionScriptPrefab ,transform.position, explodeDamage, power, radius);
			Destroy (gameObject);
		}
	}
}
