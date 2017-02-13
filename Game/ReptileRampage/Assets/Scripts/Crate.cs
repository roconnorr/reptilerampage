using UnityEngine;

public class Crate : MonoBehaviour {
	public int health = 30;

	public bool isTier1;
	public bool isTier2;
	public bool isTier3;

	public bool isWeapon;

	public GameObject pickup;
	public GameObject healthPack;
	public GameObject ammoPack;
	public GameObject grenadePickup;

	public AudioClip crateBreakSound;
	public ParticleSystem crateParticles;
	private int[] tier1rare1 = new int[2] {1, 9};
	private int[] tier1rare2 = new int[2] {12, 20};
	private int[] tier1rare3 = new int[1] {13};
	private int[] tier1rare4 = new int[0] {};
	private int[] tier1rare5 = new int[0] {};

	private int[] tier2rare1 = new int[3] {1, 9, 10};
	private int[] tier2rare2 = new int[3] {12, 14, 20};
	private int[] tier2rare3 = new int[4] {13, 16, 17, 22};
	private int[] tier2rare4 = new int[1] {0};
	private int[] tier2rare5 = new int[2] {4, 24};

	private int[] tier3rare1 = new int[1] {10};
	private int[] tier3rare2 = new int[3] {2, 14, 15};
	private int[] tier3rare3 = new int[4] {16, 17, 18, 22};
	private int[] tier3rare4 = new int[1] {0};
	private int[] tier3rare5 = new int[7] {3, 4, 6, 7, 19, 23, 24};
 
    
    void Start() {
    }

//	void OnCollisionEnter2D(Collision2D other){
//		if(other.gameObject.tag == "Player"){
//			Destroy (gameObject);
//			SpawnStuff();
//		}
//	}

	public void TakeDamage(int amount) {
		health -= amount;
		if (health <= 0) {
			//AudioSource.PlayClipAtPoint(crateBreakSound, this.transform.position, 1.0f);
			PlayHitSound(crateBreakSound, this.transform.position);
			ParticleSystem localCrateParticles = Instantiate(crateParticles, this.transform.position, new Quaternion(0,0,0,0)) as ParticleSystem;
			localCrateParticles.Play();
			Destroy (gameObject);
			SpawnStuff();
		}
	}

	void SpawnStuff(){
		if (isWeapon) {
			pickAGun ();
		} else {
			float chance = Random.value;

			if (chance <= 0.25) {
				pickAGun ();
			}
			if (chance > 0.25 && chance <= 0.35) {
				Instantiate (grenadePickup, transform.position, transform.rotation);
			}
			if (chance > 0.35 && chance <= 0.75) {
				Instantiate (ammoPack, transform.position, transform.rotation);
			}
			if (chance > 0.75 && chance <= 1.0) {
				Instantiate (healthPack, transform.position, transform.rotation);
			}
		}
	}

	void pickAGun(){
		GameObject gun = Instantiate(pickup, transform.position, transform.rotation);
		PickupPrefab pickupPrefab = gun.GetComponent<PickupPrefab>();

		bool arrayEmpty = true;
		int [] array = null;
		while(arrayEmpty){
			float chance = Random.value;
			if(chance <= 0.3){
				if(isTier1){
					array = tier1rare1;
				} else if (isTier2){
					array = tier2rare1;
				} else {
					array = tier3rare1;
				}
			}
			if(chance > 0.3 && chance <= 0.55){
				if(isTier1){
					array = tier1rare2;
				} else if (isTier2){
					array = tier2rare2;
				} else {
					array = tier3rare2;
				}
			}
			if(chance > 0.55 && chance <= 0.75){
				if(isTier1){
					array = tier1rare3;
				} else if (isTier2){
					array = tier2rare3;
				} else {
					array = tier3rare3;
				}
        	}
			if(chance > 0.75 && chance <= 0.9) {
				if(isTier1){
					array = tier1rare4;
				} else if (isTier2){
					array = tier2rare4;
				} else {
					array = tier3rare4;
				}
    		}
			if(chance > 0.9) {
				if(isTier1){
					array = tier1rare5;
				} else if (isTier2){
					array = tier2rare5;
				} else {
					array = tier3rare5;
				}
    		}
			if(array.Length > 0) {
        		arrayEmpty = false;
    		}
		}
		int pick = Random.Range(0, array.Length);
		pickupPrefab.type = (Player.WeaponType) array[pick];
	}

	public static void PlayHitSound(AudioClip clip, Vector3 pos){
		GameObject temp = new GameObject("TempAudio");
		temp.transform.position = pos;
		AudioSource tempSource = temp.AddComponent<AudioSource>();
		tempSource.clip = clip;
		tempSource.volume = 0.12f;
		if(!tempSource.isPlaying){
			tempSource.Play();
		}
		Destroy(temp, clip.length);
	}
}