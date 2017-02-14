using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour {
	 
	 public bool level1;
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
	 [HideInInspector]
	 public static bool respawned;
	 public Sprite secondBoat;
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

     void Start () {
		playerScript = player.GetComponent<Player>();
		firstRun0 = true;
		firstRun1 = true;
     }
     
	 void Update () {
		 if(level1 && arrived && respawned){
			 Destroy(gameObject);
		 }else if(level1){

			 if(transform.position == wayPointList[0].position){
				 if(firstRun0){
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
				 Move0();
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
				Move1();
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
				 Move1();
			 }
		}
		
     }
	 
	 void Move0(){
         // move towards the target
         transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed*Time.deltaTime);
 
         if(transform.position == targetWayPoint.position && (currentWayPoint+1 < this.wayPointList.Length)){
             currentWayPoint++;
             targetWayPoint = wayPointList[currentWayPoint];
         }
     }
     void Move1(){
         // move towards the target
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

	void TriggerTRex() {
		triggerdTrex = true;
	}
 }