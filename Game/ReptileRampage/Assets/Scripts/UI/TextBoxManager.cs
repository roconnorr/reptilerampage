using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject player;

	public GameObject BossTrigger;
	public GameObject dialogBox;
	public Text text;
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
	}
	
	// Update is called once per frame
	void Update () {
		if(dialogActive){
			player.GetComponent<Player>().canMove = false;
			player.GetComponent<Player>().canShoot = false;
			dialogBox.SetActive(true);
			text.text = textLines[currentLine].ToUpper();

			if(Input.GetButtonDown("Fire1")){
				currentLine += 1;
			}

			if(currentLine > endAtLine){
				dialogBox.SetActive(false);
				BossTrigger.GetComponent<TRexFight>().SpawnTRex();
				dialogActive = false;
				player.GetComponent<Player>().canMove = true;
				player.GetComponent<Player>().canShoot = true;
			}
		}
	}
}
