﻿using UnityEngine;


public class TRexFight : MonoBehaviour {
	public GameObject trex;
	private GameObject trexInstance;
	public GameObject player;
	public GameObject DialogBox;

	public void StartFight(){
		DialogBox.GetComponentInParent<TextBoxManager>().dialogActive = true;
	}

	public void SpawnTRex(){
		trexInstance = Instantiate(trex);
		trexInstance.GetComponent<TRex>().target = player.transform;
		this.gameObject.SetActive(false);
	}

}
