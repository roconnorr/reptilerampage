using UnityEngine;


public class TrikeFight : MonoBehaviour {
	public GameObject trike;
	private GameObject trikeInstance;
	public GameObject player;
	public GameObject DialogBox;
	private GameObject canvas;
	private HUDManager hudManager;

	public void StartFight(){
		DialogBox.GetComponentInParent<TextBoxManager>().dialogActive = true;
	}

	public void SpawnTrike(){
		trikeInstance = Instantiate(trike, new Vector3(12, 47, -1), new Quaternion(0,0,0,0));
		trikeInstance.GetComponent<Trike>().target = player.transform;
		canvas = GameObject.Find("Canvas");
		hudManager = canvas.GetComponent<HUDManager>();
		hudManager.levelBoss = trikeInstance;
		hudManager.inBossFight = true;
		hudManager.SetBossHealthActive(true);
		this.gameObject.SetActive(false);
	}
}
