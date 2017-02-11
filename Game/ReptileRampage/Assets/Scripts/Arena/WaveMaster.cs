using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMaster : MonoBehaviour {

	public int enemiesAlive;
	public GameObject[] spawners;

	public int currentWave = 1;

	public int waveIndex = 1;

	public float timer = 0.0f;
	public float timeToNewWave = 10.0f;
	public bool betweenWaves;

	private bool cratesPlaced;

	public GameObject tier1crate;
	public GameObject tier2crate;
	public GameObject tier3crate;

	// Use this for initialization
	void Start () {
		//currentWave = 1;
		//waveIndex = 1;
		SpawnWave();
		betweenWaves = false;
		SpawnCrates();
	}
	
	// Update is called once per frame
	void Update () {
		if(enemiesAlive == 0){
			if(!cratesPlaced){
				SpawnCrates();
				cratesPlaced = true;
			}
			timer += Time.deltaTime;
			betweenWaves = true;
			if(timer > timeToNewWave){
				currentWave++;
				waveIndex++;
				betweenWaves = false;
				cratesPlaced = false;
				timer = 0;
				SpawnWave();
			}
		}
	}

	public void SpawnCrates(){
		if(currentWave < 10){
			Instantiate(tier1crate, new Vector3(-10, 10, -2), new Quaternion(0,0,0,0));
			Instantiate(tier1crate, new Vector3(10, 10, -2), new Quaternion(0,0,0,0));
			Instantiate(tier1crate, new Vector3(10, -10, -2), new Quaternion(0,0,0,0));
			Instantiate(tier1crate, new Vector3(-10, -10, -2), new Quaternion(0,0,0,0));
		}else if(currentWave < 20){
			Instantiate(tier2crate, new Vector3(-10, 10, -2), new Quaternion(0,0,0,0));
			Instantiate(tier2crate, new Vector3(10, 10, -2), new Quaternion(0,0,0,0));
			Instantiate(tier2crate, new Vector3(10, -10, -2), new Quaternion(0,0,0,0));
			Instantiate(tier2crate, new Vector3(-10, -10, -2), new Quaternion(0,0,0,0));
		}else if(currentWave < 30){
			Instantiate(tier3crate, new Vector3(-10, 10, -2), new Quaternion(0,0,0,0));
			Instantiate(tier3crate, new Vector3(10, 10, -2), new Quaternion(0,0,0,0));
			Instantiate(tier3crate, new Vector3(10, -10, -2), new Quaternion(0,0,0,0));
			Instantiate(tier3crate, new Vector3(-10, -10, -2), new Quaternion(0,0,0,0));
		}
	}

	void SpawnWave(){
		switch(waveIndex){
			case 1:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				break;
			case 2:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				break;
			case 3:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 4:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 5:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 6:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 3);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 3);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[0].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 7:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 8:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 9:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				break;
			case 10:
				spawners[3].GetComponent<EnemySpawner>().Spawn("Trike", 1);
				break;
			case 11:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				break;
			case 12:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				break;
			case 13:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				break;
			case 14:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 3);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 3);
				break;
			case 15:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 1);
				break;
			case 16:
				spawners[3].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				break;
			case 17:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 7);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 8);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 8);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 7);
				break;
			case 18:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 19:
				spawners[1].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				break;
			case 20:
				spawners[3].GetComponent<EnemySpawner>().Spawn("Trex", 1);
				break;
			case 21:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				break;
			case 22:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 2);
				spawners[0].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 23:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				break;
			case 24:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 5);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 25:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 10);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 10);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 10);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 10);
				break;
			case 26:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				break;
			case 27:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[0].GetComponent<EnemySpawner>().Spawn("Velociraptor", 3);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Velociraptor", 3);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Velociraptor", 3);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Velociraptor", 3);
				break;
			case 28:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				break;
			case 29:
				spawners[0].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Stegosaurus", 1);
				spawners[0].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[1].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[2].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Anklyosaurus", 1);
				break;
			case 30:
				spawners[1].GetComponent<EnemySpawner>().Spawn("Trike", 1);
				spawners[3].GetComponent<EnemySpawner>().Spawn("Trex", 1);
				break;
			case 31:
				waveIndex = 21;
				goto case 21;
		}
	}

}
