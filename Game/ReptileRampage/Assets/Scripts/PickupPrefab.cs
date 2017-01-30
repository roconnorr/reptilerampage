using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPrefab : MonoBehaviour {

	public Player.WeaponType type;

	void Start(){
		//set sprite
	}

	void Update(){
		//set sprite
	}

	public void ChangeType(Player.WeaponType newType){
		type = newType;
	}
}
