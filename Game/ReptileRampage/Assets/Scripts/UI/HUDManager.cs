using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
   public GameObject HUDGunPanel;
   public Text Slot1Text;
   public Text Slot2Text;
   public Image Slot1Image;
   public Image Slot2Image;

   public Sprite[] WeaponSprites;

   public Slider HUDHealth = null;

   public GameObject player;
   private Player playerScript;
   float health;
   void Start () {
		playerScript = player.GetComponent<Player>();
      
      
      // Set the starting health value for display
      //health = healthInfoProvider.health;
   }
   
   // Update is called once per frame
	void Update () {
		if(playerScript.slot1 != null){
			Slot1Text.text = playerScript.slot1.name;
	   		Slot1Image.sprite = WeaponSprites[(int) playerScript.slot1type];
	   }

	   if(playerScript.slot2 != null){
		   Slot2Text.text = playerScript.slot2.name;
		   Slot2Image.sprite = WeaponSprites[(int) playerScript.slot2type];
	   }


      // Display the score
      //HUDGunDisplay.text = "Score: " + scoreInfoProvider.score;
      
      // Display health - but rather than doing it in one go, change the value
      // gradually (over certain period of time)   
      //health = Mathf.MoveTowards(health, healthInfoProvider.health, 20*Time.deltaTime);
      //hudHealth.value = health;
   }
}