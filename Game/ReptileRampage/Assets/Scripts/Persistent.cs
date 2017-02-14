using UnityEngine;

public class Persistent : MonoBehaviour {

	// Use this for initialization
	void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
