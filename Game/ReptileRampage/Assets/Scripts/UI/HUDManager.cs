using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
   public Text Slot1Text;
   public Text Slot2Text;
   public Image Slot1Image;
   public Image Slot2Image;

   public Sprite[] WeaponSprites;

   public Slider HUDHealth;
   public GameObject player;
   private Player playerScript;
   private float health;

   public GameObject pauseMenuPanel;
   private PauseMenuManager pauseMenu;
   void Start () {
		playerScript = player.GetComponent<Player>();
      	health = playerScript.health;
            pauseMenu = pauseMenuPanel.GetComponent<PauseMenuManager>();
            pauseMenu.Hide(); 
   }
   
	void Update () {
	   if(playerScript.slot1 != null){
            Slot1Text.text = playerScript.slot1.name.Substring(0, playerScript.slot1.name.Length - 7).ToUpper();
	   		Slot1Image.sprite = WeaponSprites[(int) playerScript.slot1type];
	   }

	   if(playerScript.slot2 != null){
		   Slot2Text.text = playerScript.slot2.name.Substring(0, playerScript.slot2.name.Length - 7).ToUpper();
		   Slot2Image.sprite = WeaponSprites[(int) playerScript.slot2type];
	   }
      
      // Display health - but rather than doing it in one go, change the value
      // gradually (over certain period of time)   
      health = Mathf.MoveTowards(health, playerScript.health, 20*Time.deltaTime);
      HUDHealth.value = health;

      if(playerScript.gameOver) {
         // If gameover state detected, show the pause menu in gameover mode   
         //pauseMenu.ShowGameOver();
      } else if(Input.GetKey(KeyCode.Escape)) {
         // If user presses ESC, show the pause menu in pause mode
         pauseMenu.ShowPause();
      }
   }
}