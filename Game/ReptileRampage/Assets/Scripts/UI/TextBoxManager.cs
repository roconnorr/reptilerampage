using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject player;

	public Text textDisplay;

	public GameObject dialogBox;

	public bool dialogActive = false;

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
		dialogBox.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if(dialogActive){
			dialogBox.SetActive(true);
			textDisplay.text = textLines[currentLine];

			if(Input.GetButtonDown("Fire1")){
				currentLine += 1;
			}

			if(currentLine > endAtLine){
				dialogBox.SetActive(false);
				this.gameObject.GetComponent<TRexFight>().SpawnTRex();
				dialogActive = false;
			}
		}
	}
}
