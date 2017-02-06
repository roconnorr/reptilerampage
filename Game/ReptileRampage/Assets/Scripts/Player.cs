using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{

    public bool canMove;

    public bool canShoot;

    public bool gameOver;
    public float speed;
    public static int playerMaxHP = 100;

    private AudioSource soundSource;
    public AudioClip[] footstepSounds;


    [HideInInspector]
    public int health;

    //public ParticleSystem dustParticles;
    public ParticleSystem bloodParticles;
    public Transform crossHairPrefab;
    private Transform crossHair;
    private Rigidbody2D rb;
    private TrikeFight trikefightscript = null;

    private TRexFight trexfightscript = null;

    //private GavinFight gavinFightScript;


    private Weapon weapon;

	public enum WeaponType {acr, ak47, aug, barret50cal, crossbow, deserteagle, g18, 
                            golddeserteagle, grenade, m1, m16, m1911, miniuzi, 
                            model1887, mp5, p90, remington870, skorpion, spas12, 
                            thompson1928, ump45, usp45, rpg7, chinalake, plasmarifle, empty};
    public GameObject[] weaponsprefabs;
    private GameObject[] weaponslist;

    public WeaponType startWeapon1Type;
    public WeaponType startWeapon2Type;
    public GameObject slot1 = null;
    public GameObject slot2 = null;
    public WeaponType slot1type;
    public WeaponType slot2type;

    private bool slot1active = true;
    public bool intrigger;
    private PickupPrefab pk;

	private Vector3 knockback;
	private int knockbackTimer = 0;
    private bool isInvulnerable;
    public int invulnerableTime = 1;
    private float horizontal;
    private float vertical;
    void Start(){
        isInvulnerable = false;  
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
        canShoot = true;
        gameOver = false;
        soundSource = gameObject.GetComponent<AudioSource>();
        weapon = GetComponentInChildren<Weapon>();
        crossHair = Instantiate(crossHairPrefab, new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), transform.rotation) as Transform;
        StartCoroutine(cycleFootsteps());
        //need to get current level and load relevant script
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Level1"){
            trikefightscript = GameObject.Find("BossTrigger1").GetComponent<TrikeFight>();
        }else if (scene.name == "Level2"){
            trexfightscript = GameObject.Find("BossTrigger2").GetComponent<TRexFight>();
        }//else gavin
        startWeapon1Type = GameMaster.slot1type;
		startWeapon2Type = GameMaster.slot2type;
		GameMaster.levelStartSlot1Type = startWeapon1Type;
		GameMaster.levelStartSlot2Type = startWeapon2Type;
        if(startWeapon1Type != WeaponType.empty){
            slot1 = Instantiate(weaponsprefabs[(int)startWeapon1Type], transform.position + weaponsprefabs[(int)startWeapon1Type].transform.position, new Quaternion(0,0,0,0), this.transform);
            slot1type = startWeapon1Type;
        }
        if(startWeapon2Type != WeaponType.empty){
            slot2 = Instantiate(weaponsprefabs[(int)startWeapon2Type], transform.position + weaponsprefabs[(int)startWeapon2Type].transform.position, new Quaternion(0,0,0,0), this.transform);
            slot2type = startWeapon2Type;
        }
        if(slot1 != null){
			slot1.GetComponent<Weapon>().ammo = GameMaster.slot1ammo;
			GameMaster.levelStartSlot1Ammo = slot1.GetComponent<Weapon>().ammo;
        }
        if(slot2 != null){
			slot2.GetComponent<Weapon>().ammo = GameMaster.slot2ammo;
			GameMaster.levelStartSlot2Ammo = slot2.GetComponent<Weapon>().ammo;
        }
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
            //Quaternion rotation = Quaternion.LookRotation(movement);
            //dustParticles.transform.rotation = Quaternion.Lerp(dustParticles.transform.rotation, Quaternion.Inverse(rotation), 0.1f);
        }
        else{
            rb.velocity = Vector3.zero;
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }
    }

    void Update(){
        GameMaster.slot1type = slot1type;
        GameMaster.slot2type = slot2type;
        if(slot1 != null){
            GameMaster.slot1ammo = slot1.GetComponent<Weapon>().ammo;
        }
        if(slot2 != null){
            GameMaster.slot2ammo = slot2.GetComponent<Weapon>().ammo;
        }
        weaponslist = GameObject.FindGameObjectsWithTag("Pickup");
        crossHair.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        //when the pickup button is pressed it gets the closest gun, if close enough its picked up
        if (Input.GetButtonDown("Pickup")){
            float minDist = Mathf.Infinity;
            GameObject closestWeapon = null;
            foreach (GameObject weapon in weaponslist){
                float dist = Vector3.Distance(transform.position, weapon.transform.position);
                if (dist < minDist){
                    minDist = dist;
                    closestWeapon = weapon;
                }
            }
            if (minDist < 2){
                PickupPrefab pk = closestWeapon.GetComponent<PickupPrefab>();
                ChangeWeapon(pk.type, pk, closestWeapon);
            }
        }

		if (Input.GetButtonDown ("SwapSlot") && (slot1active && slot2 != null || !slot1active && slot1 != null)) {
			slot1active = !slot1active;
		}

        if (slot1active && slot1 != null){
            slot1.SetActive(true);
            if (slot2 != null){
                slot2.SetActive(false);
            }
        }else if (!slot1active && slot2 != null){
            slot1.SetActive(false);
            if (slot1 != null){
                slot2.SetActive(true);
            }
        }

        if(canMove){
            
        }else{
            soundSource.Stop();
        }
        if(Time.timeScale!=0){
            Cursor.visible = false;
        } else if(Time.timeScale == 0){
            Cursor.visible = true;
        }
}

    private IEnumerator cycleFootsteps(){
        while(true){
            soundSource.clip = footstepSounds[Random.Range(0,4)];
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
                if (!soundSource.isPlaying){
                    soundSource.Play();
                }
            }else{
                soundSource.Stop();
            }
            yield return new WaitForSeconds(soundSource.clip.length);

        }
    }

	public void TakeDamage(int amount, Quaternion dir, float force){
        if(!isInvulnerable){
            foreach (Renderer r in GetComponentsInChildren<Renderer>()){
                if(r.gameObject.tag != "MuzzelFlash"){
                    Color c = r.material.color;
                    c.a = 1f;
                    r.material.color = c;
                }
            }
        }else{
            foreach (Renderer r in GetComponentsInChildren<Renderer>()){
                 if(r.gameObject.tag != "MuzzelFlash"){
                     Color c = r.material.color;
                     c.a = 0.3f;
                     r.material.color = c;
                 }
             }
        }
}

	public void TakeDamage(int amount, Quaternion dir, float force, Transform source){
        
        if(!isInvulnerable){
            health -= amount;
            FireBloodParticles(dir);

            StartCoroutine(becomeInvulnerable());
        }

        //Knockback
		knockback = dir * Vector2.up;
		knockback *= force;
		knockbackTimer = 10;

        if (health <= 0){
            //AudioSource.PlayClipAtPoint (deathRoar, transform.position);
            GameObject.Find("Canvas").GetComponent<HUDManager>().inBossFight = false;
            Destroy (gameObject);
            //Debug.Log("you should be dead");
            gameOver = true;
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
	

   public void ChangeWeapon(WeaponType type, PickupPrefab pickup, GameObject pickupObject){
		int pickupAmmo = pickup.ammo;
		if(slot1active){
			if(slot1 == null){
				slot1 = Instantiate(weaponsprefabs[(int)type], transform.position + weaponsprefabs[(int)type].transform.position, new Quaternion(0,0,0,0), this.transform);
                slot1type = type;
                Destroy(pickupObject);
				if (pickupAmmo != -1) {
					slot1.GetComponent<Weapon> ().ammo = pickupAmmo;
				}
			}else if(slot2 == null){
				slot2 = Instantiate(weaponsprefabs[(int)type], transform.position + weaponsprefabs[(int)type].transform.position, new Quaternion(0,0,0,0), this.transform);
                slot2type = type;
                slot1active = false;
				Destroy(pickupObject);
				if (pickupAmmo != -1) {
					slot2.GetComponent<Weapon> ().ammo = pickupAmmo;
				}
            }else{
				//drop slot 1 gun
				pickup.ChangeType(slot1type, slot1.GetComponent<Weapon>().ammo);
                Destroy(slot1);  
				slot1 = Instantiate(weaponsprefabs[(int)type], transform.position + weaponsprefabs[(int)type].transform.position, new Quaternion(0,0,0,0), this.transform);
				slot1type = type;
				if (pickupAmmo != -1) {
					slot1.GetComponent<Weapon> ().ammo = pickupAmmo;
				}
			}
		}else{
			if(slot2 == null){
				slot2 = Instantiate(weaponsprefabs[(int)type], transform.position + weaponsprefabs[(int)type].transform.position, new Quaternion(0,0,0,0), this.transform);
                slot2type = type;
				Destroy(pickupObject);
				if (pickupAmmo != -1) {
					slot2.GetComponent<Weapon> ().ammo = pickupAmmo;
				}
			}else if(slot1 == null){
				slot1 = Instantiate(weaponsprefabs[(int)type], transform.position + weaponsprefabs[(int)type].transform.position, new Quaternion(0,0,0,0), this.transform);
				slot1type = type;
				slot1active = true;
				Destroy(pickupObject);
				if (pickupAmmo != -1) {
					slot1.GetComponent<Weapon> ().ammo = pickupAmmo;
				}
			}else{
				//drop slot 2 gun
				pickup.ChangeType(slot2type, slot2.GetComponent<Weapon>().ammo);
                Destroy(slot2);
				slot2 = Instantiate(weaponsprefabs[(int)type], transform.position + weaponsprefabs[(int)type].transform.position, new Quaternion(0,0,0,0), this.transform);
				slot2type = type;
				if (pickupAmmo != -1) {
					slot2.GetComponent<Weapon> ().ammo = pickupAmmo;
				}
			}
        }
    }

    public void SwapSlot(){
        if (slot1active == true){
            //swap to 2
            slot1active = false;
        }else{
            //swap to 1
            slot1active = true;
        }
    }
}