using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour {

	SpriteRenderer sprite = null;

	private Enemy enemyScript;
	private Vector3 pos;

	private GameObject[] enemyGameObjects;
	private Rigidbody2D player;

	
	// Use this for initialization
	void Start () {
		enemyGameObjects = GameObject.FindGameObjectsWithTag("Enemy");
		player = GetComponent<Rigidbody2D>();
		player.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
 			pos.z = transform.position.z;
			this.transform.position = pos;
		}
	}

}