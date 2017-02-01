using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TRexFight : MonoBehaviour {

	
	public GameObject trex;
	public GameObject player;
	public GameObject textBox;

	public void StartFight(){
		Dialogue();
		//spawn rex
	}

	public void Dialogue(){
		player.GetComponent<Player>().canMove = false;
		textBox.SetActive(true);
		//Instantiate(textBox, this.transform.position, new Quaternion(0,0,0,0));
	}
		
}
