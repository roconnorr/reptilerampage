﻿using UnityEngine;


public class TrikeFight : MonoBehaviour {
	public GameObject trike;
	private GameObject trikeInstance;
	public GameObject player;
	public GameObject DialogBox;
	private GameObject canvas;
	private HUDManager hudManager;
	public static bool atTrikeDialog;

	public void StartFight(){
		atTrikeDialog = true;
		DialogBox.GetComponentInParent<TextBoxManager>().dialogActive = true;
		DialogBox.GetComponentInParent<TextBoxManager>().inBossFight = true;
		DialogBox.GetComponentInParent<TextBoxManager>().SetDialogNumber(2, 1);
	}

	public void SpawnTrike(){
		trikeInstance = Instantiate(trike, new Vector3(12, 45, -1), new Quaternion(0,0,0,0));
		trikeInstance.GetComponent<Trike>().target = player.transform;
		canvas = GameObject.Find("Canvas");
		hudManager = canvas.GetComponent<HUDManager>();
		hudManager.levelBoss = trikeInstance;
		hudManager.inBossFight = true;
		hudManager.SetBossHealthActive(true);
		GameMaster.level1Checkpoint = true;
		this.gameObject.SetActive(false);
	}
}
