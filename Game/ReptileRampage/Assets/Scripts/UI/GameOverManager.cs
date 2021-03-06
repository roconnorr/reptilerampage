﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	public void Hide() {
		gameObject.SetActive(false);
	}

	public void GameOver(){
		gameObject.SetActive(true);
	}

	public void Restart(){
		if(GameMaster.level1Checkpoint || GameMaster.level2Checkpoint){
			GameMaster.slot1ammo = GameMaster.slot1MaxAmmo;
			GameMaster.slot2ammo = GameMaster.slot2MaxAmmo;
		} else{
			GameMaster.slot1type = GameMaster.levelStartSlot1Type;
			GameMaster.slot2type = GameMaster.levelStartSlot2Type;
			GameMaster.slot1ammo = GameMaster.levelStartSlot1Ammo;
			GameMaster.slot2ammo = GameMaster.levelStartSlot2Ammo;
		}

		if(Player.scene.name == "Level1"){
			WayPoints.respawned = true;
		}
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit(){
		SceneManager.LoadScene("TitleScreen");
	}
}
