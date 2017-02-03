using UnityEngine;

public class Crate : MonoBehaviour {

	//private AudioSource soundSource;

	public int health = 30;

	public bool isTier1;
	public bool isTier2;
	public bool isTier3;

	public GameObject pickup;

	private int[] tier1rare1 = new int[3] {1, 9, 11};
	private int[] tier1rare2 = new int[4] {8, 12, 20, 21};
	private int[] tier1rare3 = new int[1] {13};
	private int[] tier1rare4 = new int[0] {};
	private int[] tier1rare5 = new int[0] {};

	private int[] tier2rare1 = new int[4] {1, 9, 10, 11};
	private int[] tier2rare2 = new int[5] {8, 12, 14, 20, 21};
	private int[] tier2rare3 = new int[4] {5, 13, 16, 17};
	private int[] tier2rare4 = new int[1] {0};
	private int[] tier2rare5 = new int[1] {4};

	private int[] tier3rare1 = new int[4] {1, 9, 10, 11};
	private int[] tier3rare2 = new int[7] {2, 8, 12, 14, 15, 20, 21};
	private int[] tier3rare3 = new int[5] {5, 13, 16, 17, 18};
	private int[] tier3rare4 = new int[2] {0, 8};
	private int[] tier3rare5 = new int[4] {3, 4, 6, 19};
 
    
    void Start() {
		//soundSource = gameObject.GetComponent<AudioSource>();
    }

	public void TakeDamage(int amount) {
		health -= amount;
		if (health <= 0) {
			Destroy (gameObject);
			SpawnStuff();
		}
	}

	void SpawnStuff(){
		float chance = Random.value;
		//should be <= 0.25
		if(chance <= 1){
			pickAGun();
		}
		//if(chance > 0.25 && chance <= 0.75){
			//Spawn ammo
		//}
		//if(chance > 0.75 && chance <= 1.0){
			//Spawn health
		//}
	}

	void pickAGun(){
		GameObject gun = Instantiate(pickup, transform.position, transform.rotation);
		PickupPrefab pickupPrefab = gun.GetComponent<PickupPrefab>();

		bool arrayEmpty = true;
		int [] array = null;
		while(arrayEmpty){
			float chance = Random.value;
			if(chance <= 0.35){
				if(isTier1){
					array = tier1rare1;
				} else if (isTier2){
					array = tier2rare1;
				} else {
					array = tier3rare1;
				}
			}
			if(chance > 0.35 && chance <= 0.6){
				if(isTier1){
					array = tier1rare2;
				} else if (isTier2){
					array = tier2rare2;
				} else {
					array = tier3rare2;
				}
			}
			if(chance > 0.6 && chance <= 0.8){
				if(isTier1){
					array = tier1rare3;
				} else if (isTier2){
					array = tier2rare3;
				} else {
					array = tier3rare3;
				}
        	}
			if(chance > 0.8 && chance <= 0.95) {
				if(isTier1){
					array = tier1rare4;
				} else if (isTier2){
					array = tier2rare4;
				} else {
					array = tier3rare4;
				}
    		}
			if(chance > 0.95) {
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
}