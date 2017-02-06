using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {
   public bool isPaused;
   
   public void ShowPause() {
      isPaused = true;
      gameObject.SetActive(true);
   }
   
   
   public void Hide() {
      gameObject.SetActive(false);
      isPaused = false;
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
      if(isPaused) {
         Time.timeScale = 0;
      }  
   }
}