﻿using UnityEngine;

public class PickupPrefab : MonoBehaviour {

	public Player.WeaponType type;

	public Sprite[] WeaponSprites;

	private SpriteRenderer spriteRenderer; 

	public int ammo = -1;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = WeaponSprites[(int)type];
	}

	public void ChangeType(Player.WeaponType newType, int ammoCount){
		ammo = ammoCount;
		type = newType;
		spriteRenderer.sprite = WeaponSprites[(int)type];
	}
}
