using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
   public Text Slot1Text;
   public Text Slot2Text;
   public Text Slot1Ammo;
   public Text Slot2Ammo;
   public Image Slot1Image;
   public Image Slot2Image;

   public Image Slot1Active;
   public Image Slot2Active;
   public Image Slot3Active;

   public Text GrenadeCount;

   public Image PistolImage;

   public Text PistolName;

   public Image[] Slot1Stars;
   
   public Image[] Slot2Stars;

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
            for(int i=0; i<Slot1Stars.Length; i++){
                  Slot1Stars[i].enabled = false;
                  Slot2Stars[i].enabled = false;
            }
   }
   
      void Update () {
	   if(playerScript.slot[0] != null){
			Slot1Text.text = playerScript.slot[0].name.Substring(0, playerScript.slot[0].name.Length - 7).ToUpper();
	   	Slot1Image.sprite = WeaponSprites[(int) playerScript.slot1type];
			Slot1Ammo.text = playerScript.slot[0].GetComponent<Weapon>().ammo + "/" +playerScript.slot[0].GetComponent<Weapon>().maxAmmo;
			for(int i=0; i < playerScript.slot[0].GetComponent<Weapon>().stars; i++){
                  Slot1Stars[i].enabled = true;
            }  
	   }

		if(playerScript.slot[1] != null){
			Slot2Text.text = playerScript.slot[1].name.Substring(0, playerScript.slot[1].name.Length - 7).ToUpper();
			Slot2Image.sprite = WeaponSprites[(int) playerScript.slot2type];
			Slot2Ammo.text = playerScript.slot[1].GetComponent<Weapon>().ammo + "/" +playerScript.slot[1].GetComponent<Weapon>().maxAmmo;
			for(int i=0; i < playerScript.slot[1].GetComponent<Weapon>().stars; i++){
                  Slot2Stars[i].enabled = true;
            }  
	   }
         PistolImage.sprite = WeaponSprites[(int)playerScript.slot3type];
         if(player != null){
            PistolName.text = playerScript.slot[2].name.Substring(0, playerScript.slot[2].name.Length - 7).ToUpper();
         }
         GrenadeCount.text = playerScript.grenadeCount.ToString();

         if(playerScript.slotActive == 0){
               Slot1Active.enabled = true;
               Slot2Active.enabled = false;
               Slot3Active.enabled = false;
         }else if(playerScript.slotActive == 1){
               Slot2Active.enabled = true;
               Slot1Active.enabled = false;         
               Slot3Active.enabled = false;
         }else{
               Slot3Active.enabled = true;
               Slot1Active.enabled = false;
               Slot2Active.enabled = false;
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

   public void ResetStars(){
         for(int i=0; i<5; i++){
               Slot1Stars[i].enabled = false;
               Slot2Stars[i].enabled = false;
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