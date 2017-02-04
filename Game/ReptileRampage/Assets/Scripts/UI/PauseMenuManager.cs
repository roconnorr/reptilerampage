using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {
   bool pauseGame;
   
   public void ShowPause() {
      pauseGame = true;
      gameObject.SetActive(true);
   }
   
   
   public void Hide() {
      gameObject.SetActive(false);
      pauseGame = false;
      Time.timeScale = 1f;
   }

   public void Resume(){
	   Hide();
   }
   
   public void Quit(){
         Resume();
         SceneManager.LoadScene("TitleScreen");
   }
   
   void Update () {   
      if(pauseGame) {
         Time.timeScale = 0;
      }  
   }
}