using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject explosionScriptPrefab;
	public ParticleSystem bloodParticles;
	public ParticleSystem deathParticles;
	public Sprite[] bloodSplatters;
	public GameObject bloodPrefab;

	public int timeToLive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeToLive--;
		if (timeToLive == 0) {
			Destroy (gameObject);
		}
		int num = Random.Range (0, 5);
		int randX = Random.Range (-2, 2);
		int randY = Random.Range (-2, 2);
		if (num == 1) {
			GameMaster.CreateExplosion (explosionPrefab, explosionScriptPrefab, new Vector3 (transform.position.x + randX, transform.position.y + randY), 0, 0, 0, false);
		}
		if (num == 2) {
			ParticleSystem localDeathParticles = Instantiate (deathParticles, this.transform.position, transform.localRotation) as ParticleSystem;
			localDeathParticles.Play ();
		}
		if (num == 3) { 
			FireBloodParticles(Quaternion.Euler (0, 0, Random.Range (0, 360)));
			SplatterBlood (5);
		}
	}

	public void SplatterBlood(int amount) {
		for (int i = 0; i < amount; i++) {
			float randX = Random.Range (-2f, 2f);
			float randY = Random.Range (-2f, 2f);
			GameObject blood = Instantiate (bloodPrefab, new Vector3(transform.position.x + randX, transform.position.y + randY), transform.localRotation);
			blood.GetComponent<SpriteRenderer> ().sprite = bloodSplatters [Random.Range (0, 6)];
		}
	}

	public void FireBloodParticles(Quaternion dir){
		Quaternion particleDir = Quaternion.Euler(dir.eulerAngles.z - 90, -90, -5);
		ParticleSystem localBloodParticles = Instantiate(bloodParticles, this.transform.position, particleDir) as ParticleSystem;
		localBloodParticles.Play();
	}
}
