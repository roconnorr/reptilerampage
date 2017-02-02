using UnityEngine;

public class YLayering : MonoBehaviour {

	public bool onUpdate;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}

	void Update() {
		if (onUpdate) {
			if (GetComponent<SpriteRenderer> ().sortingOrder != -9999) {
				GetComponent<SpriteRenderer> ().sortingOrder = Mathf.RoundToInt (transform.position.y * 100f) * -1;
			}
		}
	}
}
