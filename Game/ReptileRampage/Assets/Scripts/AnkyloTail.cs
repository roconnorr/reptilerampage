using UnityEngine;
using System.Collections;

public class AnkyloTail : MonoBehaviour {

	public int damage;
	public float fireRate;
	public float shotSpeed;
	public float range;
	//public float strayFactor;
	public GameObject target;
	public Transform bulletPrefab;
	public AudioClip shotSound = null;
	public int burstBulletLimit = 5;
	public float timeTilNextBurst = 8f;
	public int shootRange;

	private float timeToFire = 0;
	private bool trigger = true;
	private bool targetInShootRange = false;
	private Transform firePoint;
	private int bulletCount;
	private Animator animator;

	void Start () {
		firePoint = transform.FindChild ("FirePoint");
		animator = GetComponent<Animator>();
	}

	void Update () {
		targetInShootRange = Vector3.Distance(gameObject.transform.position, target.transform.position) < shootRange;

		if(targetInShootRange && GetComponent<Ankylosaurus>().isChasing){
			if (trigger && Time.time > timeToFire) {
				animator.Play ("Ankylo_Smash");
				timeToFire = Time.time + 1/fireRate;
				//Only shoot while last frame is playing
	
				//if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.3 && !animator.IsInTransition(0)){
					for(int i=0; i<=360; i+=45){
						CreateBullet(i);
					}
				//}asdasd
				bulletCount++;	
			}
			if (trigger && bulletCount >= burstBulletLimit){
				StartCoroutine(Wait());
			}
		}
	}

	IEnumerator Wait() {
    	trigger = false;
    	yield return new WaitForSeconds(timeTilNextBurst);
		bulletCount = 0;
    	trigger = true;

 	}
	
	void CreateBullet (float angle) {
		//Create bullet with stray modifier
		//float strayValue = Random.Range(-strayFactor, strayFactor);
		GameMaster.CreateBullet (bulletPrefab, firePoint.position, angle - 90, damage, shotSpeed, range, true, false);
		//Play sound
		if(shotSound != null){
			AudioSource.PlayClipAtPoint(shotSound, transform.position);
		}
	}
}
