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
	public static float volume = 0.5f;
	public static float masterVolume = 1;
	private float prefferedVolume = 0.5f;

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
			musicPlayer.volume = volume;
			AudioListener.volume = masterVolume;
		}
	}
	
	void Update () {
		if(volume != musicPlayer.volume){
			volume = musicPlayer.volume;
		}
		if(masterVolume != AudioListener.volume){
			masterVolume = AudioListener.volume;
		}

		if (fadeToBoss) {
			if (musicPlayer.volume > 0.05) { 
				musicPlayer.volume *= 0.95f;
			} else {
				musicPlayer.clip = bossMusic;
				musicPlayer.Play();
				bossMusicPlayed = true;
				levelMusicPlayed = false;
				fadeToBoss = false;
				musicPlayer.volume = prefferedVolume;
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
				musicPlayer.volume = prefferedVolume;
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

	public void VolumeControl(float volumeInput){
		prefferedVolume = volumeInput;
		musicPlayer.volume = volumeInput;
	}

	public void MasterVolumeControl(float volumeInput){
		AudioListener.volume = volumeInput;
	}
}
