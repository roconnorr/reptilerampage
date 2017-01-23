using UnityEngine;

public class DestroyAfterAnimationEnd : MonoBehaviour {

	public float delay = 0f;

	void Start () {
		Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
	}
}
