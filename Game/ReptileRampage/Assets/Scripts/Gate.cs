using UnityEngine;

public class Gate : MonoBehaviour {

	public Transform player;
	public GameObject canvas;
	private HUDManager hudManager;

	private Animator animator;

	void Start () {
		hudManager = canvas.GetComponent<HUDManager>();
		animator = gameObject.GetComponent<Animator>();
	}
	
	void Update () {
		float dist = Vector3.Distance(player.position, transform.position);

		if(dist > 2f && player.position.x < transform.position.x && hudManager.inBossFight){
			animator.Play("GateClosing");
			if(this.animator.GetCurrentAnimatorStateInfo(0).IsName("GateClosed")){
				animator.Play("GateClosed");
			}
			gameObject.GetComponent<Collider2D>().enabled = true;
		}else if(!hudManager.inBossFight){
			animator.Play("GateIdle");
		}
	}
}
