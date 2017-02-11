using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaster : MonoBehaviour {

	public int enemiesAlive;
	public GameObject[] spawners;

	public int currentWave;


	// Use this for initialization
	void Start () {
		currentWave = 1;
		SpawnWave();
	}
	
	// Update is called once per frame
	void Update () {
		if(enemiesAlive == 0){
			IncrementWave();
			SpawnWave();
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
	public void IncrementWave(){
		currentWave++;
		//wait 10, drop new supplies
	}

}
