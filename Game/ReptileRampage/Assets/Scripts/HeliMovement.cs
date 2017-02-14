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
		bobSpeed = 0.06f;
		bobHeight = 0.2f;
	}

	void Update() {
		if(Time.timeScale != 0){
			bobAmount = bobHeight*Mathf.Sin(count);
			transform.position = new Vector3(transform.position.x, yBase + bobAmount);
			count += bobSpeed;
		}
	}
}
