using UnityEngine;

public class TRexFight : MonoBehaviour {

	public GameObject rexfight;

	public GameObject trex;

	public GameObject player;

	public void StartFight(){
		Dialogue();
		//spawn rex
	}

	public void Dialogue(){
		player.GetComponent<Player>().canMove = false;
	}
}
