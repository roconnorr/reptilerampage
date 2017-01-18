using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWall : MonoBehaviour {

	
	void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Wall") {
            Destroy(gameObject);
        }
   }
}
