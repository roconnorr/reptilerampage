using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {

	public GameObject animationPrefab;
	public GameObject explosionScriptPrefab;
	public float radius = 3.0F;
    public float power = 300.0F;
	public int explodeDamage = 30;
    
    void Start() {
		//soundSource = gameObject.GetComponent<AudioSource>();
    }

	public void Destroy() {
		GameMaster.CreateExplosion(animationPrefab, explosionScriptPrefab ,transform.position, explodeDamage, power, radius);
		Destroy (gameObject);
	}
}
