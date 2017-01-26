﻿using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	
	public int health;

	public AudioClip deathRoar;
	
	public ParticleSystem bloodParticles;
	private GameObject particleRotation;

	void Start() {
		particleRotation = GameObject.Find("ParticleRotation");
	}

	public void TakeDamage(int amount, Quaternion dir) {	
		health -= 1;
		FireBloodParticles(dir);
		if (health <= 0) {
			Destroy (gameObject);
			AudioSource.PlayClipAtPoint (deathRoar, transform.position);
		}
	}

	public void FireBloodParticles(Quaternion dir){
		dir *= Quaternion.Euler(0, 0, -90);
		particleRotation.transform.rotation = dir;
		bloodParticles.Play();
	}
}
