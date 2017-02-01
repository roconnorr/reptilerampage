using UnityEngine;

public class LowerBodyAnim : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponentInParent<Player>().canMove){
			if(Input.GetKey("d")) {
				animator.Play("lower_body_run_right");
			} else if(Input.GetKey("a")) {
				animator.Play("lower_body_run_left");
			} else if(Input.GetKey("w")) {
				animator.Play("lower_body_run_up");
			} else if(Input.GetKey("s")) {
				animator.Play("lower_body_run_down");
			} else {
				animator.Play("lower_body_idle");
			}
		}else{
			animator.Play("lower_body_idle");
		}

		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}
}
