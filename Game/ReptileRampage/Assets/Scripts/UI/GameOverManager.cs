using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	public void Hide() {
		gameObject.SetActive(false);
	}

	public void GameOver(){
		gameObject.SetActive(true);
        Cursor.visible = true;
	}

	public void Restart(){
		GameMaster.slot1type = GameMaster.levelStartSlot1Type;
		GameMaster.slot2type = GameMaster.levelStartSlot2Type;
		GameMaster.slot1ammo = GameMaster.levelStartSlot1Ammo;
		GameMaster.slot2ammo = GameMaster.levelStartSlot2Ammo;
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit(){
		SceneManager.LoadScene("TitleScreen");
	}
}
