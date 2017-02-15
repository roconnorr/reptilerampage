using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour {
	 
	 public bool level1Intro;
	 public bool level1HeliTrike;
	 public bool level2;
     public Transform[] wayPointList;
     public int currentWayPoint = 0; 
     private Transform targetWayPoint;
	 public GameObject player;
	 private Player playerScript;
     public float speed = 4f;
	 [HideInInspector]
	 public static bool arrived;
	 private bool wait;
	 private bool firstRun0;
	 private bool firstRun1;
	 private bool firstRun2;
	 [HideInInspector]
	 public static bool respawned;
	 public Sprite secondBoat;
	 public static bool level1IntroFinished;
	 private Transform fakeTrike;
	 private Rigidbody2D rb;
	 public static bool triggerdTrike;
	 public static bool heliMoving;
	 [HideInInspector]
	 public static bool triggerdTrex;
	 public Transform rocketPrefab;
	 private Transform rocketFirePoint;
	 public Transform target;
	 private Animator animator;
	 [HideInInspector]
	 public static bool trexSpawnAnimationPlaying;
	 [HideInInspector]
	 public static bool trexSpawnAnimationFinished;
	 public static bool trexCam = false;
	 private AudioSource soundPlayer;

     void Start () {
		playerScript = player.GetComponent<Player>();
		soundPlayer = GetComponent<AudioSource>();
		fakeTrike = transform.Find ("FakeTrike");
		firstRun0 = true;
		firstRun1 = true;
		firstRun2 = true;
     }
     
	 void Update () {
		 //Level 1 intro
		 if(level1Intro && arrived && respawned){
			 Destroy(gameObject);
		 }else if(level1Intro){

			 if(transform.position == wayPointList[0].position){
				 if(firstRun0){
					soundPlayer.enabled = false;
					wait = true;
					firstRun0 = false;
				 }
			 	 if(TextBoxManager.dialogFinished){
				 	arrived = true;
				 	GetComponent<SpriteRenderer>().sprite = secondBoat;
				 	if(firstRun1){
				 		StartCoroutine(Wait());
				 		firstRun1 = false;
						TextBoxManager.dialogFinished = false;
						level1IntroFinished = true;
			 	 	}
			 	 }
		 	 }
			 if(transform.position == wayPointList[1].position){
				 Destroy(gameObject);
		 	 }
			 if(currentWayPoint < this.wayPointList.Length && !wait){
				 if(targetWayPoint == null){
					 targetWayPoint = wayPointList[currentWayPoint];
				 }
				 MoveBoat();
			 }


		//Level 1 heli trike
		}else if(level1HeliTrike && level1IntroFinished){
			if(TextBoxManager.dialogFinished && currentWayPoint == 0){
				if(firstRun1 /*&& arrived*/){
					//arrived = false;
					heliMoving = true;					
					firstRun1 = false;
				}
				GetComponent<HeliMovement>().enabled = true;
				animator = GetComponent<Animator>();
				animator.enabled = true;
				animator.Play("HeliWithTrike");
				GetComponent<SpriteRenderer>().enabled = true;
				if(targetWayPoint == null){
					targetWayPoint = wayPointList[currentWayPoint];
				}
				MoveHeli();
		 	 }

			 if(Mathf.Abs(transform.position.x - wayPointList[0].position.x) < 0.1f){
				 //arrived = true;
				 if(firstRun0){
					TextBoxManager.dialogFinished = false;
					wait = true;
					//======== drop trike ========
					animator.Play("HeliWithoutTrike");
					fakeTrike.localPosition = new Vector3(-0.022f, -1.441f, 1f);
					fakeTrike.GetComponent<SpriteRenderer>().enabled = true;
					rb = fakeTrike.GetComponent<Rigidbody2D>();
					rb.gravityScale = 0.4f;
					//======== drop trike ========
					firstRun0 = false;
				 }
			 }
			 //If trike is on the ground
			 if(Mathf.Abs(fakeTrike.position.y - 45) < 0.15f && firstRun2){
				 fakeTrike.GetComponent<SpriteRenderer>().enabled = false;
				 rb.velocity = Vector3.zero;
				 triggerdTrike = true;
				 gameObject.GetComponent<CameraShake>().StartShaking(1);
				 //arrived = false;
				 Invoke("cameraBackToplayer", 2f);
				 firstRun2 = false;
			 }
			 if(Mathf.Abs(transform.position.x - wayPointList[1].position.x) < 0.1f){
				 Destroy(gameObject);
		 	 }

			 if(currentWayPoint < this.wayPointList.Length && !wait && currentWayPoint != 0){
				 if(targetWayPoint == null){
					 targetWayPoint = wayPointList[currentWayPoint];
				 }
				 MoveHeli();
			 }
		

		//Level 2 cutscene
		}else if(level2){
			if(firstRun1 && arrived){
				arrived = false;
				firstRun1 = false;
			}
			if(TextBoxManager.dialogFinished && currentWayPoint == 0){
				GetComponent<HeliMovement>().enabled = true;
				animator = GetComponent<Animator>();
				animator.enabled = true;
				animator.Play("Helicopter");
				GetComponent<SpriteRenderer>().enabled = true;
				if(targetWayPoint == null){
					targetWayPoint = wayPointList[currentWayPoint];
				}
				MoveHeli();
		 	 }

			 if(Mathf.Abs(transform.position.x - wayPointList[0].position.x) < 0.1f){
				 arrived = true;
				 if(firstRun0){
					TextBoxManager.dialogFinished = false;
					wait = true;
					rocketFirePoint = transform.Find ("RocketFirePoint");
					float dist = Vector3.Distance(rocketFirePoint.position, target.position) + 18;
					GameMaster.CreateHomingBullet (rocketPrefab, rocketFirePoint.position, Random.Range (240, 260), 0, 12, dist, false, false, target, transform);
					GameMaster.CreateHomingBullet (rocketPrefab, rocketFirePoint.position, Random.Range (240, 260), 0, 12, dist, false, false, target, transform);
					firstRun0 = false;
				 }
			 }
			 if(BulletHoming.bridgeExploded){
				target.GetComponent<SpriteRenderer>().enabled = true;
				trexCam = true;
				Invoke ("TriggerTRex", 2);
				arrived = false;
			 }
			 if(trexSpawnAnimationFinished){
				 trexSpawnAnimationPlaying = false;
				 playerScript.canMove = true;
				 playerScript.canShoot = true;
				 wait = false;
				 trexSpawnAnimationFinished = false;
			 }
			 if(Mathf.Abs(transform.position.x - wayPointList[1].position.x) < 0.1f){
				 Destroy(gameObject);
		 	 }

			 if(currentWayPoint < this.wayPointList.Length && !wait && currentWayPoint != 0){
				 if(targetWayPoint == null){
					 targetWayPoint = wayPointList[currentWayPoint];
				 }
				 MoveHeli();
			 }
		}
		
     }
	 
	 void MoveBoat(){
         // move towards the target
		 soundPlayer.enabled = true;
         transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed*Time.deltaTime);
 
         if(transform.position == targetWayPoint.position && (currentWayPoint+1 < this.wayPointList.Length)){
             currentWayPoint++;
             targetWayPoint = wayPointList[currentWayPoint];
         }
     }
     void MoveHeli(){
         // move towards the target
		 soundPlayer.enabled = true;
         transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed*Time.deltaTime);
 
		 if((Mathf.Abs(transform.position.x - targetWayPoint.position.x)) < 0.1f && (currentWayPoint+1 < this.wayPointList.Length)){
             currentWayPoint++;
             targetWayPoint = wayPointList[currentWayPoint];
         }
     } 

	 IEnumerator Wait() {
		wait = true;
    	yield return new WaitForSeconds(3f);
		wait = false;
 	 }

	 void cameraBackToplayer() {
		 heliMoving = false;
		 playerScript.canMove = true;
		 playerScript.canShoot = true;
		 wait = false;
	 }

	 void TriggerTRex() {
		 triggerdTrex = true;
	 }
 }