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

   public GameObject HudGunPanel;

   public GameObject HudPistolPanel;

   public bool arenaMode;

   public Text waveNumberText;
   
   public Text betweenWaveText;
   private WaveMaster waveMaster;

   public GameObject arenaTrikeHealthObject;

   public GameObject arenaTrexHealthObject;
   public Slider arenaTrikeHealth;

   public Slider arenaTrexHealth;

   public bool arenaTrikeAlive;
   public bool arenaTrexAlive;

   public GameObject arenaTrikeInstance;
   public GameObject arenaTrexInstance;
   


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
            if(arenaMode){
                  waveNumberText.enabled = true;
                  waveMaster = GameObject.Find("WaveMaster").GetComponent<WaveMaster>();
            }else{
                  waveNumberText.enabled = false;
            }
            betweenWaveText.enabled = false;
            arenaTrikeHealthObject.SetActive(false);
            arenaTrexHealthObject.SetActive(false);
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
         if(arenaMode){
               waveNumberText.text = "WAVE: " + waveMaster.currentWave;
               if(waveMaster.betweenWaves){
                  float timeLeft =  waveMaster.timeToNewWave - waveMaster.timer;
                  betweenWaveText.enabled = true;
                  betweenWaveText.text = "NEXT WAVE IN: " + timeLeft.ToString("0.0");
               }else{
                  betweenWaveText.enabled = false;
               }
               if(arenaTrikeAlive){
                  arenaTrikeHealthObject.SetActive(true);
                  arenaTrikeHealth.value = arenaTrikeInstance.GetComponent<Enemy>().health;
               }else{
                   arenaTrikeHealthObject.SetActive(false);
               }
               if(arenaTrexAlive){
                  arenaTrexHealthObject.SetActive(true);
                  arenaTrexHealth.value = arenaTrexInstance.GetComponent<Enemy>().health;
               }else{
                  arenaTrexHealthObject.SetActive(false);
               }
               
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

   public void HideBottomHUD(bool active){
         if(!active){
            HudGunPanel.SetActive(true);
            HudPistolPanel.SetActive(true);
         }else{
            HudGunPanel.SetActive(false);
            HudPistolPanel.SetActive(false);
         }
   }
}