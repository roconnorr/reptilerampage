﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{

    public bool canMove;

    public bool canShoot;

    public bool gameOver;
    public float speed;
	public Sprite deadSprite;
	private bool isDead;
	private SpriteRenderer sr;
    public static int playerMaxHP = 100;

	private AudioSource soundSource;
	public AudioClip[] footstepSounds;
	private bool footstepSoundEnabled;
    public int health;

    public ParticleSystem bloodParticles;
	public Transform grenadePrefab;
    public Transform crossHairPrefab;
    private Transform crossHair;
	private bool crossHairEnabled;
    private Rigidbody2D rb;
    private TrikeFight trikefightscript = null;
    private TRexFight trexfightscript = null;

	public enum WeaponType {acr, ak47, aug, barret50cal, crossbow, deserteagle, g18, 
                            golddeserteagle, grenade, m1, m16, m1911, miniuzi, 
                            model1887, mp5, p90, remington870, skorpion, spas12, 
                            thompson1928, ump45, usp45, rpg7, chinalake, plasmarifle, 
							empty, bazooka, bow, flamethrower, minigun};
    public GameObject[] weaponsprefabs;
    private GameObject[] weaponslist;

    public WeaponType startWeapon1Type;
	public WeaponType startWeapon2Type;
	public WeaponType startWeapon3Type;
	public GameObject[] slot = new GameObject[3] {null, null, null};
    public WeaponType slot1type;
	public WeaponType slot2type;
	public WeaponType slot3type;
	public int grenadeCount;
	public int slotActive = 2;

	private Vector3 knockback;
	private int knockbackTimer = 0;
	[HideInInspector]
    public bool isInvulnerable;
    public int invulnerableTime = 1;
	[HideInInspector]
	public bool dialogInvulnerable;
    private float horizontal;
    private float vertical;
	private Transform shadow;
	
	public AudioClip playerHitClip;
	public AudioClip noBulletSound;
	public static Scene scene;

	void Start(){
		canShoot = true;
		canMove = true;
		sr = GetComponent<SpriteRenderer> ();
		shadow = transform.Find ("Shadow");
		shadow.GetComponent<SpriteRenderer>().enabled = true;
        if(GameMaster.level1Checkpoint){
            transform.position = new Vector3(45, 46, -1);
        } else if(GameMaster.level2Checkpoint){
            transform.position = new Vector3(40, -40, -1);
        }
        rb = GetComponent<Rigidbody2D>();
        gameOver = false;
        soundSource = gameObject.GetComponent<AudioSource>();

        startWeapon1Type = GameMaster.slot1type;
		startWeapon2Type = GameMaster.slot2type;
		if(GameMaster.currentLevel == 1) {
			startWeapon3Type = WeaponType.m1911;
		}
		if(GameMaster.currentLevel == 2) {
			startWeapon3Type = WeaponType.usp45;
		}
		if(GameMaster.currentLevel == 3) {
			if (GameMaster.levelStartSlot3Type == WeaponType.golddeserteagle) {
				startWeapon3Type = GameMaster.levelStartSlot3Type;
			} else {
				startWeapon3Type = WeaponType.deserteagle;
			}
		}
		grenadeCount = GameMaster.grenadeCount;
		GameMaster.levelStartSlot1Type = startWeapon1Type;
		GameMaster.levelStartSlot2Type = startWeapon2Type;
        if(startWeapon1Type != WeaponType.empty){
            slot[0] = Instantiate(weaponsprefabs[(int)startWeapon1Type], transform.position + weaponsprefabs[(int)startWeapon1Type].transform.position, new Quaternion(0,0,0,0), this.transform);
            slot1type = startWeapon1Type;
        }
        if(startWeapon2Type != WeaponType.empty){
			slot[1] = Instantiate(weaponsprefabs[(int)startWeapon2Type], transform.position + weaponsprefabs[(int)startWeapon2Type].transform.position, new Quaternion(0,0,0,0), this.transform);
            slot2type = startWeapon2Type;
        }
		slot[2] = Instantiate(weaponsprefabs[(int)startWeapon3Type], transform.position + weaponsprefabs[(int)startWeapon3Type].transform.position, new Quaternion(0,0,0,0), this.transform);
		slot3type = startWeapon3Type;
		if(slot[0] != null){
			slot[0].GetComponent<Weapon>().ammo = GameMaster.slot1ammo;
			GameMaster.levelStartSlot1Ammo = slot[0].GetComponent<Weapon>().ammo;
        }
		if(slot[1] != null){
			slot[1].GetComponent<Weapon>().ammo = GameMaster.slot2ammo;
			GameMaster.levelStartSlot2Ammo = slot[1].GetComponent<Weapon>().ammo;
        }

		//need to get current level and load relevant script
		scene = SceneManager.GetActiveScene();
        if(scene.name == "Level1"){
            trikefightscript = GameObject.Find("BossTrigger1").GetComponent<TrikeFight>();
			if(!WayPoints.arrived){
				isDead = true;
				canMove = false;
            	canShoot = false;
				foreach (Renderer r in GetComponentsInChildren<Renderer>()){
					r.enabled = false;
				}
			}
        }else if (scene.name == "Level2"){
            trexfightscript = GameObject.Find("BossTrigger2").GetComponent<TRexFight>();
        }//else gavin
    }

    void OnCollisionEnter2D(Collision2D other){
        rb.velocity = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "BossFight1"){
            trikefightscript.StartFight();
        }
        if (other.tag == "BossFight2"){
            trexfightscript.StartFight();
        }
        if (other.tag == "BossFight3"){
            SceneManager.LoadScene("FinalBoss");
        }
    }

    void FixedUpdate(){
        if (canMove){

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(horizontal, vertical);

            rb.AddForce(movement * speed / Time.deltaTime);
            //rb.velocity = movement * speed;

            if (rb.velocity.magnitude > speed){
                rb.velocity = rb.velocity.normalized * speed;
            }
			//Knockback
			if (knockbackTimer > 0) {
				rb.AddForce (knockback);
				knockbackTimer -= 1;
			}
        }
        else{
            rb.velocity = Vector3.zero;
        }
    }

    void Update(){
		if(scene.name == "Level1"){
			if(WayPoints.arrived && !crossHairEnabled){
				crossHair = Instantiate(crossHairPrefab, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), transform.rotation) as Transform;
				foreach (Renderer r in GetComponentsInChildren<Renderer>()){
					r.enabled = true;
				}
				isDead = false;
				canMove = true;
				canShoot = true;
				crossHairEnabled = true;
			}
		} else if(!crossHairEnabled){
			crossHair = Instantiate(crossHairPrefab, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), transform.rotation) as Transform;
			crossHairEnabled = true;
		}

		if (!isDead) {
			if(!footstepSoundEnabled){
				StartCoroutine(cycleFootsteps());
				footstepSoundEnabled = true;
			}
			GameMaster.slot1type = slot1type;
			GameMaster.slot2type = slot2type;
			GameMaster.slot3type = slot3type;
			GameMaster.grenadeCount = grenadeCount;
			if (slot [0] != null) {
				GameMaster.slot1ammo = slot [0].GetComponent<Weapon> ().ammo;
				GameMaster.slot1MaxAmmo = slot [0].GetComponent<Weapon> ().maxAmmo;
			}
			if (slot [1] != null) {
				GameMaster.slot2ammo = slot [1].GetComponent<Weapon> ().ammo;
				GameMaster.slot2MaxAmmo = slot [1].GetComponent<Weapon> ().maxAmmo;
			}
			if (Input.GetButtonDown ("Grenade") || Input.GetButtonDown("AltGrenade")) {
				Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
				difference.Normalize ();
				float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
				if (grenadeCount > 0) {
					GameMaster.CreateGrenade (grenadePrefab, transform.position, rotZ - 90, 100, 20, 70, false, true);
					grenadeCount--;
				} else {
					//play no bullet sound
					PickUpLog.noGrenadeLog = true;
				}
			}
			weaponslist = GameObject.FindGameObjectsWithTag ("Pickup");

			float minDist = Mathf.Infinity;
			GameObject closestWeapon = null;
			foreach (GameObject weapon in weaponslist) {
				float dist = Vector3.Distance (transform.position, weapon.transform.position);
				if (dist < minDist) {
					minDist = dist;
					closestWeapon = weapon;
					
				}
			}
			foreach (GameObject weapon in weaponslist) {
				weapon.GetComponent<PickupPrefab>().HideStars ();
			}
			if(closestWeapon != null){
				PickupPrefab pk = closestWeapon.GetComponent<PickupPrefab>();
				if (minDist < 2) {
					pk.DisplayStars();
					if (Input.GetButtonDown ("Pickup") && pk.type != WeaponType.golddeserteagle) {
						ChangeWeapon (pk.type, pk, closestWeapon);
						PlayHitSound(noBulletSound, this.transform.position);
					}
				}
			}			

			if (Input.GetButtonDown ("SwapSlot") || Input.GetAxis ("Mouse ScrollWheel") < 0) {
				PlayHitSound(noBulletSound, this.transform.position);
				slotActive = (slotActive + 1) % 3;
				while (slot [slotActive] == null) {
					slotActive = (slotActive + 1) % 3;
				}
			}
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				PlayHitSound(noBulletSound, this.transform.position);
				slotActive = (slotActive + 2) % 3;
				while (slot [slotActive] == null) {
					slotActive = (slotActive + 2) % 3;
				}
			}

			if (Input.GetButtonDown ("Slot1") && slot [0] != null) {
				PlayHitSound(noBulletSound, this.transform.position);
				slotActive = 0;
			}
			if (Input.GetButtonDown ("Slot2") && slot [1] != null) {
				PlayHitSound(noBulletSound, this.transform.position);
				slotActive = 1;
			}
			if (Input.GetButtonDown ("Slot3")) {
				PlayHitSound(noBulletSound, this.transform.position);
				slotActive = 2;
			}

			slot [slotActive].SetActive (true);
			if (slot [(slotActive + 1) % 3] != null) {
				slot [(slotActive + 1) % 3].SetActive (false);
			}
			if (slot [(slotActive + 2) % 3] != null) {
				slot [(slotActive + 2) % 3].SetActive (false);
			}

			if (!isInvulnerable) {
				foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
					if (r.gameObject.tag != "MuzzelFlash") {
						Color c = r.material.color;
						c.a = 1f;
						r.material.color = c;
					}
				}
			} else {
				foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
					if (r.gameObject.tag != "MuzzelFlash") {
						Color c = r.material.color;
						c.a = 0.3f;
						r.material.color = c;
					}
				}
			}
		}

		if(crossHairEnabled){
			crossHair.position = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		}
		if (Time.timeScale != 0 && !gameOver) {
			Cursor.visible = false;
		} else if (Time.timeScale == 0 || gameOver) {
			Cursor.visible = true;
		}
    }

    private IEnumerator cycleFootsteps(){
		while (true) {
			soundSource.clip = footstepSounds [Random.Range (0, 4)];
			if (Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) {
				if (!soundSource.isPlaying) {
					soundSource.Play ();
				}
			} else {
				soundSource.Stop ();
			}
			yield return new WaitForSeconds (soundSource.clip.length);
		}
    }

	public void TakeDamage(int amount, Quaternion dir, float force, Transform source){
		if (!isDead) {
			if (!isInvulnerable && !dialogInvulnerable) {
				PlayHitSound(playerHitClip, this.transform.position);
				health -= amount;
				FireBloodParticles (dir);
				StartCoroutine (becomeInvulnerable ());
			}

			//Knockback
			knockback = dir * Vector2.up;
			knockback *= force;
			knockbackTimer = 10;

			if (health <= 0) {
				GameObject.Find ("Canvas").GetComponent<HUDManager> ().inBossFight = false;
				sr.enabled = true;
				GetComponentInChildren<UpperBodyAnim> ().GetComponent<SpriteRenderer> ().enabled = false;
				GetComponentInChildren<LowerBodyAnim> ().GetComponent<SpriteRenderer> ().enabled = false;
				foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
					Color c = r.material.color;
					c.a = 1f;
					r.material.color = c;
				}
				shadow.GetComponent<SpriteRenderer>().enabled = false;
				sr.sprite = deadSprite;
				isDead = true;
				canMove = false;
				canShoot = false;
				isInvulnerable = false;
				dialogInvulnerable = false;
				GetComponent<Rigidbody2D> ().isKinematic = true;
				slot [slotActive].SetActive (false);
				gameOver = true;
			}
		}
    }

    IEnumerator becomeInvulnerable() {
    	isInvulnerable = true;
    	yield return new WaitForSeconds(invulnerableTime);
    	isInvulnerable = false;
 	}

    public void FireBloodParticles(Quaternion dir){
		Quaternion particleDir = Quaternion.Euler(dir.eulerAngles.z - 90, -90, -5);
		ParticleSystem localBloodParticles = Instantiate(bloodParticles, this.transform.position, particleDir) as ParticleSystem;
		localBloodParticles.Play();
	}

	public static void PlayHitSound(AudioClip clip, Vector3 pos){
		GameObject temp = new GameObject("TempAudio");
		temp.transform.position = pos;
		AudioSource tempSource = temp.AddComponent<AudioSource>();
		tempSource.clip = clip;
		tempSource.volume = 0.15f;
		tempSource.Play();
		Destroy(temp, clip.length);
	}
	

   public void ChangeWeapon(WeaponType type, PickupPrefab pickup, GameObject pickupObject){
		int pickupAmmo = pickup.ammo;
        GameObject.Find("Canvas").GetComponent<HUDManager>().ResetStars();
		if (slot [0] != null && slot [1] != null) {
			if (slotActive == 0) {
				pickup.ChangeType (slot1type, slot [0].GetComponent<Weapon> ().ammo);
				Destroy (slot [0]);  
				slot [0] = Instantiate (weaponsprefabs [(int)type], transform.position + weaponsprefabs [(int)type].transform.position, new Quaternion (0, 0, 0, 0), this.transform);
				slot1type = type;
				if (pickupAmmo != -1) {
					slot [0].GetComponent<Weapon> ().ammo = pickupAmmo;
				}
			}
			if (slotActive == 1) {
				pickup.ChangeType (slot2type, slot [1].GetComponent<Weapon> ().ammo);
				Destroy (slot [1]);  
				slot [1] = Instantiate (weaponsprefabs [(int)type], transform.position + weaponsprefabs [(int)type].transform.position, new Quaternion (0, 0, 0, 0), this.transform);
				slot2type = type;
				if (pickupAmmo != -1) {
					slot [1].GetComponent<Weapon> ().ammo = pickupAmmo;
				}
			}
			if (slotActive == 2) {
				pickup.ChangeType (slot1type, slot [0].GetComponent<Weapon> ().ammo);
				Destroy (slot [0]);  
				slot [0] = Instantiate (weaponsprefabs [(int)type], transform.position + weaponsprefabs [(int)type].transform.position, new Quaternion (0, 0, 0, 0), this.transform);
				slot1type = type;
				if (pickupAmmo != -1) {
					slot [0].GetComponent<Weapon> ().ammo = pickupAmmo;
				}
				slotActive = 0;
			}
		} else if (slot [0] == null) {
			slot [0] = Instantiate(weaponsprefabs[(int)type], transform.position + weaponsprefabs[(int)type].transform.position, new Quaternion(0,0,0,0), this.transform);
			slot1type = type;
			Destroy(pickupObject);
			if (pickupAmmo != -1) {
				slot [0].GetComponent<Weapon> ().ammo = pickupAmmo;
			}
			slotActive = 0;
		} else if (slot [1] == null) {
			slot [1] = Instantiate(weaponsprefabs[(int)type], transform.position + weaponsprefabs[(int)type].transform.position, new Quaternion(0,0,0,0), this.transform);
			slot2type = type;
			Destroy(pickupObject);
			if (pickupAmmo != -1) {
				slot [1].GetComponent<Weapon> ().ammo = pickupAmmo;
			}
			slotActive = 1;
		}
    }
}