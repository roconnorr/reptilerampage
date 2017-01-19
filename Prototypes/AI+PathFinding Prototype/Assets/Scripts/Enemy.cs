using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{

    // Use this for initialization
    public bool playerInRange; // is the player within the enemy's sight range collider (this only checks if the enemy can theoretically see the player if nothing is in the way)

	[SerializeField]
	Transform lineOfSightEnd;
    Transform player; // a reference to the player for raycasting
    void Start()
    {
        playerInRange = false;
        player = GameObject.Find("Player").transform;
    }

  
    void FixedUpdate()
    {
        if (CanPlayerBeSeen()){
			transform.position = Vector3.MoveTowards(this.transform.position,player.position, 0.1f);
			this.GetComponent<Chase>().enabled = false;
			   
		}else{
			//pathfind for a few seconds
			this.GetComponent<Chase>().enabled = true;
			//go back to idling
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
        // note, we don't really need to check the transform tag since the collision matrix is set to only 'see' collisions with the player layer
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
                return true;
            }
        }

        // if no objects were closer to the enemy than the player return false (player is not hidden by an object)
        return false; 

    }

}
