using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	private AudioSource bossMusic;
	private HUDManager hudManager;
	public GameObject canvas;
	void Start () {
		bossMusic = gameObject.GetComponent<AudioSource>();
		hudManager = canvas.GetComponent<HUDManager>();
	}
	
	void Update () {
		if(hudManager.inBossFight){
			bossMusic.enabled = true;
		}else{
			bossMusic.enabled = false;
		}
	}
}
