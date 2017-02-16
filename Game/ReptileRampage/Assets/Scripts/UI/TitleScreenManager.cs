using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour {

	public GameObject ButtonStart;
	public GameObject ButtonArena;
	public GameObject ButtonQuit;
	public GameObject ButtonCredits;
	public GameObject ButtonCreditsQuit;
	public Image CreditsImage;

	void Start(){
		ButtonStart.SetActive(false);
		ButtonQuit.SetActive(false);
		ButtonArena.SetActive(false);
		ButtonCredits.SetActive(false);
		ButtonCreditsQuit.SetActive(false);
		CreditsImage.enabled = false;
		Cursor.visible = true;
		WayPoints.level1IntroFinished = false;
		TextBoxManager.dialogFinished = false;
		WayPoints.arrived = false;
		WayPoints.respawned = false;
		WayPoints.triggerdTrike = false;
		WayPoints.heliMoving = false;
		WayPoints.triggerdTrex = false;
		WayPoints.trexSpawnAnimationPlaying = false;
		WayPoints.trexSpawnAnimationFinished = false;
		WayPoints.trexCam = false;
		GameMaster.slot1type = Player.WeaponType.empty;
		GameMaster.slot2type = Player.WeaponType.empty;
		GameMaster.slot3type = Player.WeaponType.m1911;
		GameMaster.levelStartSlot1Type = Player.WeaponType.empty;
		GameMaster.levelStartSlot2Type = Player.WeaponType.empty;
		GameMaster.levelStartSlot3Type = Player.WeaponType.m1911;
		GameMaster.grenadeCount = 3;
		GameMaster.slot1ammo = 0;
		GameMaster.slot2ammo = 0;
		GameMaster.slot1MaxAmmo = 0;
		GameMaster.slot2MaxAmmo = 0;
		GameMaster.levelStartSlot1Ammo = 0;
		GameMaster.levelStartSlot2Ammo = 0;
		GameMaster.level1Checkpoint = false;
		GameMaster.level2Checkpoint = false;
		GameMaster.currentLevel = 1;
	}

	public void EnableButtons(){
		ButtonStart.SetActive(true);
		ButtonQuit.SetActive(true);
		ButtonArena.SetActive(true);
		ButtonCredits.SetActive(true);
	}
   
   public void StartGame () {
      SceneManager.LoadScene("Level1");   
   }

   public void StartArena(){
	   SceneManager.LoadScene("Arena");
   }

   public void DisplayCredits(){
	   CreditsImage.enabled = true;
	   ButtonCreditsQuit.SetActive(true);
   }
   public void HideCredits(){
	   CreditsImage.enabled = false;
	   ButtonCreditsQuit.SetActive(false);
   }
   
   public void QuitGame() {
      // Quit the application
      Application.Quit();
   }
}
