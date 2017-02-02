using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {

	public GameObject ButtonStart;
	public GameObject ButtonQuit;

	void Start(){
		ButtonStart.SetActive(false);
		ButtonQuit.SetActive(false);
	}

	public void EnableButtons(){
		ButtonStart.SetActive(true);
		ButtonQuit.SetActive(true);
	}
   
   public void StartGame () {
      SceneManager.LoadScene("Level1");   
   }
   
   public void QuitGame() {
      // Quit the application
      Application.Quit();
   }
}
