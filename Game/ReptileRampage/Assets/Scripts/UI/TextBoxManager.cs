using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject player;

	public GameObject BossTrigger;
	public GameObject dialogBox;

	public enum CurrentLevelBoss {trike, trex, gavin};
	public CurrentLevelBoss levelBoss;
	public Text text;
	public bool dialogActive = false;
	public bool inBossFight = false;

	public TextAsset[] textFiles;
	private int dialogTextNumber;
	public string[] textLines;

	public int currentLine;

	void Start () {
		if(textFiles[dialogTextNumber] != null){
			textLines = (textFiles[dialogTextNumber].text.Split('\n'));
		}
		dialogBox.SetActive(false);
	}

	
	// Update is called once per frame
	void Update () {
		if(dialogActive){
			player.GetComponent<Player>().canMove = false;
			player.GetComponent<Player>().canShoot = false;
			dialogBox.SetActive(true);

			if(Input.GetButtonDown("Fire")){
				currentLine++;
			}

			if(currentLine == textLines.Length){
				dialogBox.SetActive(false);
				if(inBossFight){
					if(levelBoss == CurrentLevelBoss.trike){
						BossTrigger.GetComponent<TrikeFight>().SpawnTrike();
					}else if(levelBoss == CurrentLevelBoss.trex){
						BossTrigger.GetComponent<TRexFight>().SpawnTRex();
					}else{
						//gavin
						//BossTrigger.GetComponent<TrikeFight>().SpawnTrike();
					}
				}

				dialogActive = false;
				player.GetComponent<Player>().canMove = true;
				player.GetComponent<Player>().canShoot = true;
			}else{
				text.text = textLines[currentLine].ToUpper();
			}
		}
	}

	public void SetDialogNumber(int num){
		dialogTextNumber = num;
		if(textFiles[dialogTextNumber] != null){
			textLines = (textFiles[dialogTextNumber].text.Split('\n'));
		}
		currentLine = 0;
	}
}
