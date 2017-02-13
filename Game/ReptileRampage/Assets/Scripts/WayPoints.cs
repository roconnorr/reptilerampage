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
	 public static bool arrived;
	 private bool wait;
	 private bool firstRun0;
	 private bool firstRun1;
	 public Sprite secondBoat;
	 public static bool triggerdTrex;
	 public Transform rocketPrefab;
	 private Transform rocketFirePoint;
	 public Transform target;
 
     void Start () {
		playerScript = player.GetComponent<Player>();
		firstRun0 = true;
		firstRun1 = true;
     }
     
	 void Update () {
		 if(level1){
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
			 	 	}
					TextBoxManager.dialogFinished = false;
			 	}
		 	 }
			 if(transform.position == wayPointList[1].position){
				 Destroy(gameObject);
		 	 }
			 if(currentWayPoint < this.wayPointList.Length && !wait){
				 if(targetWayPoint == null){
					 targetWayPoint = wayPointList[currentWayPoint];
				 }
				 Move();
			 }


		//Level 2 cutscene
		}else if(level2){
			if(TextBoxManager.dialogFinished && currentWayPoint == 0){
				GetComponent<SpriteRenderer>().enabled = true;
				//triggerdTrex = true;
					  if(targetWayPoint == null){
						  targetWayPoint = wayPointList[currentWayPoint];
					  }
					  Move();
				 	  /*if(firstRun1){
						   StartCoroutine(Wait());
				 		   firstRun1 = false;
			 	 	  }*/
		 	 }

			 if(transform.position == wayPointList[0].position){
				 arrived = true;
				 if(firstRun0){
					wait = true;
					rocketFirePoint = transform.Find ("RocketFirePoint");
					GameMaster.CreateHomingBullet (rocketPrefab, rocketFirePoint.position, Random.Range (240, 260), 0, 12, 30, false, false, target, transform);
					GameMaster.CreateHomingBullet (rocketPrefab, rocketFirePoint.position, Random.Range (240, 260), 0, 12, 30, false, false, target, transform);
					firstRun0 = false;
				 }
			 }
			 if(BulletHoming.bridgeExploded){
					target.GetComponent<SpriteRenderer>().enabled = true;
					playerScript.canMove = true;
				 	playerScript.canShoot = true;
					triggerdTrex = true;
					arrived = false;
					wait = false;
			 }
	
			 if(transform.position == wayPointList[1].position){
				 Destroy(gameObject);
		 	 }

			 if(currentWayPoint < this.wayPointList.Length && !wait && currentWayPoint != 0){
				 if(targetWayPoint == null){
					 targetWayPoint = wayPointList[currentWayPoint];
				 }
				 Move();
			 }
		}
     }
 
     void Move(){
         // move towards the target
         transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed*Time.deltaTime);
 
         if(transform.position == targetWayPoint.position && (currentWayPoint+1 < this.wayPointList.Length)){
             currentWayPoint++;
             targetWayPoint = wayPointList[currentWayPoint];
         }
     } 

	 IEnumerator Wait() {
		wait = true;
    	yield return new WaitForSeconds(3f);
		wait = false;
 	}
 }