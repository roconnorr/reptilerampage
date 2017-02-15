using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	private AudioSource musicPlayer;
	public AudioClip levelMusic;
	public AudioClip bossMusic;
	private HUDManager hudManager;
	public GameObject canvas;
	private bool levelMusicPlayed;
	private bool bossMusicPlayed;
	public bool titleScreen;
	private Player playerScript;
	public GameObject player;
	public static bool won;
	void Start () {
		musicPlayer = GetComponent<AudioSource>();
		hudManager = canvas.GetComponent<HUDManager>();
		musicPlayer.loop = true;
		if(player != null){	
			playerScript = player.GetComponent<Player>();
		}
		if(titleScreen){
			musicPlayer.clip = levelMusic;
			musicPlayer.Play();
		}
	}
	
	void Update () {
		if(!titleScreen){
			if(playerScript.gameOver || won){
				musicPlayer.loop = false;
			}
			if(hudManager.inBossFight && !bossMusicPlayed){
				musicPlayer.clip = bossMusic;
				musicPlayer.volume = 0.5f;
				musicPlayer.Play();
				bossMusicPlayed = true;
				levelMusicPlayed = false;
			}else if(!hudManager.inBossFight && !levelMusicPlayed){
				musicPlayer.clip = levelMusic;
				musicPlayer.volume = 0.2f;
				musicPlayer.Play();
				levelMusicPlayed = true;
				bossMusicPlayed = false;
			}
		}
	}
}
