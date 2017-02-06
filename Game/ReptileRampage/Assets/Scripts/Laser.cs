using UnityEngine;

public class Laser : MonoBehaviour {

	private Vector3 position;

	void Start() {
		position = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
	}

	void Update() {
		transform.localPosition = position;
		float shakeY = Random.Range(-0.05f, 0.05f);
		Vector3 pp = transform.position;
		pp.y += shakeY;
		transform.position = pp;
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			float angle =Mathf.Atan2(other.transform.position.y-transform.position.y, transform.position.x-transform.position.x)*180 / Mathf.PI;
			angle -= 90;
			other.gameObject.GetComponent<Player>().TakeDamage (GetComponentInParent<Gavin>().laserDamage, Quaternion.Euler(0, 0, angle), 500, GetComponentInParent<Gavin>().transform);
		}
	}
}
