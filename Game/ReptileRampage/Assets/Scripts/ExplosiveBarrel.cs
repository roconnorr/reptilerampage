using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour {

	public GameObject animationPrefab;
	public GameObject explosionScriptPrefab;
	public float radius = 3.0F;
    public float power = 300.0F;
	public int explodeDamage = 30;
    private Camera mainCamera;
    void Start() {
		mainCamera = Camera.main;
    }

	public void Destroy() {
		Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
 		bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
		if(onScreen){
			GameMaster.CreateExplosion(animationPrefab, explosionScriptPrefab ,transform.position, explodeDamage, power, radius, false);
			Destroy (gameObject);
		}
	}
}
