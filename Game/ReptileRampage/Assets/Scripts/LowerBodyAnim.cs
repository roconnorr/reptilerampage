using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBodyAnim : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
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
	}
}
