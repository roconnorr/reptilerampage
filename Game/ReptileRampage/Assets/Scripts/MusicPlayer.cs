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
	public static bool fadeToBoss = false;
	public static bool fadeToLevel = false;

	void Start () {
		musicPlayer = GetComponent<AudioSource>();
		hudManager = canvas.GetComponent<HUDManager>();
		musicPlayer.loop = true;
		if(player != null){	
			playerScript = player.GetComponent<Player>();
		}
		if (titleScreen) {
			musicPlayer.clip = levelMusic;
			musicPlayer.Play ();
		} else {
			bossMusicPlayed = false;
			levelMusicPlayed = true;
			musicPlayer.volume = 0.5f;
		}
	}
	
	void Update () {
		if (fadeToBoss) {
			if (musicPlayer.volume > 0.05) { 
				musicPlayer.volume *= 0.95f;
			} else {
				musicPlayer.clip = bossMusic;
				musicPlayer.Play();
				bossMusicPlayed = true;
				levelMusicPlayed = false;
				fadeToBoss = false;
				musicPlayer.volume = 0.5f;
			}
		}
		if (fadeToLevel) {
			if (musicPlayer.volume > 0.05) { 
				musicPlayer.volume *= 0.95f;
			} else {
				musicPlayer.clip = levelMusic;
				musicPlayer.Play();
				bossMusicPlayed = false;
				levelMusicPlayed = true;
				fadeToLevel = false;
				musicPlayer.volume = 0.5f;
			}
		}
		if(!titleScreen){
			if(playerScript.gameOver || won){
				musicPlayer.loop = false;
			}
			if(hudManager.inBossFight && !bossMusicPlayed && !fadeToBoss){
				fadeToBoss = true;
			}else if(!hudManager.inBossFight && !levelMusicPlayed && !fadeToLevel){
				fadeToLevel = true;
			}
		}
	}
}
