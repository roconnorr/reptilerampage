using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {

	public bool isLeft;
	public bool isRight;
	public bool isUp;
	public bool isDown;

	private bool open = false;
	private Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
		if (isRight) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Player> ()) {
			open = true;
			if (isLeft || isRight) {
				animator.Play ("OpenLeft");
			}
			if (isUp) {
				animator.Play ("OpenUp");
			}
			if (isDown) {
				animator.Play ("OpenDown");
			}
		}
	}
}
