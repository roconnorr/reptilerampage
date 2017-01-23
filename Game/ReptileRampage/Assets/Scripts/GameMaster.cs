using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static void CreateBullet(Transform prefab, Vector3 position, float angle, float damage, float speed, bool dmgPlayer) {
		Transform bullet = Instantiate (prefab, position, Quaternion.Euler(0, 0, angle));
		bullet.GetComponent<Bullet>().moveSpeed = speed;
		bullet.GetComponent<Bullet>().damage = damage;
		bullet.GetComponent<Bullet>().dmgPlayer = dmgPlayer;
	}
}
