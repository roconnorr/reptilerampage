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
	private static bool fadeToBoss = false;
	private static bool fadeToLevel = false;
	public static float volume;
	private float defaultMusicVolume = 0.2f;
	public static float masterVolume;
	private float defaultMasterVolume = 1;
	private float prefferedVolume = 0.2f;
	private static bool musicVolumeChangedByPlayer; 
	private static bool masterVolumeChangedByPlayer;
	private bool stopChanging;

	void Start () {
		Debug.Log(masterVolume);
		Debug.Log(volume);
		if(musicVolumeChangedByPlayer){
			stopChanging = true;
		}
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
			if(!musicVolumeChangedByPlayer){
				Debug.Log("bad");
				musicPlayer.volume = defaultMusicVolume;
			}else if(musicVolumeChangedByPlayer){
				Debug.Log("good");
				musicPlayer.volume = volume;
				stopChanging = false;
			}

			if(!masterVolumeChangedByPlayer){
				AudioListener.volume = defaultMasterVolume;
			}else if(masterVolumeChangedByPlayer){
				AudioListener.volume = masterVolume;
			}
		}
	}
	
	void Update () {
		Debug.Log(stopChanging);
		if(volume != musicPlayer.volume && !stopChanging){
			volume = musicPlayer.volume;
		}
		if(masterVolume != AudioListener.volume){
			masterVolume = AudioListener.volume;
		}

		if (fadeToBoss) {
			if (musicPlayer.volume > 0.05) {
				stopChanging = true;
				musicPlayer.volume *= 0.95f;
			} else {
				stopChanging = false;
				musicPlayer.clip = bossMusic;
				musicPlayer.Play();
				bossMusicPlayed = true;
				levelMusicPlayed = false;
				fadeToBoss = false;
				musicPlayer.volume = volume;
			}
		}
		if (fadeToLevel && !won) {
			if (musicPlayer.volume > 0.05) { 
				stopChanging = true;
				musicPlayer.volume *= 0.95f;
			} else {
				stopChanging = false;
				musicPlayer.clip = levelMusic;
				musicPlayer.Play();
				bossMusicPlayed = false;
				levelMusicPlayed = true;
				fadeToLevel = false;
				musicPlayer.volume = volume;
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
		if(volumeInput != defaultMusicVolume && !musicVolumeChangedByPlayer){
			Debug.Log("music volume changed by user");
			musicVolumeChangedByPlayer = true;
		}
		//prefferedVolume = volumeInput;
		musicPlayer.volume = volumeInput;
	}

	public void MasterVolumeControl(float volumeInput){
		if(volumeInput != defaultMasterVolume && !masterVolumeChangedByPlayer){
			Debug.Log("master volume changed by user");
			masterVolumeChangedByPlayer = true;
		}
		AudioListener.volume = volumeInput;
	}
}
