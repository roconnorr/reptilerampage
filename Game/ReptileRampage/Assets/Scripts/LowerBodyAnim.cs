using UnityEngine;

public class LowerBodyAnim : MonoBehaviour {

	private Animator animator;
	private float horizontal;
	private float vertical;

	void Start () {
		animator = GetComponent<Animator>();
	}
	
	void Update () {
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		if(gameObject.GetComponentInParent<Player>().canMove){
			if (Mathf.Abs (horizontal) > Mathf.Abs (vertical)) {
				if (horizontal > 0) {
					animator.Play ("lower_body_run_right");
				} else {
					animator.Play ("lower_body_run_left");
				}
			} else if (Mathf.Abs (horizontal) < Mathf.Abs (vertical)){
				if(vertical < 0) {
					animator.Play("lower_body_run_up");
				} else {
					animator.Play("lower_body_run_down");
				}
			} else {
				animator.Play("lower_body_idle");
			}
		}else{
			animator.Play("lower_body_idle");
		}

		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}
}
