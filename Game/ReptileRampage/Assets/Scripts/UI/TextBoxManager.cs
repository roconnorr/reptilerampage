using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject player;

	private Player playerScript;

	public GameObject BossTrigger;
	public GameObject dialogBox;
	public Image speakingCharacter;
	public Sprite[] characterPortraits;

	public enum CurrentLevelBoss {trike, trex, gavin};
	public CurrentLevelBoss levelBoss;
	public Text text;
	public bool dialogActive = false;
	public bool inBossFight = false;

	private HUDManager hudManager;

	public TextAsset[] textFiles;
	private int dialogTextNumber;
	public string[] textLines;
	public int currentLine;
	public static bool dialogFinished;

	void Start () {
		hudManager = GameObject.Find("Canvas").GetComponent<HUDManager>();
		playerScript = player.GetComponent<Player>();
		if(textFiles[dialogTextNumber] != null){
			textLines = (textFiles[dialogTextNumber].text.Split('\n'));
		}
		dialogBox.SetActive(false);
	}

	
	// Update is called once per frame
	void Update () {
		if(dialogActive){
			playerScript.canMove = false;
			playerScript.canShoot = false;
			playerScript.slot[playerScript.slotActive].SetActive(false);
			hudManager.HideBottomHUD(true);
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
						//BossTrigger.GetComponent<TRexFight>().SpawnTRex();
					}else{
						//gavin
						//BossTrigger.GetComponent<TrikeFight>().SpawnTrike();
					}
				}

				dialogActive = false;
				if(Player.scene.name == "Level1" && PlayDialog.atDialog0){
					dialogFinished = true;
					PlayDialog.atDialog0 = false;
				}else if(Player.scene.name == "Level2" && TRexFight.atTrexDialog){
					dialogFinished = true;
					TRexFight.atTrexDialog = false;
				}else{
					playerScript.canMove = true;
					playerScript.canShoot = true;
				}
			}else{
				text.text = textLines[currentLine].ToUpper();
			}
		}else{
			hudManager.HideBottomHUD(false);
		}
	}

	public void SetDialogNumber(int num, int charIndex){
		speakingCharacter.sprite = characterPortraits[charIndex];
		dialogTextNumber = num;
		if(textFiles[dialogTextNumber] != null){
			textLines = (textFiles[dialogTextNumber].text.Split('\n'));
		}
		currentLine = 0;
	}
}
