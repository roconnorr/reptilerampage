using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	
	public int health;
	public bool isTRex;

	public AudioClip deathRoar;
	
	public ParticleSystem bloodParticles;
	private GameObject particleRotation;

	void Start() {
		
	}

	public void TakeDamage(int amount, Quaternion dir) {
		if (isTRex && !GetComponent<TRex>().defencesDown) {
			//don't take damage
		} else {
			health -= amount;
			FireBloodParticles(dir);
			if (health <= 0) {
				Destroy (gameObject);
				AudioSource.PlayClipAtPoint (deathRoar, transform.position);
			}
		}
	}

	public void FireBloodParticles(Quaternion dir){
		Quaternion particleDir = Quaternion.Euler(dir.eulerAngles.z - 90, -90, -5);
		ParticleSystem localBloodParticles = Instantiate(bloodParticles, this.transform.position, particleDir) as ParticleSystem;
		localBloodParticles.Play();
	}
}
