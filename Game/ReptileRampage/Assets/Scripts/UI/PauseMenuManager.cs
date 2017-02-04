using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {
   public Text resumeText;
   public Text quitText;
   bool pauseGame;
   
   void Start () {
   }
   
   // Show the pause menu in pause mode (the
   // first option will say "Resume")
   public void ShowPause() {
      pauseGame = true;
      gameObject.SetActive(true);
   }
   
   
   // Hide the menu panel
   public void Hide() {
      // Deactivate the panel
      gameObject.SetActive(false);
      // Resume the game (if paused)
      pauseGame = false;
      Time.timeScale = 1f;
   }

   public void Resume(){
	   Hide();
   }

   public void Restart(){
	   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
   
   public void Quit(){
         SceneManager.LoadScene("TitleScreen");
   }
   
   void Update () {    
      // If game is in pause mode, stop the timeScale value to 0
      if(pauseGame) {
         Time.timeScale = 0;
      }      
   }
}