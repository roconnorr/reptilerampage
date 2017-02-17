using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	int currentChar = 0;
	int maxChar = 0;

	public static bool lastDialog = false;

	private bool lastDialogChanged = false;
	private bool trexSpawned;
	private bool trikeSpawned;
	public float letterPause = 0.01f;

	void Start () {
		hudManager = GameObject.Find("Canvas").GetComponent<HUDManager>();
		playerScript = player.GetComponent<Player>();
		if(textFiles[dialogTextNumber] != null){
			textLines = (textFiles[dialogTextNumber].text.Split('\n'));
		}
		text.text = "";
		dialogBox.SetActive(false);
		currentChar = 0;
		maxChar = textLines [0].Length;
	}

	
	// Update is called once per frame
	void Update () {
		if(dialogActive){
			playerScript.canMove = false;
			playerScript.canShoot = false;
			//playerScript.slot[playerScript.slotActive].SetActive(false);
			hudManager.HideBottomHUD(true);
			dialogBox.SetActive(true);

			//Next line
			if(Input.GetButtonDown("Fire")){
				if (currentChar < maxChar) {
					text.text = textLines [currentLine];
					currentChar = maxChar;
				} else {
					currentLine++;
					if (lastDialogChanged) {
						//nasti hackk
						SceneManager.LoadScene ("WinScreen");
					}
					if (lastDialog && !lastDialogChanged) {
						SetDialogNumber (10, 0);
						lastDialogChanged = true;
					}
					if (currentLine < textLines.Length) {
						text.text = "";
						currentChar = 0;
						maxChar = textLines [currentLine].Length;
					}
				}
			}

			//End of dialog
			if(currentLine == textLines.Length){
				dialogBox.SetActive(false);
				dialogActive = false;
				if (Player.scene.name == "Level1" && PlayDialog.atDialog0) {
					dialogFinished = true;
					PlayDialog.atDialog0 = false;
				} else if (Player.scene.name == "Level1" && TrikeFight.atTrikeDialog) {
					dialogFinished = true;
					TrikeFight.atTrikeDialog = false;
				} else if (Player.scene.name == "Level2" && TRexFight.atTrexDialog) {
					dialogFinished = true;
					TRexFight.atTrexDialog = false;
				} else if (Player.scene.name == "Level2" && PlayDialog.atDialog6) {
					dialogFinished = true;
					PlayDialog.atDialog6 = false;
				}else if(Player.scene.name == "FinalBoss" && !lastDialog){
					GameObject.Find("Diplo").GetComponent<Gavin>().isActive = true;
					Invoke ("unFreeze", 0.1f);
				}else{
					Invoke ("unFreeze", 0.1f);
				}
			}else{
				if (currentChar < maxChar) {
					text.text += textLines [currentLine][currentChar];
					currentChar++;
				}
			}
		}else{
			hudManager.HideBottomHUD(false);
		}

		if(inBossFight && levelBoss == CurrentLevelBoss.trike && WayPoints.triggerdTrike && !trikeSpawned){
			BossTrigger.GetComponent<TrikeFight>().SpawnTrike();
			trikeSpawned = true;
			WayPoints.triggerdTrike = false;
		}
		//Spawn Trex after dialog and cut scene is finished
		if(inBossFight && levelBoss == CurrentLevelBoss.trex && WayPoints.triggerdTrex && !trexSpawned){
			BossTrigger.GetComponent<TRexFight>().SpawnTRex();
			WayPoints.trexSpawnAnimationPlaying = true;
			trexSpawned = true;
			WayPoints.triggerdTrex = false;
		}
		if(inBossFight && levelBoss == CurrentLevelBoss.gavin){

		}
	}

	public void SetDialogNumber(int num, int charIndex){
		speakingCharacter.sprite = characterPortraits[charIndex];
		dialogTextNumber = num;
		if(textFiles[dialogTextNumber] != null){
			textLines = (textFiles[dialogTextNumber].text.Split('\n'));
		}
		currentLine = 0;
		text.text = "";
		currentChar = 0;
		maxChar = textLines [0].Length;
	}

	private void unFreeze() {
		playerScript.canMove = true;
		playerScript.canShoot = true;
	}
		  
}
