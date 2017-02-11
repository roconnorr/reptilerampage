using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour {
 
     public Transform[] wayPointList;
     public int currentWayPoint = 0; 
     private Transform targetWayPoint;
	 public GameObject player;
	 private Player playerScript;
     public float speed = 4f;
	 public static bool arrived;
	 private bool wait;
	 private bool firstRun;
 
     void Start () {
		playerScript = player.GetComponent<Player>();
		firstRun = true;
     }
     
     void Update () {
         if(transform.position == wayPointList[0].position){
			 arrived = true;
			 if(firstRun){
				 StartCoroutine(Wait());
				 firstRun = false;
			 }
			 playerScript.canMove = true;
			 playerScript.canShoot = true;
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