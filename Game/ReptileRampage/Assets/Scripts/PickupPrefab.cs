using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPrefab : MonoBehaviour {

	public Player.WeaponType type;

	public Sprite[] WeaponSprites;

	private SpriteRenderer spriteRenderer; 


	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = WeaponSprites[(int)type];
	}

	void Update(){
		
	}

	public void ChangeType(Player.WeaponType newType){
		type = newType;
		spriteRenderer.sprite = WeaponSprites[(int)type];
	}
}
