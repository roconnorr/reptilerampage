using UnityEngine;

public class BulletHitDetection : MonoBehaviour{

    public GameObject explosionPrefab;

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Wall"){
            Explode();
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player"){
            Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>());
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == "Enemy"){
            Explode();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void Explode(){
        GameObject explosion = (GameObject)Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
    }
}
