using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    Transform player; // a reference to the player for raycasting
    public bool playerInRange; // is the player within the enemy's sight range
    public bool playerSeen;
    // Target of the chase
	// (initialise via the Inspector Panel)
	public GameObject target = null;
    //pathfinding speed
    public float speed;

    private AStarPathfinder pathfinder = null;

    public Transform[] navpoints;
    private int destPoint = 0;

	
    void Start()
    {
        playerInRange = false;
        player = GameObject.Find("Player").transform;
        pathfinder = transform.GetComponent<AStarPathfinder> ();
    }

  
    void FixedUpdate()
    {
        if (CanPlayerBeSeen()){
			transform.position = Vector3.MoveTowards(this.transform.position,player.position, 0.05f);
            //broken pathfinding
            //pathfinder.GoTowards(target, speed);	   
		}else{
		    //NextPoint();
            pathfinder.GoTowards(player.position, 2, true);
		}
    }


    bool CanPlayerBeSeen()
    {
        // we only need to check visibility if the player is within the enemy's visual range
        if (playerInRange){
            return (!PlayerHiddenByObstacles());
		}else{
            return false;
		}


    }
    void OnTriggerStay2D(Collider2D other)
    {
        // if 'other' is player, the player is seen 
        if (other.transform.tag == "Player")
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // if 'other' is player, the player is seen
        // note, we don't really need to check the transform tag since the collision matrix is set to only 'see' collisions with the player layer
        if (other.transform.tag == "Player")
            playerInRange = false;
    }

    void NextPoint() {
            // Returns if no points have been set up
            if (navpoints.Length == 0)
                return;

            //transform.position = Vector3.MoveTowards(this.transform.position,navpoints[destPoint].position, 0.1f);
            pathfinder.GoTowards(navpoints[destPoint].position, speed, false);


            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            if(Vector3.Distance(navpoints[destPoint].position, this.transform.position) < 0.5){
                //destPoint = (destPoint + 1) % navpoints.Length;
                //random movement between points
                destPoint = Random.Range(0, navpoints.Length);
            }
        }

    bool PlayerHiddenByObstacles()
    {

        float distanceToPlayer = Vector2.Distance(this.transform.position, player.position);
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, player.position - transform.position, distanceToPlayer);
        Debug.DrawRay(this.transform.position, player.position - this.transform.position, Color.blue); // draw line in the Scene window to show where the raycast is looking
        List<float> distances = new List<float>();
     
        foreach (RaycastHit2D hit in hits)
        {           
            // ignore the enemy's own colliders (and other enemies)
            if (hit.transform.tag == "Enemy")
                continue;
            
            // if anything other than the player is hit then it must be between the player and the enemy's eyes (since the player can only see as far as the player)
            if (hit.transform.tag != "Player")
            {
                if(playerSeen == true){
                   // pathfinder.Reset(this.transform.position, player.position);
                }
                playerSeen = false;
                return true;
            }
        }

        // if no objects were closer to the enemy than the player return false (player is not hidden by an object)
        playerSeen = true;
        return false; 
        

    }

}
