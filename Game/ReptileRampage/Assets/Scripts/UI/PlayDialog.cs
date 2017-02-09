using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDialog : MonoBehaviour {

	public GameObject canvas;

	private TextBoxManager textBoxManager;

	// Use this for initialization
	void Start () {
		textBoxManager = canvas.GetComponent<TextBoxManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Dialog1"){
			textBoxManager.SetDialogNumber(1);
			textBoxManager.dialogActive = true;
		}
	}
}
