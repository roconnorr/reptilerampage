using UnityEngine;

public class Player : MonoBehaviour {

   public float speed = 10;

   private AudioSource soundSource;

   public ParticleSystem dustParticles;

   private Rigidbody2D rb;

   private Weapon weapon;

   public enum WeaponType {handgun, machinegun};
   public GameObject[] weaponsprefabs;
   private GameObject[] weaponslist;
   public GameObject slot1 = null;
   public GameObject slot2 = null;
   public WeaponType slot1type;
   public WeaponType slot2type;
   
   private bool slot1active = true;
   public bool intrigger;
   private PickupPrefab pk;

   private float horizontal;
   private float vertical;
   void Start() {
        rb = GetComponent<Rigidbody2D>();
        soundSource = gameObject.GetComponent<AudioSource>();
        weapon = GetComponentInChildren<Weapon>();
    }

   void OnCollisionEnter2D(Collision2D other){
    	rb.velocity = Vector3.zero;
    }
    /*
    void OnTriggerEnter2D(Collider2D other){
        intrigger = true;
        pickupObject = other.gameObject;
        pk = other.gameObject.GetComponent<PickupPrefab>();
    }

    void OnTriggerExit2D(Collider2D other){
        intrigger = false;
    }*/


   void FixedUpdate () {

         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");
 
         Vector2 movement = new Vector2(horizontal, vertical);

         rb.AddForce(movement * speed / Time.deltaTime);
         //rb.velocity = movement * speed;
 
         if (rb.velocity.magnitude > speed)
         {
             rb.velocity = rb.velocity.normalized * speed;
         }
         //Quaternion rotation = Quaternion.LookRotation(movement);
         //dustParticles.transform.rotation = Quaternion.Lerp(dustParticles.transform.rotation, Quaternion.Inverse(rotation), 0.1f);    
   }

   void Update(){
       weaponslist = GameObject.FindGameObjectsWithTag("Pickup");

       //when the pickup button is pressed it gets the closest gun, if close enough its picked up
       if(Input.GetButtonDown("Pickup")){
           float minDist = Mathf.Infinity;
           GameObject closestWeapon = null; 
           foreach (GameObject weapon in weaponslist){
               float dist = Vector3.Distance(transform.position, weapon.transform.position);
               if(dist < minDist){
                   minDist = dist;
                   closestWeapon = weapon;
               }
           }
           if(minDist < 2){
               PickupPrefab pk = closestWeapon.GetComponent<PickupPrefab>();
               ChangeWeapon(pk.type, pk, closestWeapon);
           }
        }

       if(Input.GetButton("Slot1")){
            slot1active = true;
            slot1.SetActive(true);
       }

       if(Input.GetButton("Slot2")){
            slot1active = false;
            slot2.SetActive(true); 
        }
       
        if(slot1active && slot1 != null){
            slot1.SetActive(true);
            if(slot2 != null){
                slot2.SetActive(false);
            }
        }else if(!slot1active && slot2 != null){
            slot1.SetActive(false);
            if(slot1 != null){
                slot2.SetActive(true);
            }
        }

        

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
             if(!soundSource.isPlaying){
                soundSource.Play();
             }
        }else{
            soundSource.Stop();
        }
        
   }

   public void ChangeWeapon(WeaponType type, PickupPrefab pickup, GameObject pickupObject){
       if(slot1active){
			if(slot1 == null){
				slot1 = Instantiate(weaponsprefabs[(int)type], this.transform.position, new Quaternion(0,0,0,0), this.transform);
                slot1type = type;
                Destroy(pickupObject);
			}else if(slot2 == null){
                slot2 = Instantiate(weaponsprefabs[(int)type], this.transform.position, new Quaternion(0,0,0,0), this.transform);
                slot2type = type;
                slot1active = false;
                Destroy(pickupObject);
			}else{
				//drop slot 1 gun
                Destroy(slot1);
                pickup.ChangeType(slot1type);  
                slot1 = Instantiate(weaponsprefabs[(int)type], this.transform.position, new Quaternion(0,0,0,0), this.transform);
                slot1type = type;
			}
		}else{
			if(slot2 == null){
                slot2 = Instantiate(weaponsprefabs[(int)type], this.transform.position, new Quaternion(0,0,0,0), this.transform);
                slot2type = type;
				Destroy(pickupObject);
			}else if(slot1 == null){
                slot1 = Instantiate(weaponsprefabs[(int)type], this.transform.position, new Quaternion(0,0,0,0), this.transform);
                slot1type = type;
				Destroy(pickupObject);
			}else{
				//drop slot 2 gun
                Destroy(slot2);
                pickup.ChangeType(slot2type);
                slot2 = Instantiate(weaponsprefabs[(int)type], this.transform.position, new Quaternion(0,0,0,0), this.transform);
                slot2type = type;
			}
		}
	}

	public void SwapSlot(){
		if(slot1active == true){
            //swap to 2
			slot1active = false;
		}else{
			//swap to 1
			slot1active = true;
		}
	}
}