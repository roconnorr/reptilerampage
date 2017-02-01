using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject player;
	public GameObject textBox;

	public Text text;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	void Start () {
		if(textFile != null){
			textLines = (textFile.text.Split('\n'));
		}

		if(endAtLine == 0){
			endAtLine = textLines.Length -1;
		}

	}
	
	// Update is called once per frame
	void Update () {
		player.GetComponent<Player>().canMove = false;
		text.text = textLines[currentLine];

		if(Input.GetButtonDown("Fire1")){
			currentLine += 1;
		}

		if(currentLine > endAtLine){
			player.GetComponent<Player>().canMove = true;
			textBox.SetActive(false);
			this.gameObject.SetActive(false);
		}
	}
}
