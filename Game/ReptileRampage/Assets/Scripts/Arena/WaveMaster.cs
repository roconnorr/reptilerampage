using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaster : MonoBehaviour {

	public int enemiesAlive;
	public GameObject[] spawners;

	public int currentWave;

	public float timer = 0.0f;
	public float timeToNewWave = 10.0f;
	public bool betweenWaves;

	private bool cratesPlaced;

	public GameObject tier1crate;
	public GameObject tier2crate;
	public GameObject tier3crate;

	// Use this for initialization
	void Start () {
		currentWave = 1;
		SpawnWave();
		betweenWaves = false;
		SpawnCrates(1);
	}
	
	// Update is called once per frame
	void Update () {
		if(enemiesAlive == 0){
			if(!cratesPlaced){
				SpawnCrates(1);
				cratesPlaced = true;
			}
			timer += Time.deltaTime;
			betweenWaves = true;
			if(timer > timeToNewWave){
				currentWave++;
				betweenWaves = false;
				cratesPlaced = false;
				timer = 0;
				SpawnWave();
			}
		}
	}

	void SpawnWave(){
		if(currentWave < 5){
			spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", currentWave);
			spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", currentWave);
			spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", currentWave);
			spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", currentWave);
			//Spawn(Anklyosaurus, 1);
			//Spawn(Pteradactyl, 1);
			//Spawn(Stegosaurus, 1);
			//Spawn(Velociraptor, 1);
			//Spawn(Trike, 1);
			//Spawn(Trex, 1);
		}else if(currentWave == 5){
			spawners[3].GetComponent<EnemySpawner>().Spawn("Trike", 1);
		}else if(currentWave > 5 && currentWave < 10){
		}else if(currentWave == 10){
		}
	}
	public void SpawnCrates(int level){
		Instantiate(tier1crate, new Vector3(-10, 10, -2), new Quaternion(0,0,0,0));
		Instantiate(tier1crate, new Vector3(10, 10, -2), new Quaternion(0,0,0,0));
		Instantiate(tier1crate, new Vector3(10, -10, -2), new Quaternion(0,0,0,0));
		Instantiate(tier1crate, new Vector3(-10, -10, -2), new Quaternion(0,0,0,0));
	}

}
