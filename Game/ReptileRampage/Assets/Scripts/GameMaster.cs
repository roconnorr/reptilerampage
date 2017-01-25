using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static void CreateBullet(Transform prefab, Vector3 position, float angle, int damage, float speed, bool dmgPlayer) {
		Transform bullet = Instantiate (prefab, position, Quaternion.Euler(0, 0, angle));
		bullet.GetComponent<Bullet>().moveSpeed = speed;
		bullet.GetComponent<Bullet>().damage = damage;
		bullet.GetComponent<Bullet>().dmgPlayer = dmgPlayer;
	}

	public static void CreateHomingBullet(Transform prefab, Vector3 position, float angle, int damage, float speed, bool dmgPlayer, Transform target, float turnSpeed) {
		Transform bullet = Instantiate (prefab, position, Quaternion.Euler(0, 0, angle));
		bullet.GetComponent<BulletHoming>().moveSpeed = speed;
		bullet.GetComponent<BulletHoming>().damage = damage;
		bullet.GetComponent<BulletHoming>().turnSpeed = turnSpeed;
		bullet.GetComponent<BulletHoming> ().target = target;
		bullet.GetComponent<BulletHoming>().dmgPlayer = dmgPlayer;
	}
}
