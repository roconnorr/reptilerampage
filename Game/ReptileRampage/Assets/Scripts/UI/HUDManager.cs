using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
   public Text Slot1Text;
   public Text Slot2Text;
   public Text Slot1Ammo;
   public Text Slot2Ammo;
   public Image Slot1Image;
   public Image Slot2Image;

   public Sprite[] WeaponSprites;

   public Slider HUDHealth;

   public bool inBossFight = false;

   public GameObject BossHealthObject;
   public Slider HUDBossHealth;
   public GameObject levelBoss;

   public GameObject player;
   private Player playerScript;
   private Enemy bossScript;
   private float bossHealth;
   private float health;

   public GameObject pauseMenuPanel;
   private PauseMenuManager pauseMenu;

   public GameObject gameOverPanel;
   private GameOverManager gameOver;
   void Start () {
		playerScript = player.GetComponent<Player>();
      	health = playerScript.health;
            pauseMenu = pauseMenuPanel.GetComponent<PauseMenuManager>();
            pauseMenu.Hide(); 
            gameOver = gameOverPanel.GetComponent<GameOverManager>();
            gameOver.Hide(); 
            BossHealthObject.SetActive(false);
   }
   
      void Update () {
	   if(playerScript.slot1 != null){
            Slot1Text.text = playerScript.slot1.name.Substring(0, playerScript.slot1.name.Length - 7).ToUpper();
	   	Slot1Image.sprite = WeaponSprites[(int) playerScript.slot1type];
            Slot1Ammo.text = playerScript.slot1.GetComponent<Weapon>().ammo + "/" +playerScript.slot1.GetComponent<Weapon>().maxAmmo;
	   }

	   if(playerScript.slot2 != null){
		Slot2Text.text = playerScript.slot2.name.Substring(0, playerScript.slot2.name.Length - 7).ToUpper();
		Slot2Image.sprite = WeaponSprites[(int) playerScript.slot2type];
            Slot2Ammo.text = playerScript.slot2.GetComponent<Weapon>().ammo + "/" +playerScript.slot2.GetComponent<Weapon>().maxAmmo;
	   }
         if(inBossFight){
            bossScript = levelBoss.GetComponent<Enemy>();
            bossHealth = bossScript.health;
            bossHealth = Mathf.MoveTowards(bossHealth, bossScript.health, 60*Time.deltaTime);
            HUDBossHealth.value = bossHealth;
         }
      
      // Display health - but rather than doing it in one go, change the value
      // gradually (over certain period of time)   
      health = Mathf.MoveTowards(health, playerScript.health, 60*Time.deltaTime);
      HUDHealth.value = health;


      if(playerScript.gameOver) {
         // If gameover state detected, show the pause menu in gameover mode   
         gameOver.GameOver();
      } else if(Input.GetKeyDown(KeyCode.Escape)) {
         // If user presses ESC, show the pause menu in pause mode
         if(pauseMenu.isPaused){
               pauseMenu.Resume();
         }else{
            pauseMenu.ShowPause();
         }
      }
   }

   public void SetBossHealthActive(bool active){
         if(active){
               BossHealthObject.SetActive(true);  
         }else{
               BossHealthObject.SetActive(false); 
         }
   }
}