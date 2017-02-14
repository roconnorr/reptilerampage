using UnityEngine;
using UnityEngine.SceneManagement;

public class Presentation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Level1")) {
			SceneManager.LoadScene("Level1");
		}
		if (Input.GetButtonDown ("Level2")) {
			SceneManager.LoadScene("Level2");
		}
		if (Input.GetButtonDown ("Level3")) {
			SceneManager.LoadScene("Level3");
		}
		if (Input.GetButtonDown ("Level4")) {
			SceneManager.LoadScene("FinalBoss");
		}
	}
}
