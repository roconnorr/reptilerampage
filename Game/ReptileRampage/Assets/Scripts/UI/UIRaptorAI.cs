using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRaptorAI : MonoBehaviour {

	private Animator animator;

	void Start(){
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.Play ("velociraptor_run");
	}
}
