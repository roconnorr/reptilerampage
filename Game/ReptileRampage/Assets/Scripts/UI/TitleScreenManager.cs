using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour {

	public GameObject ButtonStart;
	public GameObject ButtonArena;
	public GameObject ButtonQuit;
	public GameObject ButtonCredits;
	public GameObject ButtonCreditsQuit;
	public Image CreditsImage;

	void Start(){
		ButtonStart.SetActive(false);
		ButtonQuit.SetActive(false);
		ButtonArena.SetActive(false);
		ButtonCredits.SetActive(false);
		ButtonCreditsQuit.SetActive(false);
		CreditsImage.enabled = false;
		Cursor.visible = true;
	}

	public void EnableButtons(){
		ButtonStart.SetActive(true);
		ButtonQuit.SetActive(true);
		ButtonArena.SetActive(true);
		ButtonCredits.SetActive(true);
	}
   
   public void StartGame () {
      SceneManager.LoadScene("Level1");   
   }

   public void StartArena(){
	   SceneManager.LoadScene("Arena");
   }

   public void DisplayCredits(){
	   CreditsImage.enabled = true;
	   ButtonCreditsQuit.SetActive(true);
   }
   public void HideCredits(){
	   CreditsImage.enabled = false;
	   ButtonCreditsQuit.SetActive(false);
   }
   
   public void QuitGame() {
      // Quit the application
      Application.Quit();
   }
}
