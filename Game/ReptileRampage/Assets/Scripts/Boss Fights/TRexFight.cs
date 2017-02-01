using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TRexFight : MonoBehaviour {
	public GameObject trex;
	private GameObject trexInstance;
	public GameObject player;

	public void StartFight(){
		player.GetComponent<Player>().canMove = false;
		this.gameObject.GetComponent<TextBoxManager>().dialogActive = true;
	}

	public void SpawnTRex(){
		player.GetComponent<Player>().canMove = true;
		trexInstance = Instantiate(trex, new Vector3(12, 47, -1), new Quaternion(0,0,0,0));
		trexInstance.GetComponent<TRex>().target = player.transform;
		this.gameObject.SetActive(false);
	}
}
