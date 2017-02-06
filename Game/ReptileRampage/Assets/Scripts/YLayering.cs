using UnityEngine;

public class YLayering : MonoBehaviour {

	public bool onUpdate;
	public bool isStego;
	public bool isParticles;

	// Use this for initialization
	void Start () {
		if (isParticles) {
			GetComponent<Renderer>().sortingOrder = (Mathf.RoundToInt (transform.position.y * 100f) * -1) + 1;
		} else {
			GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt (transform.position.y * 100f) * -1;
		}
	}

	void Update() {
		if (onUpdate) {
			if (GetComponent<SpriteRenderer> ().sortingOrder != -9999) {
				GetComponent<SpriteRenderer> ().sortingOrder = Mathf.RoundToInt (transform.position.y * 100f) * -1;
				if (isStego) {
					GetComponentInChildren<StegoTurret>().GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt (transform.position.y * 100f) * -1;
				}
			}
		}
	}
}
