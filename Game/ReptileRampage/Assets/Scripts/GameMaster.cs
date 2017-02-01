using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static Transform CreateBullet(Transform prefab, Vector3 position, float angle, int damage, float speed, float range, bool dmgPlayer, bool dmgEnemy) {
		Transform bullet = Instantiate (prefab, position, Quaternion.Euler(0, 0, angle));
		bullet.GetComponent<Bullet>().moveSpeed = speed;
		bullet.GetComponent<Bullet>().damage = damage;
		bullet.GetComponent<Bullet>().range = range;
		bullet.GetComponent<Bullet>().dmgPlayer = dmgPlayer;
		bullet.GetComponent<Bullet>().dmgEnemy = dmgEnemy;
		return bullet;
	}

	public static Transform CreateHomingBullet(Transform prefab, Vector3 position, float angle, int damage, float speed, float range, bool dmgPlayer, bool dmgEnemy, Transform target) {
		Transform bullet = Instantiate (prefab, position, Quaternion.Euler(0, 0, angle));
		bullet.GetComponent<BulletHoming> ().initialAngle = angle;
		bullet.GetComponent<BulletHoming>().moveSpeed = speed;
		bullet.GetComponent<BulletHoming>().damage = damage;
		bullet.GetComponent<BulletHoming>().range = range;
		bullet.GetComponent<BulletHoming> ().target = target;
		bullet.GetComponent<BulletHoming>().dmgPlayer = dmgPlayer;
		bullet.GetComponent<BulletHoming>().dmgEnemy = dmgEnemy;
		return bullet;
	}
}
