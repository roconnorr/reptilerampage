using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TRexFight : MonoBehaviour {

	
	public GameObject trex;
	public GameObject trexInstance;
	public GameObject player;
	public GameObject textBox;
	public GameObject textBoxManager;

	public void StartFight(){
		Dialogue();
		textBox.SetActive(true);
		textBoxManager.SetActive(true);
		trexInstance = Instantiate(trex, new Vector3(12, 47, -1), new Quaternion(0,0,0,0));
		trexInstance.GetComponent<TRex>().target = player.transform;
	}

	public void Dialogue(){
	}
		
}
