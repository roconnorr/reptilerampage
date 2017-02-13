using UnityEngine;

public class HeliMovement : MonoBehaviour {

	private float yBase;
	private float count;
	private float bobSpeed;
	private float bobHeight;
	private float bobAmount;
	void Start() {
		yBase = transform.position.y;
		count = 0;
		bobSpeed = 0.1f;
		bobHeight = 0.3f;
	}

	void Update() {
		bobAmount = bobHeight*Mathf.Sin(count);
		transform.position = new Vector3(transform.position.x, yBase + bobAmount);
		count += bobSpeed;
	}
}
