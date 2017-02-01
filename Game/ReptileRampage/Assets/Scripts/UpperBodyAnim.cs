using UnityEngine;

public class UpperBodyAnim : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		float rotation = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		if (rotation < 0) {
			rotation += 360;
		}
		if (rotation >= 0 && rotation < 45 || rotation < 360 && rotation > 315) {
			animator.Play("upper_body_right");
		}
		if (rotation >= 45 && rotation < 135) {
			animator.Play("upper_body_up");
		}
		if (rotation >= 135 && rotation < 225) {
			animator.Play("upper_body_left");
		}
		if (rotation >= 225 && rotation < 315) {
			animator.Play("upper_body_down");
		}
		if (Input.GetKey ("w") || Input.GetKey ("a") || Input.GetKey ("s") || Input.GetKey ("d")) {
			animator.speed = 1;
		} else {
			animator.speed = 0;
		}

		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}
}
