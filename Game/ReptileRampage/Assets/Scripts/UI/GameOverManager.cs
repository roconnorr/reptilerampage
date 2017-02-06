using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

	public void Hide() {
            gameObject.SetActive(false);
      }
	public void GameOver(){
            gameObject.SetActive(true);
            Cursor.visible = true;
	}
	public void Restart(){
	      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }

	public void Quit(){
            SceneManager.LoadScene("TitleScreen");
   }
}
