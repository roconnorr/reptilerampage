using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public int spawnerID;
	public GameObject Anklyosaurus;
	public GameObject Pteradactyl;
	public GameObject Stegosaurus;
	public GameObject Velociraptor;
	public GameObject Trike;
	public GameObject Trex;

	public GameObject waveMasterObject;

	public GameObject canvas;
	private WaveMaster waveMaster;
	public GameObject grid;
	public GameObject player;


	private HUDManager hudManager;


	// Use this for initialization
	void Start () {
		//waveMaster = waveMasterObject.GetComponent<WaveMaster>();
		hudManager =  canvas.GetComponent<HUDManager>();
	}

	public void Spawn(string type, int quantity){
		waveMaster = waveMasterObject.GetComponent<WaveMaster>();
		//array of gameobjects instantiate each index
		if(type == "Anklyosaurus"){
			GameObject[] anklyArray;
			anklyArray = new GameObject[quantity];
			for(int i = 0; i<quantity; i++){
				anklyArray[i] = Instantiate(Anklyosaurus, new Vector3(this.transform.position.x + Random.Range(0, 0.5f),this.transform.position.y + Random.Range(0, 0.5f), 0), new Quaternion(0,0,0,0), this.transform);
				anklyArray[i].GetComponent<Enemy>().arenaMode = true;
				anklyArray[i].GetComponent<Enemy>().hasSeen = true;
				anklyArray[i].GetComponent<AStarPathfinder>().gridObject = grid;
				anklyArray[i].GetComponent<Ankylosaurus>().target = player;
				waveMaster.enemiesAlive++;
			}
		}else if(type == "Pteradactyl"){
			GameObject[] pterArray;
			pterArray = new GameObject[quantity];
			for(int i = 0; i<quantity; i++){
				pterArray[i] = Instantiate(Pteradactyl, new Vector3(this.transform.position.x + Random.Range(0, 0.5f),this.transform.position.y + Random.Range(0, 0.5f), 0), new Quaternion(0,0,0,0), this.transform);
				pterArray[i].GetComponent<Enemy>().arenaMode = true;
				pterArray[i].GetComponent<Enemy>().hasSeen = true;
				pterArray[i].GetComponent<Pteradactyl>().target = player.transform;
				waveMaster.enemiesAlive++;
			}
		}else if(type == "Stegosaurus"){
			GameObject[] stegoArray;
			stegoArray = new GameObject[quantity];
			for(int i = 0; i<quantity; i++){
				stegoArray[i] = Instantiate(Stegosaurus, new Vector3(this.transform.position.x + Random.Range(0, 0.5f),this.transform.position.y + Random.Range(0, 0.5f), 0), new Quaternion(0,0,0,0), this.transform);
				stegoArray[i].GetComponent<Enemy>().arenaMode = true;
				stegoArray[i].GetComponent<Enemy>().hasSeen = true;
				stegoArray[i].GetComponent<AStarPathfinder>().gridObject = grid;
				stegoArray[i].GetComponent<Stegosaurus>().target = player;
				stegoArray[i].GetComponentInChildren<StegoTurret>().target = player;
				waveMaster.enemiesAlive++;
			}
		}else if(type == "Velociraptor"){
			GameObject[] raptorArray;
			raptorArray = new GameObject[quantity];
			for(int i = 0; i<quantity; i++){
				raptorArray[i] = Instantiate(Velociraptor, new Vector3(this.transform.position.x + Random.Range(0, 0.5f),this.transform.position.y + Random.Range(0, 0.5f), 0), new Quaternion(0,0,0,0), this.transform);
				raptorArray[i].GetComponent<Enemy>().arenaMode = true;
				raptorArray[i].GetComponent<Enemy>().hasSeen = true;
				raptorArray[i].GetComponent<AStarPathfinder>().gridObject = grid;
				raptorArray[i].GetComponent<Velociraptor>().target = player;
				waveMaster.enemiesAlive++;
			}
		}else if(type == "Trike"){
			GameObject[] trikeArray;
			trikeArray = new GameObject[quantity];
			for(int i = 0; i<quantity; i++){
				trikeArray[i] = Instantiate(Trike, new Vector3(this.transform.position.x + Random.Range(0, 0.5f),this.transform.position.y + Random.Range(0, 0.5f), 0), new Quaternion(0,0,0,0), this.transform);
				trikeArray[i].GetComponent<Enemy>().arenaMode = true;
				trikeArray[i].GetComponent<Trike>().target = player.transform;
				hudManager.arenaTrikeInstance = trikeArray[i];
				hudManager.arenaTrikeAlive = true;
				hudManager.arenaTrikeAlive = true;
				waveMaster.enemiesAlive++;
			}
		}else if(type == "Trex"){
			GameObject[] trexArray;
			trexArray = new GameObject[quantity];
			for(int i = 0; i<quantity; i++){
				trexArray[i] = Instantiate(Trex, new Vector3(this.transform.position.x + Random.Range(0, 0.5f),this.transform.position.y + Random.Range(0, 0.5f), 0), new Quaternion(0,0,0,0), this.transform);
				trexArray[i].GetComponent<Enemy>().arenaMode = true;
				trexArray[i].GetComponent<TRex>().target = player.transform;
				hudManager.arenaTrexInstance = trexArray[i];
				hudManager.arenaTrexAlive = true;
				hudManager.arenaTrexAlive = true;
				waveMaster.enemiesAlive++;
			}
		}
	}
}
