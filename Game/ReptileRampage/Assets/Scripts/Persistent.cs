using UnityEngine;
using UnityEngine.SceneManagement;

public class Persistent : MonoBehaviour {

    private Scene currentScene;
    private string sceneName;

	// Use this for initialization
	void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    void Update(){
        currentScene = SceneManager.GetActiveScene ();
        sceneName = currentScene.name;
        if(sceneName == "TitleScreen"){
            Destroy(gameObject);
        }
    }
}

