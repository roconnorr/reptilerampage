using UnityEngine;

public class Enemy : MonoBehaviour {

	[HideInInspector]
	public Rigidbody2D rb;
	
	public int health;
	public bool isTrike;
	public bool isTRex;
	public bool isGavin;
	public float knockbackModifier;
	public int meleeDamage = 10;
	public AudioClip deathRoar;
	[HideInInspector]
	public bool hasSeen = false;
	public Sprite[] bloodSplatters;
	public GameObject bloodPrefab;

	private GameObject canvas;
	
	public ParticleSystem bloodParticles;
	public ParticleSystem deathParticles;

	public GameObject bossDeathPrefab;

	private Vector3 knockback;
	private int knockbackTimer = 0;

	private GameObject player;
	public GameObject deadEnemyPrefab;
	public bool noFlip;

	public bool arenaMode;

	private HUDManager hudManager;
	
	public AudioClip[] enemyHitSounds;
	public AudioClip[] trexArmourSounds;


	//public ParticleSystem dustParticles;
	//private Quaternion dustRotation;

	void Start() {
		//dustRotation = Quaternion.Euler(dir.eulerAngles.z - 90, -90, -5);
		//ParticleSystem localDustParticles = Instantiate(dustParticles, transform.position, dustRotation, transform) as ParticleSystem;
		//localDustParticles.Play();
		rb = GetComponent<Rigidbody2D>();
		canvas = GameObject.Find("Canvas");
		hudManager = canvas.GetComponent<HUDManager>();
		player = GameObject.FindWithTag("Player");
	}
	void FixedUpdate() {
		//Knockback
		if (knockbackTimer > 0) {
			rb.AddForce (knockback);
			knockbackTimer -= 1;
		}
	}

	public void TakeDamage(int amount, Quaternion dir, float force, Transform source, bool isExplosion, bool isFire) {
		knockback = dir * Vector2.up;
		knockback *= (force / 2);
		knockback *= knockbackModifier;
		knockbackTimer = 3;
		if(!isTRex && !isFire){
			PlayHitSound(enemyHitSounds[Random.Range(0,enemyHitSounds.Length)], this.transform.position);
		}
		if (isTRex && isExplosion) {
			GetComponent<TRex> ().defencesDown = true;
			GetComponent<TRex> ().defenceTimer = 200;
		}
		if (!isFire) {
			SplatterBlood (1);
		}
		if (isTRex && !GetComponent<TRex>().defencesDown && !isFire) {
			//don't take damage and play an armor sound
			PlayHitSound (trexArmourSounds [Random.Range (0, trexArmourSounds.Length)], this.transform.position);
		} else {
			if(isTRex && !isFire){
				PlayHitSound(enemyHitSounds[Random.Range(0,enemyHitSounds.Length)], this.transform.position);
			}
			if (!isFire) {
				FireBloodParticles (dir);
			}
			health -= amount;
			if (health <= 0) {
				AudioSource.PlayClipAtPoint (deathRoar, transform.position);
				if (isTrike) {
					//possibly some more dialogue
					
					player.GetComponent<PlayDialog>().ActivateAfterTrikeDialog();
					hudManager.SetBossHealthActive (false);
					hudManager.inBossFight = false;
					if (arenaMode) {
						hudManager.arenaTrikeAlive = false;
					}
					
					GameMaster.currentLevel = 2;
					GameMaster.level1Checkpoint = false;

					//SceneManager.LoadScene("Level2");
				} else if (isTRex) {
					//possibly some more dialogue
					hudManager.SetBossHealthActive (false);
					hudManager.inBossFight = false;
					if (arenaMode) {
						hudManager.arenaTrexAlive = false;
					}
					GameMaster.currentLevel = 3;
					GameMaster.level2Checkpoint = false;
				} else if (isGavin) {
					MusicPlayer.won = true;
					hudManager.inBossFight = false;
					TextBoxManager.lastDialog = true;
					//also loads last scene ehe
					player.GetComponent<PlayDialog>().PlayAfterGavinDialog();
					GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
					foreach (GameObject enemy in enemies) {
						if (gameObject != enemy) {
							Destroy (enemy);
						}
					}
				}
				if(deadEnemyPrefab != null){
					GameObject deadBody = Instantiate (deadEnemyPrefab, transform.position, transform.rotation);
					if (noFlip) {
						deadBody.transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
					}
					Destroy (deadBody, 120f);
				}
				if (arenaMode) {
					GameObject.Find("WaveMaster").GetComponent<WaveMaster>().enemiesAlive--;
				}
				ParticleSystem localDeathParticles = Instantiate (deathParticles, this.transform.position, transform.localRotation) as ParticleSystem;
				localDeathParticles.Play ();
				SplatterBlood (4);
				if (bossDeathPrefab != null) {
					Instantiate (bossDeathPrefab, transform.position, Quaternion.Euler (0, 0, 0));
				}
				Destroy (gameObject);
			}
		}
		if (source != null && source.gameObject == player) {
			hasSeen = true;
		}
	}

	/*Vector2 GetXYDirection(float angle, float magnitude){
    	return Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * magnitude;
	}*/

	public void FireBloodParticles(Quaternion dir){
		float randX = Random.Range (-0.5f, 0.5f);
		float randY = Random.Range (-0.5f, 0.5f);
		Quaternion particleDir = Quaternion.Euler(dir.eulerAngles.z - 90, -90, -5);
		ParticleSystem localBloodParticles = Instantiate(bloodParticles, new Vector3(transform.position.x + randX, transform.position.y + randY), particleDir) as ParticleSystem;
		localBloodParticles.Play();
	}

	public void SplatterBlood(int amount) {
		for (int i = 0; i < amount; i++) {
			float randX = Random.Range (-0.5f, 0.5f);
			float randY = Random.Range (-0.5f, 0.5f);
			GameObject blood = Instantiate (bloodPrefab, new Vector3(transform.position.x + randX, transform.position.y + randY), transform.localRotation);
			blood.GetComponent<SpriteRenderer> ().sprite = bloodSplatters [Random.Range (0, 6)];
			Destroy (blood, 120f);
		}
	}

	void OnCollisionStay2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			float angle =Mathf.Atan2(other.transform.position.y-transform.position.y, other.transform.position.x-transform.position.x)*180 / Mathf.PI;
			angle -= 90;
			other.gameObject.GetComponent<Player>().TakeDamage (meleeDamage, Quaternion.Euler(0, 0, angle), 500, transform);
		}
	}
	
	public static void PlayHitSound(AudioClip clip, Vector3 pos){
		GameObject temp = new GameObject("TempAudio");
		temp.transform.position = pos;
		AudioSource tempSource = temp.AddComponent<AudioSource>();
		tempSource.clip = clip;
		tempSource.volume = 0.15f;
		if(!tempSource.isPlaying){
			tempSource.Play();
		}
		Destroy(temp, clip.length);
	}

}
