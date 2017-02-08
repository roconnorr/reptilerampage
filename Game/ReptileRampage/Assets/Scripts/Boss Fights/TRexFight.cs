using UnityEngine;


public class TRexFight : MonoBehaviour {
	public GameObject trex;
	private GameObject trexInstance;
	public GameObject player;
	public GameObject DialogBox;
	private GameObject canvas;
	private HUDManager hudManager;

	public void StartFight(){
		DialogBox.GetComponentInParent<TextBoxManager>().dialogActive = true;
	}

	public void SpawnTRex(){
		trexInstance = Instantiate(trex);
		trexInstance.GetComponent<TRex>().target = player.transform;
		canvas = GameObject.Find("Canvas");
		hudManager = canvas.GetComponent<HUDManager>();
		hudManager.levelBoss = trexInstance;
		hudManager.inBossFight = true;
		hudManager.SetBossHealthActive(true);
		GameMaster.level2Checkpoint = true;
		this.gameObject.SetActive(false);
	}
}
