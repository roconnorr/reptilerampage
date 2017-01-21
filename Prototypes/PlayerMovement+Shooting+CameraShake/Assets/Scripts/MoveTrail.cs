using UnityEngine;

public class MoveTrail : MonoBehaviour {
	
	public int moveSpeed = 230;
	
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		Destroy (gameObject, 4);
	}
}
