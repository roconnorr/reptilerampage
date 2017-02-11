using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour {

	public Transform player;
	public GameObject canvas;
	private HUDManager hudManager;
	private Animator animator;
	private SpriteRenderer sr;
	public bool isLevel1;
	public bool isLevel2;
	public bool gateway1;
	public bool gateway2;
	public Sprite sprite2;

	void Start () {
		hudManager = canvas.GetComponent<HUDManager>();
		animator = gameObject.GetComponent<Animator>();
		sr = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	void Update () {
		if(player == null){
			return;
		}
		float dist = Vector3.Distance(player.position, transform.position);

		if (isLevel1) {
			if (dist > 2f && player.position.x < transform.position.x && hudManager.inBossFight) {
				animator.Play ("GateClosing");
				if (this.animator.GetCurrentAnimatorStateInfo (0).IsName ("GateClosed")) {
					animator.Play ("GateClosed");
				}
				gameObject.GetComponent<Collider2D> ().enabled = true;
			} else if (!hudManager.inBossFight) {
				animator.Play ("GateIdle");
			}
		} else if (isLevel2) {
			//Debug.Log(dist);
			if (dist > 5.5f && player.position.y > transform.position.y && hudManager.inBossFight) {
				gameObject.GetComponent<Collider2D> ().enabled = true;
				gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			}
		} else if (gateway1) {
			if (GameMaster.currentLevel == 2) {
				gameObject.GetComponent<Collider2D> ().enabled = true;
				sr.sprite = sprite2;
			}
		} else if(gateway2) {
			if(GameMaster.currentLevel == 3){
				gameObject.GetComponent<Collider2D>().enabled = true;
				sr.sprite = sprite2;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(gateway1 && gameObject.GetComponent<Collider2D>().enabled){
			if(other.tag == "Player"){
				SceneManager.LoadScene("Level2");
			}
		}
		if(gateway2 && gameObject.GetComponent<Collider2D>().enabled){
			if(other.tag == "Player"){
				SceneManager.LoadScene("Level3");
			}
		}
	}

	void ShakeScreen() {
		gameObject.GetComponent<CameraShake>().StartShaking(1);
	}
}
