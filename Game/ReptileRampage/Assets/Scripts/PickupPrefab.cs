using UnityEngine;

public class PickupPrefab : MonoBehaviour {

	public Player.WeaponType type;

	public Sprite[] WeaponSprites;

	private SpriteRenderer spriteRenderer; 

	public int ammo = -1;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = WeaponSprites[(int)type];
	}

	public void ChangeType(Player.WeaponType newType, int ammoCount){
		ammo = ammoCount;
		type = newType;
		spriteRenderer.sprite = WeaponSprites[(int)type];
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			if (other.GetComponent<Player>().slot1.GetComponent<Weapon>().type == type) {
				other.GetComponent<Player>().slot1.GetComponent<Weapon>().AddAmmo (ammo);
				Destroy (gameObject);
			} else if (other.GetComponent<Player>().slot2 != null && other.GetComponent<Player>().slot2.GetComponent<Weapon>().type == type) {
				other.GetComponent<Player>().slot1.GetComponent<Weapon>().AddAmmo (ammo);
				Destroy (gameObject);
			}
		}
	}
}
