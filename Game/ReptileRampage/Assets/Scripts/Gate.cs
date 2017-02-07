using UnityEngine;

public class Gate : MonoBehaviour {

	public Transform player;
	public GameObject canvas;
	private HUDManager hudManager;
	private Animator animator;
	public bool isLevel1;
	public bool isLevel2;

	void Start () {
		hudManager = canvas.GetComponent<HUDManager>();
		animator = gameObject.GetComponent<Animator>();
	}
	
	void Update () {
		float dist = Vector3.Distance(player.position, transform.position);

		if(isLevel1){
			if(dist > 2f && player.position.x < transform.position.x && hudManager.inBossFight){
				animator.Play("GateClosing");
				if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("GateClosed")){
					animator.Play("GateClosed");
				}
				gameObject.GetComponent<Collider2D>().enabled = true;
			}else if(!hudManager.inBossFight){
				animator.Play("GateIdle");
			}
		} else if(isLevel2){
			//Debug.Log(dist);
			if(dist > 6f && player.position.y > transform.position.y && hudManager.inBossFight){
				gameObject.GetComponent<Collider2D>().enabled = true;
				gameObject.GetComponent<SpriteRenderer>().enabled = true;
			}
		}
	}

	void ShakeScreen() {
		gameObject.GetComponent<CameraShake>().StartShaking(1);
	}
}
