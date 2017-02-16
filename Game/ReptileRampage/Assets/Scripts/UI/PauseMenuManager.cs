using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {
   public bool isPaused;
   public GameObject volumePanel;
   
   public void ShowPause() {
      isPaused = true;
      gameObject.SetActive(true);

      transform.GetChild(0).gameObject.SetActive(true);
      transform.GetChild(1).gameObject.SetActive(true);
      transform.GetChild(2).gameObject.SetActive(true);
      gameObject.GetComponent<Image>().enabled = true;
      volumePanel.SetActive(false);
   }
   
   
   public void Hide() {
      gameObject.SetActive(false);
      isPaused = false;
      Time.timeScale = 1f;
   }

   public void Resume(){
	   Hide();
   }

   public void Volume(){
         /*for(int i = 0; i < transform.childCount-1; i++){
            transform.GetChild(i).gameObject.SetActive(false);
         }*/
         transform.GetChild(0).gameObject.SetActive(false);
         transform.GetChild(1).gameObject.SetActive(false);
         transform.GetChild(2).gameObject.SetActive(false);
         gameObject.GetComponent<Image>().enabled = false;
         volumePanel.SetActive(true);
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