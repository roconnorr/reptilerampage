using UnityEngine;

public class LowerBodyAnim : MonoBehaviour {

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
	}
	
	void Update () {

		if (gameObject.GetComponentInParent<Player> ().canMove) {
			if (Input.GetKey ("d")) {
				animator.Play ("lower_body_run_right");
			} else if (Input.GetKey ("a")) {
				animator.Play ("lower_body_run_left");
			} else if (Input.GetKey ("w")) {
				animator.Play ("lower_body_run_up");
			} else if (Input.GetKey ("s")) {
				animator.Play ("lower_body_run_down");
			} else {
				animator.Play ("lower_body_idle");
			}
			GetComponent<SpriteRenderer> ().sortingOrder = Mathf.RoundToInt (transform.position.y * 100f) * -1;
		} else {
			animator.Play ("lower_body_idle");
		}
	}
}
