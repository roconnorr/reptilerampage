using UnityEngine;

public class DisableDialog : MonoBehaviour {

	void Start () {
		if(WayPoints.respawned){
			gameObject.SetActive(false);
		}
	}
}
