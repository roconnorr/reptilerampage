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
			animator.Play ("ClosedLeft");
		}
		if (isLeft) {
			animator.Play ("ClosedLeft");
		}
		if (isUp) {
			animator.Play ("ClosedUp");
		}
		if (isDown) {
			animator.Play ("ClosedDown");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Player> () || other.GetComponent<Enemy> ()) {
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

	void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<Player> () || other.GetComponent<Enemy> ()) {
			open = false;
			if (isLeft || isRight) {
				animator.Play ("CloseLeft");
			}
			if (isUp) {
				animator.Play ("CloseUp");
			}
			if (isDown) {
				animator.Play ("CloseDown");
			}
		}
	}
}
