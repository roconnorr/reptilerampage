using UnityEngine;

public class DisableDialog : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(WayPoints.respawned){
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
