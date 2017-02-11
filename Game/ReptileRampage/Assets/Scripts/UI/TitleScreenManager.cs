using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour {

	public GameObject ButtonStart;
	public GameObject ButtonArena;
	public GameObject ButtonQuit;
	public string ArenaPassword;

	void Start(){
		ButtonStart.SetActive(false);
		ButtonQuit.SetActive(false);
		ButtonArena.SetActive(false);
		Cursor.visible = true;
	}

	public void EnableButtons(){
		ButtonStart.SetActive(true);
		ButtonQuit.SetActive(true);
		ButtonArena.SetActive(true);
	}
   
   public void StartGame () {
      SceneManager.LoadScene("Level1");   
   }

   public void StartArena(){
	   SceneManager.LoadScene("Arena");
   }
   
   public void QuitGame() {
      // Quit the application
      Application.Quit();
   }
}
