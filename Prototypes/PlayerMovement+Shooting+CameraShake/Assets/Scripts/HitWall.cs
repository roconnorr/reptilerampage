using UnityEngine;

public class HitWall : MonoBehaviour {

    public GameObject explosionPrefab;
    
	void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Wall") {
            Explode();
            Destroy(gameObject);
        }
   }

      void Explode(){
       GameObject explosion = (GameObject)Instantiate(explosionPrefab);
       explosion.transform.position = transform.position;
   }
}
